using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA2211 // Nekonstantní pole nemají být viditelná.
#pragma warning disable IDE0090 // Použít new(...)
#pragma warning disable IDE0056 // Použít operátor indexu
#pragma warning disable IDE0057 // Použít operátor rozsahu

namespace Application
{
    public class Global
    {
        public static int DefaultBroadcastPort = 11000;
        public static List<string> ValidTokens = new List<string>();
        public static List<Udp.BroadcastMessage> SendMessages = new List<Udp.BroadcastMessage>();
        public static List<Udp.BroadcastMessage> RecieveMessages = new List<Udp.BroadcastMessage>();
        public static string LocalHostname = Dns.GetHostName();
        public static IPAddress LocalIP = Tools.GetDefaultIPV4Address(Tools.GetDefaultInterface());
        public static int[] ports = new int[4];
    }

    public class Udp
    {
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
            public Broadcast(IPAddress iPAddress, Socket socket, int port = 11000)
            {
                IpAddress = iPAddress;
                Port = port;
                Socket = socket;
                EndPoint = new IPEndPoint(iPAddress, port);
            }
        }
        public class KnownServers
        {
            public static List<Server> Servers = new List<Server>();
            public class Server
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
                public override string ToString()
                {
                    string result = "{\n" +
                    "  \"Hostname\": \"" + Hostname + "\", \n" +
                    "  \"lastIpAddress\": \"" + lastIpAddress + "\", \n" +
                    "  \"ipAddressHistory\": \"" + IpAddressHistory.First();
                    foreach (var i in IpAddressHistory.Skip(1))
                    {
                        result = String.Concat(result, " " + i);
                    }
                    result = String.Concat(result, "\", \n  \"ValidTokens\": \"" + ValidTokens.First());
                    foreach (var i in ValidTokens.Skip(1))
                    {
                        result = String.Concat(result, " " + i);
                    }
                    result = String.Concat(result, "\", \n  \"Tokens\": \"" + Tokens.First());
                    foreach (var i in Tokens.Skip(1))
                    {
                        result = String.Concat(result, " " + i);
                    }
                    result = String.Concat(result, "\", \n" +
                    "  \"Alive\": \"" + Alive + "\", \n" +
                    "  \"AvailablePorts\": \"[" + AvailablePorts[0] + ", " + AvailablePorts[1] + ", " + AvailablePorts[2] + ", " + AvailablePorts[2] + "]\", \n" +
                    "}");
                    return result;
                }

