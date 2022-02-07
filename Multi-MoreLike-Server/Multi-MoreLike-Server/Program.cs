﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA2211 // Nekonstantní pole nemají být viditelná.
#pragma warning disable CA1845 // Použít řetězec založený na rozsahu string.Concat
#pragma warning disable IDE0090 // Použít new(...)
#pragma warning disable IDE0056 // Použít operátor indexu
#pragma warning disable IDE0057 // Použít operátor rozsahu

namespace Application
{
    public class Global
    {
        public static int MaxClients = 3;
        public const int DefaultBroadcastPort = 11000;
        public static List<string> ValidTokens = new List<string>();
        public static List<Udp.BroadcastMessage> SendMessages = new List<Udp.BroadcastMessage>();
        public static List<Udp.BroadcastMessage> RecieveMessages = new List<Udp.BroadcastMessage>();
        public static string LocalHostname = Dns.GetHostName();
        public static IPAddress LocalIP = Tools.GetDefaultIPv4Address();
        public static List<Port> Ports = new List<Port>();
        public static List<MergeSort.ToSort> Works = new List<MergeSort.ToSort>();
        public static List<MergeSort.ToSort> JobsDone = new List<MergeSort.ToSort>();
        public class Port
        {
            private int number = 0;
            private bool promiss = false;
            private bool active = false;
            public int Number { get => number; set => number = value; }
            public bool Promiss { get => promiss; set => promiss = value; }
            public bool Active { get => active; set => active = value; }
            public Port(int PortNumber = 0, bool isPromiss = false, bool isActive = false)
            {
                Number = PortNumber;
                Promiss = isPromiss;
                Active = isActive;
            }
        }
        public static int[] ToBroadCast()
        {
            int[] result = new int[4];
            foreach (var i in Ports.Where(port => port.Active || port.Promiss))
            {
                result[0] = i.Number;
            }
            return result;
        }
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
            private int[] availablePorts = new int[3] { 0, 0, 0 };
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
                    "  \"AvailablePorts\": \"[");
                    foreach (var i in AvailablePorts)
                    {
                        result = String.Concat(result, i + " ");
                    }
                    result = String.Concat(result, "\b]\", \n" +
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
            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, Global.DefaultBroadcastPort);
            UdpClient listener = new UdpClient(EndPoint);
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
                                if (KnownServers.Servers.First(host => host.Hostname == tmp.Hostname).Tokens.Count > 8)
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
        public static void SendBroadcastMessage(ref Broadcast Connection, BroadcastMessage Message)
        {
            try
            {
                Connection.Socket.SendTo(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(Message)), Connection.EndPoint);
            }
            catch
            {
                Console.WriteLine("Unable to send broadcast message.");
            }
        }

        public static Task StartSender(bool DEBUG = false)
        {
            var Broadcast = new Broadcast(IPAddress.Parse(string.Concat(string.Join(".", IPAddress.Parse(Global.LocalIP.ToString()).GetAddressBytes().Take(3).ToArray()), ".255")), new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true }, Global.Ports[0].Number);
            Global.Ports[0].Active = true;
            if (DEBUG == true)
            {
                Console.WriteLine("This PC:\t" + Dns.GetHostName());
            }

            //First message
            //Tools.CheckPorts(ref ports, Broadcast.Port);
            Global.SendMessages.Add(new BroadcastMessage
            {
                Hostname = Global.LocalHostname,
                IpAddress = Global.LocalIP.ToString(),
                Token = Tools.NextToken(ref Global.ValidTokens),
                BirthMessage = true,
                AvailablePorts = Global.Ports.Skip(1).Where(port => port.Promiss == true).Select(port => port.Number).ToArray()
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
                    Hostname = Global.LocalHostname,
                    IpAddress = Global.LocalIP.ToString(),
                    Token = Tools.NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2].Substring(8),
                    TerminationMessage = true
                })), Broadcast.EndPoint);
            };
            Thread.Sleep(2000);

            while (true)
            {
                //Tools.CheckPorts(ref ports, Broadcast.Port);
                Global.SendMessages.Add(new BroadcastMessage
                {
                    Hostname = Global.LocalHostname,
                    IpAddress = Global.LocalIP.ToString(),
                    Token = Tools.NextToken(ref Global.ValidTokens),
                    LastToken = Global.ValidTokens[Global.ValidTokens.Count - 2].Substring(8),
                    AvailablePorts = Global.Ports.Skip(1).Where(port => port.Promiss == true).Select(port => port.Number).ToArray(),
                    Message = String.Empty
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
        public class HandShake
        {
            public string Token { get; set; }
            public HandShake(string token)
            {
                Token = token;
            }
        }
        public static async Task<dynamic> RecieveSocketMessage(dynamic Server)
        {
            byte[] bytes = new byte[8192];
            try
            {
                if (Server.State == WebSocketState.Open || Server.State == WebSocketState.CloseSent)
                {
                    /*string[] tokens = new string[Global.ValidTokens.Count];
                      Global.ValidTokens.CopyTo(tokens);
                      Console.WriteLine("\nValid tokens:");
                      foreach (string token in tokens)
                      {
                          Console.WriteLine(" {0} - {1}", token.Substring(0, 8), token.Substring(8));
                      }*/

                    await Server.ReceiveAsync(bytes, CancellationToken.None);
                    string result = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                    string[] tokens = new string[Global.ValidTokens.Count];
                    Global.ValidTokens.CopyTo(tokens);
                    //System.Diagnostics.Debugger.Break();
                    //Console.WriteLine(tokens.ElementAt(tokens.ToList().IndexOf(tokens.First(token => token.Substring(8) == result)) - 1).Substring(0, 8));
                    if (tokens.Select(token => token.Substring(8)).Contains(result))
                    {
                        return new HandShake(tokens.ElementAt(tokens.ToList().IndexOf(tokens.First(token => token.Substring(8) == result)) - 1).Substring(0, 8));
                    }
                    Thread.Sleep(5);
                    return result;
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
                Console.WriteLine("Cannot send message!");
            }
            Thread.Sleep(5);
        }
        public static dynamic InitListener(dynamic Server, [Optional] string NewTokenToCompare, [Optional] string OldTokenForHash)
        {
            var recieved = Tcp.RecieveSocketMessage(Server).Result;
            if (recieved is string)
            {
                if (recieved.StartsWith("$#T-"))
                {
                    var tmp = Tools.Hash(recieved.Substring(4) + OldTokenForHash);
                    if (tmp == NewTokenToCompare)
                    {
                        Console.WriteLine("HandShake Completed\nValid connection :)");
                        return true;
                    }
                    else
                    {
                        //TODO občas novější token než kontrolní, má být vždy handshake + starší 
                        //Když je with "xxxxxx" prostřední v validtokens, tak je validtoken ten obrácenej
                        Console.WriteLine("WRONG TOKEN!!!!!!!!!!!");
                        Server.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        return false;
                    }
                }
                if (recieved.StartsWith("$#W-"))
                {
                    //Console.WriteLine("Output1: {0}", recieved);
                    var work = Newtonsoft.Json.JsonConvert.DeserializeObject<MergeSort.ToSort>(recieved.Substring(4));
                    Task.Run(() =>
                    {
                        MergeSort.Sort(work.Numbers, 0, work.Numbers.Length - 1);
                        work.Sorted = true;
                        SendSocketMessage(Server, "$#J-" + Newtonsoft.Json.JsonConvert.SerializeObject(work));
                    });
                }
                if (recieved.StartsWith("$#J-"))
                {
                    //Console.WriteLine("Output2: {0}", recieved);
                    var work = Newtonsoft.Json.JsonConvert.DeserializeObject<MergeSort.ToSort>(recieved.Substring(4));
                    Global.JobsDone.Add(work);
                    Console.WriteLine("Sorted Numbers: ");
                    foreach (int i in work.Numbers)
                    {
                        Console.Write(i + " ");
                    }
                    Console.WriteLine();
                    System.Diagnostics.Debugger.Break();
                }
                //Upravit aby se odesílalo v nějakým spec tvaru těch 8charakterů, regex na detekci a upravit extra output: {0}
                Console.WriteLine("Output: {0}", recieved);
            }
            if (recieved is Tcp.HandShake)
            {
                //Auth token send back
                Tcp.SendSocketMessage(Server, recieved.Token);
                Task.Run(() =>
                {
                    Tcp.InitListener(Server);
                });
                Thread.Sleep(5000);
                while (true)
                {
                    Console.WriteLine("\nEnter your message:");
                    var input = Tools.ReadLine();
                    SendSocketMessage(Server, input);
                }
            }
            if (recieved is bool)
            {
                if (Server.State != WebSocketState.Connecting || Server.State != WebSocketState.Open)
                {
                    throw new Exception("Closed");
                }
            }
            return true;
        }

        public static List<WebSocket> Servers = new List<WebSocket>();
        public class Server
        {
            public static WebSocket StartServer(IPAddress ip, int port)
            {
                var server = new TcpListener(ip, port);
                server.Start();
                Global.Ports.First(portNumber => portNumber.Number == port).Promiss = true;
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
                        Global.Ports.First(portNumber => portNumber.Number == port).Promiss = false;
                        Global.Ports.First(portNumber => portNumber.Number == port).Active = true;
                        return Connection;
                    }
                }
            }
        }
        public class Client
        {
            public static ClientWebSocket TryEstablishConnection(ref dynamic Connection, IPAddress ip, int port, string token)
            {
                Task<bool> Connected;
                Connection.Options.SetBuffer(8192, 8192);
                Connected = EstablishConnection(Connection, ip, port, token);
                while (Connected.Result == false)
                {
                    Connection = new ClientWebSocket();
                    Connection.Options.SetBuffer(8192, 8192);
                    Connected = EstablishConnection(Connection, ip, port, token);
                }
                return Connection;
            }
            public static async Task<bool> EstablishConnection(dynamic Connection, IPAddress ip, int port, string token)
            {
                try
                {
                    await Connection.ConnectAsync(new Uri("ws://" + ip + ":" + port), CancellationToken.None);
                    Console.WriteLine("\nServer reached!");
                    Tcp.SendSocketMessage(Connection, token);
                    Console.WriteLine("\nToken send!");
                }
                catch
                {
                    Console.WriteLine("Cannot reach destination server!");
                    Thread.Sleep(500);
                    return false;
                }
                return true;
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
        public static void CheckPorts(ref List<Global.Port> Ports, [Optional] int BroadcastPort)
        {
            if (BroadcastPort < 1)
            {
                BroadcastPort = Global.DefaultBroadcastPort;
            }
            Ports.Add(new Global.Port(BroadcastPort));
            for (int i = 0; i < Global.MaxClients; i++)
            {
                Ports.Add(new Global.Port());
            }
            for (int i = BroadcastPort; true; i++)
            {
                if (PortInUse(i) == false)
                {
                    Ports[0].Number = i;
                    break;
                }
            }
            for (int i = 1; i < Ports.Count; i++)
            {
                for (int j = Ports[i - 1].Number + 1; true; j++)
                {
                    if (PortInUse(j) == false)
                    {
                        Ports[i].Number = j;
                        break;
                    }
                }
            }
        }
        public static IPAddress GetDefaultIPv4Address()
        {
            var Interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var Interface in Interfaces)
            {
                if (Interface.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }
                if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                var properties = Interface.GetIPProperties();
                if (Interface.GetIPProperties() == null)
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
                if (addresses[0].Address.AddressFamily != AddressFamily.InterNetwork)
                {
                    continue;
                }
                return Interface.GetIPProperties().UnicastAddresses[0].Address;
            }
            return IPAddress.Any;
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
        public static byte[] GenerateRandomData()
        {
            byte[] data = new byte[8192];
            using (var gen = RandomNumberGenerator.Create())
            {
                gen.GetBytes(data);
            }
            return data;
        }
        public static string Hash(string input)
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
    public class MergeSort
    {
        public class ToSort
        {
            public int[] Numbers { get; set; }
            public bool Sorted { get; set; }
            public ToSort(int[] numbers)
            {
                Numbers = numbers;
                Sorted = false;
            }
        }
        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
        public static void Merge(int[] arr, int l, int m, int r)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        // Main function that
        // sorts arr[l..r] using
        // merge()
        public static void Sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;

                // Sort first and
                // second halves
                Sort(arr, l, m);
                Sort(arr, m + 1, r);

                // Merge the sorted halves
                Merge(arr, l, m, r);
            }
        }
    }
    class Primary
    {
        static void Main()
        {
            //BEGIN Setup
            Global.LocalHostname = String.Concat(Global.LocalHostname + ":", BitConverter.ToInt32(Tools.GenerateRandomData(), 0).ToString("x2").Substring(0, 4));
            Tools.CheckPorts(ref Global.Ports, 11000);
            Info();
            //END Setup

            //BEGIN Server
            StartServers();
            //END Server

            //BEGIN Listener
            StartListener();
            //END Listener

            //BEGIN Controll
            Controll();
            //END Controll

            //END OF FILE
            Console.WriteLine("EOF");
        }
        public static void Info()
        {
            //Global.DefaultBroadcastPort = 11000;
            Console.WriteLine("This PC: [" + Global.LocalHostname + "]");
            Console.Write("Ports: [");
            foreach (int port in Global.Ports.Skip(1).Select(port => port.Number))
            {
                Console.Write(port + ", ");
            }
            Console.WriteLine("\b\b]");
        }
        public static void StartServers()
        {
            foreach (int TcpPortPool in Global.Ports.Skip(1).Select(port => port.Number))
            {
                Task.Run(() =>
                {
                    Tcp.Servers.Add(Tcp.Server.StartServer(Global.LocalIP, TcpPortPool));
                    Console.WriteLine("\nTCP-Server started");
                    var Server = Tcp.Servers.Last();
                    Task.Run(() =>
                    {
                        var TcpPort = TcpPortPool;
                        while (true)
                        {
                            Console.WriteLine();
                            try
                            {
                                Tcp.InitListener(Server);
                            }
                            catch
                            {
                                for (int i = Global.Ports.Select(port => port.Number).ToArray()[0] + 1; ; i++)
                                {
                                    if (Tools.PortInUse(i) == false)
                                    {
                                        Global.Ports[Array.IndexOf(Global.Ports.Select(port => port.Number).ToArray(), TcpPort)] = new Global.Port(i);
                                        TcpPort = i;
                                        break;
                                    }
                                }
                                try
                                {
                                    Server = Tcp.Server.StartServer(Global.LocalIP, TcpPort);
                                }
                                catch
                                {
                                }
                            }
                        }
                    });
                });
            }
            Task.Run(() =>
            {
                Udp.StartSender();
            });
            Console.WriteLine("\nUDP-Server started, Broadcasting now...");
        }
        public static void StartListener()
        {
            Task.Run(() =>
            {
                Udp.StartListener();
            });
        }
        public static void Sort()
        {
            int MaxNumbers = 30;
            int ShowNumbers = 10;
            int[] Numbers = new int[MaxNumbers];
            Random rand = new Random();
            for (int i = 0; i < Numbers.Length; i++)
            {
                Numbers[i] = rand.Next(0, 1000000);
            }

            Console.WriteLine("\nFirst {0} numbers:", ShowNumbers);
            for (int i = 0; i < Numbers.Length && i < ShowNumbers; i++)
            {
                Console.Write(Numbers[i] + " ");
            }
            Console.WriteLine("#");

            Global.Works.Add(new MergeSort.ToSort(Numbers));

            //MergeSort.Sort(Numbers, 0, Numbers.Length - 1);

            Console.WriteLine("\nFirst {0} sorted numbers:", ShowNumbers);
            for (int i = 0; i < Numbers.Length && i < ShowNumbers; i++)
            {
                Console.Write(Numbers[i] + " ");
            }
            Console.WriteLine("#");

            //System.Diagnostics.Debugger.Break();
        }
        public static void Controll()
        {
            Thread.Sleep(300);
            Console.WriteLine("Press [S] to start processing random numbers.");
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                        Sort();
                        break;
                    case ConsoleKey.D:
                        System.Diagnostics.Debugger.Break();
                        break;
                    case ConsoleKey.H:
                        Console.WriteLine();
                        foreach (var i in Udp.KnownServers.Servers)
                        {
                            Console.WriteLine(i + "\n");
                        }
                        Console.WriteLine("-----------");
                        break;
                    case ConsoleKey.T:
                        string[] tmp = new string[Global.ValidTokens.Count];
                        Global.ValidTokens.CopyTo(tmp);
                        Console.WriteLine("\nValid tokens:");
                        foreach (string token in tmp)
                        {
                            Console.WriteLine(" {0} - {1}", token.Substring(0, 8), token.Substring(8));
                        }
                        break;
                    case ConsoleKey.C:
                        Task.Run(() =>
                        {
                            while (true)
                            {
                                Console.WriteLine("Searching for server...");
                                if (Udp.KnownServers.Servers.Where(server => server.Alive == true && server.Hostname != Global.LocalHostname && server.ValidTokens.Count > 1).Any())
                                {
                                    var server = Udp.KnownServers.Servers.Where(server => server.Alive == true && server.Hostname != Global.LocalHostname && server.ValidTokens.Count > 1).OrderByDescending(server => server.AvailablePorts.Length).ToList().First();
                                    string[] Tokens = new string[server.ValidTokens.Count];
                                    server.ValidTokens.CopyTo(Tokens);
                                    Console.WriteLine("Connecting to: \"" + server.Hostname + "\" as \"" + server.LastIpAddress + "\" on \"" + server.AvailablePorts.First() + "\" with \"" + Tokens[Tokens.Length - 1] + "\" ...");
                                    dynamic Connection = new ClientWebSocket();
                                    Tcp.Client.TryEstablishConnection(ref Connection, IPAddress.Parse(server.LastIpAddress), server.AvailablePorts.First(), Tokens[Tokens.Length - 1]); var result = Tcp.InitListener(Connection, Tokens[Tokens.Length - 1], Tokens.ElementAt(Tokens.Length - 2));

                                    Console.WriteLine("ReadyToSendJob!");
                                    MergeSort.ToSort work = Global.Works.Last();
                                    Global.Works.RemoveAt(Global.Works.IndexOf(work));
                                    try
                                    {
                                        Tcp.SendSocketMessage(Connection, "$#W-" + Newtonsoft.Json.JsonConvert.SerializeObject(work));
                                        Console.WriteLine("JobOfferMayBeSend!");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Kur*!!§");
                                    }

                                    if (result == true)
                                    {
                                        Task.Run(() =>
                                        {
                                            while (true)
                                            {
                                                Console.WriteLine();
                                                try
                                                {
                                                    Tcp.InitListener(Connection);
                                                }
                                                catch
                                                {
                                                    break;
                                                    //Console.WriteLine("DISCONNECTED!!!!!!!!");
                                                }
                                            }
                                        });
                                    }
                                    //System.Diagnostics.Debugger.Break();
                                    break;
                                }
                                Thread.Sleep(5);
                            }
                        });
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