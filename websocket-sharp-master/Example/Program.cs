using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Linq;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Security.Cryptography;

class Program
{
    public static NetworkInterface GetDefaultInterface()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (var intf in interfaces)
        {
            if (intf.OperationalStatus != OperationalStatus.Up)
            {
                continue;
            }
            if (intf.NetworkInterfaceType == NetworkInterfaceType.Loopback)
            {
                continue;
            }

            var properties = intf.GetIPProperties();
            if (properties == null)
            {
                continue;
            }
            var gateways = properties.GatewayAddresses;
            if ((gateways == null) || (gateways.Count == 0))
            {
                continue;
            }
            var addresses = properties.UnicastAddresses;
            if ((addresses == null) || (addresses.Count == 0))
            {
                continue;
            }
            return intf;
        }
        return NetworkInterface.GetAllNetworkInterfaces()[0];
    }

    public static IPAddress GetDefaultIPV4Address(NetworkInterface intf)
    {
        if (intf == null)
        {
            return IPAddress.Parse("0.0.0.0");
        }
        foreach (var address in intf.GetIPProperties().UnicastAddresses)
        {
            if (address.Address.AddressFamily != AddressFamily.InterNetwork)
            {
                continue;
            }
            if (address.IsTransient)
            {
                continue;
            }
            return address.Address;
        }
        return IPAddress.Parse("0.0.0.0");
    }
    public class MsgToSend
    {
        private string hostname = "";
        private string ipAddress = "";
        private string message = "";
        private string token = "";

        public string Hostname { get => hostname; set => hostname = value; }
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public string Token { get => token; set => token = value; }
        public string Message { get => message; set => message = value; }
        public override string ToString() => "{\n  \"Hostname\": \"" + Hostname + "\", \n  \"IpAddress\": \"" + IpAddress + "\", \n  \"Token\": \"" + Token + "\", \n  \"Message\": \"" + Message + "\"\n}";
    }
    private static byte[] GenerateRandomData()
    {
        byte[] data = new byte[8192];
        using (var gen = RandomNumberGenerator.Create())
        {
            gen.GetBytes(data);
        }
        return data;
    }
    static string Hash(string input)
    {
        var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        var stringBuilder = new StringBuilder(hash.Length * 2);

        foreach (byte b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }
    public static string NextToken(ref List<string> tokens)
    {
        tokens.Add(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2").Substring(0, 6) + "00" + Hash(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2")));
        if (tokens.Count > 3)
        {
            tokens.RemoveAt(0);
        }
        return tokens.Last();
    }


    static void Main(string[] args)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.EnableBroadcast = true;

        Console.WriteLine("This PC:\t" + Dns.GetHostName());

        int BroadcastPort = 11000;

        List<string> validTokens = new List<string>();

        //IPAddress BroadcastIP = IPAddress.Parse("10.0.0.255");
        IPAddress BroadcastIP = IPAddress.Parse(String.Concat(String.Join(".", IPAddress.Parse(GetDefaultIPV4Address(GetDefaultInterface()).ToString()).GetAddressBytes().Take(3).ToArray()), ".255"));
        IPEndPoint EndPoint = new IPEndPoint(BroadcastIP, BroadcastPort);

        int[] ports = new int[4] { BroadcastPort, 0, 0, 0 };
        for (int i = 1; i < ports.Length; i++)
        {
            for (int j = ports[i - 1] + 1; true; j++)
            {
                if (PortInUse(j) == false)
                {
                    ports[i] = j;
                    break;
                }
            }
        }

        while (true)
        {
            Console.WriteLine("Enter your message: ");
            string input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            MsgToSend msgToSend = new MsgToSend
            {
                Hostname = Dns.GetHostName(),
                IpAddress = GetDefaultIPV4Address(GetDefaultInterface()).ToString(),
                Message = input,
                Token = NextToken(ref validTokens)
            };

            //byte[] SendBuffer = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(msgToSend);
            var testSendBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msgToSend));

            socket.SendTo(testSendBuffer, EndPoint);

            Console.WriteLine("\n{0}", String.Join(" ", testSendBuffer));
            Console.WriteLine("\nMessage sent to the broadcast address!");

            Console.WriteLine("MsgToSend:\n" + msgToSend);

            Console.WriteLine();
            foreach (var token in validTokens)
            {
                Console.WriteLine(token);
            }
            Console.WriteLine();

            //JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            //string Visualized = System.Text.Json.JsonSerializer.Serialize(msgToSend, options);

            //Console.WriteLine("Visualized:\n" + Visualized);
        }
    }
    public static bool PortInUse(int port)
    {
        bool inUse = false;

        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

        foreach (IPEndPoint endPoint in ipEndPoints)
        {
            if (endPoint.Port == port)
            {
                inUse = true;
                break;
            }
        }
        return inUse;
    }
}