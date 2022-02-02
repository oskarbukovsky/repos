//using System.Text.Json;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace ClientServer
{
    public class Application
    {
        public class Global
        {
            public static int BroadcastPort = 11000;
            public static List<string> ValidTokens = new List<string>();
            public static List<BroadcastMessage> SendMessages = new List<BroadcastMessage>();
            public static List<BroadcastMessage> RecieveMessages = new List<BroadcastMessage>();
        }
        public class BroadcastMessage
        {
            private string hostname = "";
            private string ipAddress = "";
            private string token = "";
            private string lastToken = "";
            private bool birthMessage = false;
            private bool terminationMessage = false;
            private int[] availablePorts = new int[] { 0, 0, 0 };
            private string message = "";

            public string Hostname { get => hostname; set => hostname = value; }
            public string IpAddress { get => ipAddress; set => ipAddress = value; }
            public string Token { get => token; set => token = value; }
            public string LastToken { get => lastToken; set => lastToken = value; }
            public bool BirthMessage { get => birthMessage; set => birthMessage = value; }
            public bool TerminationMessage { get => terminationMessage; set => terminationMessage = value; }
            public int[] AvailablePorts { get => availablePorts; set => availablePorts = value; }
            public string Message { get => message; set => message = value; }
            public override string ToString() => "{\n" +
                "  \"Hostname\": \"" + Hostname + "\", \n" +
                "  \"IpAddress\": \"" + IpAddress + "\", \n" +
                "  \"Token\": \"" + Token + "\", \n" +
                "  \"LastToken\": \"" + LastToken + "\", \n" +
                "  \"BirthMessage\": \"" + BirthMessage + "\", \n" +
                "  \"TerminationMessage\": \"" + TerminationMessage + "\", \n" +
                "  \"AvailablePorts\": \"[" + AvailablePorts[0] + ", " + AvailablePorts[1] + ", " + AvailablePorts[2] + "]\", \n" +
                "  \"Message\": \"" + Message + "\"\n" +
                "}";
        }
        public class Broadcast
        {
            public IPAddress IpAddress { get; set; }
            public int Port { get; set; }
            public Socket Socket { get; set; }
            public IPEndPoint EndPoint { get; set; }
            public Broadcast(IPAddress iPAddress, int port, Socket socket)
            {
                IpAddress = iPAddress;
                Port = port;
                Socket = socket;
                EndPoint = new IPEndPoint(iPAddress, port);
            }
        }
        public class KnownServer
        {
            private string hostname = "";
            private string lastIpAddress = "";
            private List<string> ipAddressHistory = new List<string>();
            private List<string> validTokens = new List<string>();
            private List<string> tokens = new List<string>();
            private bool alive = false;
            private int[] availablePorts = new int[] { 0, 0, 0 };

            public string Hostname { get => hostname; set => hostname = value; }
            public string LastIpAddress { get => lastIpAddress; set => lastIpAddress = value; }
            public List<string> IpAddressHistory { get => ipAddressHistory; set => ipAddressHistory = value; }
            public List<string> ValidTokens { get => validTokens; set => validTokens = value; }
            public List<string> Tokens { get => tokens; set => tokens = value; }
            public bool Alive { get => alive; set => alive = value; }
            public int[] AvailablePorts { get => availablePorts; set => availablePorts = value; }

            public KnownServer(string hostname, string lastIpAddress, string lastToken, bool alive, int[] availablePorts)
            {
                Hostname = hostname;
                LastIpAddress = lastIpAddress;
                IpAddressHistory.Add(lastIpAddress);
                if (IpAddressHistory.Count >= 2)
                {
                    if (IpAddressHistory.Last() == IpAddressHistory[IpAddressHistory.Count - 2])
                    {
                        IpAddressHistory.RemoveAt(IpAddressHistory.Count - 1);
                    }
                }

                Tokens.Add(lastToken);
                ValidTokens.Add(lastToken);
                if (ValidTokens.Count > 3)
                {
                    ValidTokens.RemoveAt(0);
                }
                Alive = alive;
                AvailablePorts = availablePorts;
            }
        }
        public class KnownHosts
        {
            public static List<KnownServer> Host = new List<KnownServer>();
        }
        public static Task StartListener(bool DEBUG = false)
        {
            UdpClient listener = new UdpClient(Global.BroadcastPort);
            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (true)
                {
                    if (DEBUG == true)
                    {
                        Console.WriteLine("Waiting for broadcast");
                    }
                    byte[] RecievedBytes = listener.Receive(ref EndPoint);

                    if (DEBUG == true)
                    {
                        Console.WriteLine($"\nReceived broadcast from {EndPoint}:");
                    }
                    //Console.WriteLine("\nRaw input:\n" + string.Join(" ", RecievedBytes));

                    var tmp = JsonConvert.DeserializeObject<BroadcastMessage>(Encoding.UTF8.GetString(RecievedBytes, 0, RecievedBytes.Length));
                    if (tmp != null)
                    {
                        Global.RecieveMessages.Add(tmp);
                        if (KnownHosts.Host.Where(host => host.Hostname == tmp.Hostname).Count() == 0)
                        {
                            KnownHosts.Host.Add(new KnownServer(tmp.Hostname, tmp.IpAddress, tmp.Token, !tmp.TerminationMessage, tmp.AvailablePorts));
                        }
                        else
                        {
                            //TODO
                            Console.WriteLine("BB:" + String.Join(", ", KnownHosts.Host.Where(host => host.Hostname == tmp.Hostname).First().ValidTokens) + "CC:" + KnownHosts.Host.Where(host => host.Hostname == tmp.Hostname).First().Hostname);
                        }
                        Console.WriteLine("AA:" + KnownHosts.Host.Where(host => host.Hostname == tmp.Hostname).Count());
                        foreach (var i in KnownHosts.Host.Where(host => host.Hostname == tmp.Hostname))
                        {
                            Console.WriteLine(i.Hostname);
                        }

                        if (DEBUG == true)
                        {
                            Console.WriteLine("Decoded: \n" + Global.RecieveMessages.Last());
                        }
                    }
                }
            }
            catch { }
            finally
            {
                listener.Close();
            }
            return Task.FromResult(true);
        }
        public static void SendBroadcastMessage(ref Broadcast broadcast, BroadcastMessage message)
        {
            broadcast.Socket.SendTo(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)), broadcast.EndPoint);
        }

        public static Task StartSender(bool DEBUG = false)
        {
            int[] ports = new int[4];
            string LocalHostname = Dns.GetHostName();
            string LocalIP = GetDefaultIPV4Address(GetDefaultInterface()).ToString();

            var Broadcast = new Broadcast(IPAddress.Parse(string.Concat(string.Join(".", IPAddress.Parse(GetDefaultIPV4Address(GetDefaultInterface()).ToString()).GetAddressBytes().Take(3).ToArray()), ".255")), Global.BroadcastPort, new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true });

            if (DEBUG == true)
            {
                Console.WriteLine("This PC:\t" + Dns.GetHostName());
            }

            //First message
            CheckPorts(ref ports, Broadcast.Port);
            Global.SendMessages.Add(new BroadcastMessage
            {
                Hostname = LocalHostname,
                IpAddress = LocalIP,
                Token = NextToken(ref Global.ValidTokens),
                BirthMessage = true,
                AvailablePorts = ports.Skip(1).ToArray()
            });
            SendBroadcastMessage(ref Broadcast, Global.SendMessages.Last());

            if (DEBUG == true)
            {
                Console.WriteLine("MsgToSend:\n" + Global.SendMessages.Last());
                Console.WriteLine("ValidTokens:");
                foreach (var token in Global.ValidTokens)
                {
                    Console.WriteLine(" " + token);
                }
                Console.WriteLine("Message sent to the broadcast address!\n");
            }

            //OnExit termination message
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Broadcast.Socket.SendTo(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BroadcastMessage
                {
                    Hostname = LocalHostname,
                    IpAddress = LocalIP,
                    Token = NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2],
                    TerminationMessage = true
                })), Broadcast.EndPoint);
            };
            System.Threading.Thread.Sleep(2000);

            while (true)
            {
                //Console.WriteLine("Enter your message: ");
                //string input = ReadLine();
                string input = "Test";

                CheckPorts(ref ports, Broadcast.Port);
                Global.SendMessages.Add(new BroadcastMessage
                {
                    Hostname = LocalHostname,
                    IpAddress = LocalIP,
                    Token = NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2],
                    AvailablePorts = ports.Skip(1).ToArray(),
                    Message = input
                });
                SendBroadcastMessage(ref Broadcast, Global.SendMessages.Last());

                if (DEBUG == true)
                {
                    Console.WriteLine("MsgToSend:\n" + Global.SendMessages.Last());
                    Console.WriteLine("ValidTokens:");
                    foreach (var token in Global.ValidTokens)
                    {
                        Console.WriteLine(" " + token);
                    }
                    Console.WriteLine("Message sent to the broadcast address!\n");
                }

                System.Threading.Thread.Sleep(2000);
            }
        }
        public static string ReadLine()
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                return string.Empty;
            }
            return input;
        }
        public static void CheckPorts(ref int[] ports, int BroadcastPort)
        {
            ports = new int[4] { BroadcastPort, 0, 0, 0 };
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
        }
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
                if (gateways == null || gateways.Count == 0)
                {
                    continue;
                }
                var addresses = properties.UnicastAddresses;
                if (addresses == null || addresses.Count == 0)
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
            if (tokens.Count == 0)
            {
                tokens.Add(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2").Substring(0, 4) + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2").Substring(0, 4) + Hash(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2")));
            }
            else
            {
                tokens.Add(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2").Substring(0, 4) + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2").Substring(0, 4) + Hash(tokens[tokens.Count - 1]));
            }

            if (tokens.Count > 3)
            {
                tokens.RemoveAt(0);
            }
            return tokens.Last();
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
        public static volatile bool exit = false;
        static void Main()
        {
            Task.Run(() =>
                {
                    StartListener(true);
                });

            Task.Run(() =>
            {
                StartSender();
            });

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.T:
                        string[] tmp = new string[Global.ValidTokens.Count];
                        Global.ValidTokens.CopyTo(tmp);
                        Console.WriteLine("Valid tokens:");
                        foreach (string token in tmp)
                        {
                            Console.WriteLine(" " + token);
                        }
                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        System.Threading.Thread.Sleep(5);
                        break;
                }
            }
        }
    }
}