                public Server(string hostname, string lastIpAddress, string lastToken, bool alive, int[] availablePorts)
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
        }
        public static void StartListener(bool DEBUG = false)
        {
            UdpClient listener = new UdpClient(Global.DefaultBroadcastPort);
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

                    var tmp = Newtonsoft.Json.JsonConvert.DeserializeObject<BroadcastMessage>(Encoding.UTF8.GetString(RecievedBytes, 0, RecievedBytes.Length));
                    if (tmp != null)
                    {
                        Global.RecieveMessages.Add(tmp);
                        if (!KnownServers.Servers.Where(host => host.Hostname == tmp.Hostname).Any())
                        {
                            KnownServers.Servers.Add(new KnownServers.Server(tmp.Hostname, tmp.IpAddress, tmp.Token, !tmp.TerminationMessage, tmp.AvailablePorts));
                        }
                        else
                        {
                            KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).Alive = !tmp.TerminationMessage;
                            KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).AvailablePorts = tmp.AvailablePorts;
                            KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).LastIpAddress = tmp.IpAddress;
                            if (!KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).IpAddressHistory.Where(iphistory => iphistory == tmp.IpAddress).Any())
                            {
                                KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).IpAddressHistory.Add(tmp.IpAddress);
                            };
                            if (!tmp.TerminationMessage)
                            {
                                KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).Tokens.Add(tmp.Token);
                                //if (KnownServers.Servers.Where(host => host.Hostname == tmp.Hostname).First().Tokens.Count() > 128)
                                if (KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).Tokens.Count > 128)
                                {
                                    KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).Tokens.RemoveAt(0);
                                }
                                if (KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens.First() == "0")
                                {
                                    KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens.RemoveAt(0);
                                }
                                KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens.Add(tmp.Token);
                                if (KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens.Count > 3)
                                {
                                    KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens.RemoveAt(0);
                                }
                            }
                            else
                            {
                                //On served death, disable all valid tokens
                                KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).ValidTokens = new List<string> { "0" };
                            }
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
            //return Task.FromResult(true);
        }
        public static void SendBroadcastMessage(ref Broadcast broadcast, BroadcastMessage message)
        {
            broadcast.Socket.SendTo(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(message)), broadcast.EndPoint);
        }

        public static Task StartSender(bool DEBUG = false)
        {
            int[] ports = new int[4];
            string LocalHostname = Dns.GetHostName();
            string LocalIP = Tools.GetDefaultIPV4Address(Tools.GetDefaultInterface()).ToString();

            var Broadcast = new Broadcast(IPAddress.Parse(string.Concat(string.Join(".", IPAddress.Parse(Global.LocalIP.ToString()).GetAddressBytes().Take(3).ToArray()), ".255")), new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true }, Global.DefaultBroadcastPort);

            if (DEBUG == true)
            {
                Console.WriteLine("This PC:\t" + Dns.GetHostName());
            }

            //First message
            Tools.CheckPorts(ref ports, Broadcast.Port);
            Global.SendMessages.Add(new BroadcastMessage
            {
                Hostname = LocalHostname,
                IpAddress = LocalIP,
                Token = Tools.NextToken(ref Global.ValidTokens),
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
                    Token = Tools.NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2].Substring(8),
                    TerminationMessage = true
                })), Broadcast.EndPoint);
            };
            Thread.Sleep(2000);

            while (true)
            {
                //Console.WriteLine("Enter your message: ");
                //string input = ReadLine();
                string input = "Test";

                Tools.CheckPorts(ref ports, Broadcast.Port);
                Global.SendMessages.Add(new BroadcastMessage
                {
                    Hostname = LocalHostname,
                    IpAddress = LocalIP,
                    Token = Tools.NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2].Substring(8),
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

                Thread.Sleep(2000);
            }
        }
    }
    public class Tcp
    {
        public static async Task<dynamic> RecieveSocketMessage(dynamic Server)
        {
            byte[] bytes = new byte[8192];
            try
            {
                if (Server.State == WebSocketState.Open || Server.State == WebSocketState.CloseSent)
                {
                    await Server.ReceiveAsync(bytes, CancellationToken.None);
                }
                else
                {
                    throw new Exception("Closed");
                }
            }
            catch
            {
                Thread.Sleep(5);
                return false;
            }
            Thread.Sleep(5);
            return Encoding.UTF8.GetString(bytes);
        }
        public static async void SendSocketMessage(dynamic Server, string input)
        {
            try
            {
                if (Server.State == WebSocketState.Open || Server.State == WebSocketState.CloseSent)
                {
                    await Server.SendAsync(Encoding.UTF8.GetBytes(input), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    throw new Exception("Closed");
                }
            }
            catch
            {
                //Console.WriteLine("Cannot send message!");
            }
            Thread.Sleep(5);
        }

        public static List<WebSocket> Servers = new List<WebSocket>();
        public class Server
        {
            public static WebSocket StartServer(IPAddress ip, int port)
            {
                var server = new TcpListener(ip, port);
                server.Start();
                Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("\nA client connected.\n");
                NetworkStream stream = client.GetStream();

                // enter to an infinite cycle to be able to handle every change in stream
                while (true)
                {
                    while (!stream.DataAvailable)
                    {
                        Thread.Sleep(5);
                    }
                    while (client.Available < 3) ; // match against "get"

                    byte[] bytes = new byte[client.Available];
                    stream.Read(bytes, 0, client.Available);
                    string s = Encoding.UTF8.GetString(bytes);
                    if (Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase))
                    {
                        //Console.WriteLine("=====Handshaking from client=====\n{0}", s);

                        string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                        string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                        byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                        string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                        // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                        byte[] response = Encoding.UTF8.GetBytes(
                            "HTTP/1.1 101 Switching Protocols\r\n" +
                            "Connection: Upgrade\r\n" +
                            "Upgrade: websocket\r\n" +
                            "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                        //Without valid response, client will break connection
                        stream.Write(response, 0, response.Length);
                        //Console.WriteLine("=====End of client handshake=====\n");
                        var Connection = WebSocket.CreateFromStream(stream, true, "tcp", TimeSpan.FromSeconds(10));
                        server.Stop();
                        return Connection;
                    }
                }
            }
        }
    }
    public class Tools
    {
        public static string ReadLine()
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                return String.Empty;
            }
            return input;
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
        public static void CheckPorts(ref int[] ports, int BroadcastPort = 11000)
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
            return tokens.Last().Substring(8);
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
    }
    class Controller
    {
        static void Main()
        {
            Tools.CheckPorts(ref Global.ports);

            Console.WriteLine("This PC: [" + Global.LocalHostname + "]");

            Console.Write("Ports: [");
            foreach (int port in Global.ports)
            {
                Console.Write(port + ", ");
            }
            Console.WriteLine("\b\b]\n");


            ////////////////////////////////////////////////////////////////////////////////////////////
            Task.Run(() =>
            {
                Udp.StartSender(true);
            });
            Console.WriteLine("\n UDP-Server started");

            ////////////////////////////////////////////////////////////////////////////////////////////
            foreach (int TcpPortPool in Global.ports.Skip(1))
            {
                Task.Run(() =>
                {
                    Tcp.Servers.Add(Tcp.Server.StartServer(Global.LocalIP, TcpPortPool));
                    Console.WriteLine("\n TCP-Server started");
                    var Server = Tcp.Servers.Last();
                    Task.Run(() =>
                    {
                        var TcpPort = TcpPortPool;
                        while (true)
                        {
                            Console.WriteLine();
                            try
                            {
                                var recieved = Tcp.RecieveSocketMessage(Server).Result;
                                if (recieved is string)
                                {
                                    Console.WriteLine("Output: {0}", recieved.TrimEnd('\0'), String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                                }
                                else
                                {
                                    if (Server.State != WebSocketState.Connecting || Server.State != WebSocketState.Open)
                                    {
                                        throw new Exception("Closed");
                                    }
                                }
                            }
                            catch
                            {
                                for (int i = Global.ports[0] + 1; ; i++)
                                {
                                    if (Tools.PortInUse(i) == false)
                                    {
                                        Global.ports[Array.IndexOf(Global.ports, TcpPort)] = i;
                                        TcpPort = i;
                                        break;
                                    }
                                }
                                try
                                {
                                    Server = Tcp.Server.StartServer(Global.LocalIP, TcpPort);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }
                    });
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            Tcp.SendSocketMessage(Server, Global.LocalHostname + "_" + DateTime.Now.ToString("HH:mm:ss"));
                            Thread.Sleep(1000);
                        }
                    });
                });
            }
            Console.ReadKey();
        }
        public static void Main1()
        {
            Tools.CheckPorts(ref Global.ports);

            Console.WriteLine("This PC: [" + Global.LocalHostname + "]");

            Console.Write("Ports: [");
            foreach (int port in Global.ports)
            {
                Console.Write(port + ", ");
            }
            Console.WriteLine("\b\b]\n");

            Console.Write("TCP(P2P) or UDP(Broadcast): [U/T]: default TCP:\t");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.U:
                    Console.Write("\nServer or Client: [S/C]: default Server:\t");
                    switch (Console.ReadKey(true).Key)
                    {
                        default:
                        case ConsoleKey.S:
                            Console.WriteLine("\n UDP-Server");
                            Task.Run(() =>
                            {
                                Udp.StartSender(true);
                            });
                            break;
                        case ConsoleKey.C:
                            Console.WriteLine("\n UDP-Client");
                            Task.Run(() =>
                            {
                                Udp.StartListener(true);
                            });
                            break;
                    }
                    break;
                default:
                case ConsoleKey.T:
                    Console.Write("\nServer or Client: [S/C]: default Server:\t");
                    switch (Console.ReadKey().Key)
                    {
                        default:
                        case ConsoleKey.S:
                            Console.WriteLine("\n TCP-Server");
                            foreach (int TcpPortPool in Global.ports.Skip(1))
                            {
                                Task.Run(() =>
                                {
                                    Tcp.Servers.Add(Tcp.Server.StartServer(Global.LocalIP, TcpPortPool));
                                    var Server = Tcp.Servers.Last();
                                    Task.Run(() =>
                                    {
                                        var TcpPort = TcpPortPool;
                                        while (true)
                                        {
                                            Console.WriteLine();
                                            try
                                            {
                                                var recieved = Tcp.RecieveSocketMessage(Server).Result;
                                                if (recieved is string)
                                                {
                                                    Console.WriteLine("Output: {0}", recieved.TrimEnd('\0'), String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                                                }
                                                else
                                                {
                                                    if (Server.State != WebSocketState.Connecting || Server.State != WebSocketState.Open)
                                                    {
                                                        throw new Exception("Closed");
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                for (int i = Global.ports[0] + 1; ; i++)
                                                {
                                                    if (Tools.PortInUse(i) == false)
                                                    {
                                                        Global.ports[Array.IndexOf(Global.ports, TcpPort)] = i;
                                                        TcpPort = i;
                                                        break;
                                                    }
                                                }
                                                try
                                                {
                                                    Server = Tcp.Server.StartServer(Global.LocalIP, TcpPort);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex);
                                                }
                                            }
                                        }
                                    });
                                    Task.Run(() =>
                                    {
                                        while (true)
                                        {
                                            Tcp.SendSocketMessage(Server, Global.LocalHostname + "_" + DateTime.Now.ToString("HH:mm:ss"));
                                            Thread.Sleep(1000);
                                        }
                                    });
                                });
                            }
                            break;
                        case ConsoleKey.C:
                            Console.WriteLine("\n TCP-Client");
                            break;
                    }
                    break;
            }
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D:
                        System.Diagnostics.Debugger.Break();
                        break;
                    case ConsoleKey.H:
                        foreach (var i in Udp.KnownServers.Servers)
                        {
                            Console.WriteLine(i + "\n");
                        }
                        Console.WriteLine("-----------");
                        break;
                    case ConsoleKey.T:
                        string[] tmp = new string[Global.ValidTokens.Count];
                        Global.ValidTokens.CopyTo(tmp);
                        Console.WriteLine("Valid tokens:");
                        foreach (string token in tmp)
                        {
                            Console.WriteLine(" {0} - {1}", token.Substring(0, 8), token.Substring(8));
                        }
                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        Thread.Sleep(5);
                        break;
                }
            }
        }
    }
}