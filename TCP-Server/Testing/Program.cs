using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

class Server
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
    public static WebSocket TryStartServer(IPAddress ip, int port)
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
                return Connection;
            }
        }
    }
    public static async void SendSocketMessage(dynamic Connection, string input)
    {
        try
        {
            if (Connection.State == WebSocketState.Open || Connection.State == WebSocketState.CloseSent)
            {
                await Connection.SendAsync(Encoding.UTF8.GetBytes(input), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            else
            {
                throw new Exception("Other side closed connection.");
            }
        }
        catch
        {
            Console.WriteLine("Cannot send message!");
        }
        Thread.Sleep(5);
    }
    public static async Task<string> RecieveSocketMessage(dynamic Connection)
    {
        byte[] bytes = new byte[8192];
        try
        {
            if (Connection.State == WebSocketState.Open || Connection.State == WebSocketState.CloseSent)
            {
                var result = await Connection.ReceiveAsync(bytes, CancellationToken.None);
            }
            else
            {
                Console.WriteLine("ERRRROR");
                throw new Exception("My - Closed socket");
            }
        }
        catch
        {
            Console.WriteLine("Cannot read message!");
            Thread.Sleep(5);
            throw new Exception("My - Fucked Up!!!");
            //return String.Empty;
        }
        Thread.Sleep(5);
        return Encoding.UTF8.GetString(bytes);
    }
    public static void Main()
    {
        string input;
        IPAddress ip = IPAddress.Parse(IPAddress.Any.ToString());
        int port = 11002;
        WebSocket Connection1;
        Task.Run(() =>
        {
            Connection1 = TryStartServer(ip, 11001);
            SendSocketMessage(Connection1, "Test-Server1");
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var recieved = RecieveSocketMessage(Connection1).Result;
                        Console.WriteLine("Output1: {0}", recieved.TrimEnd('\0'), String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                    }
                    catch
                    {

                    }
                }
            });
        });
        WebSocket Connection = TryStartServer(ip, port);
        SendSocketMessage(Connection, "Test-Server");

        try
        {
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Console.WriteLine("END");
                //Connection.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            };
        }
        catch { }

        Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    var recieved = RecieveSocketMessage(Connection).Result;
                    Console.WriteLine("Output: {0}", recieved.TrimEnd('\0'), String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                }
                catch
                {

                }
            }
        });

        while (true)
        {
            Console.WriteLine("\nEnter your message:");
            input = ReadLine();
            SendSocketMessage(Connection, input);
        }
    }
}