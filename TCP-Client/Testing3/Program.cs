using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
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

    static ClientWebSocket TryEstablishConnection(ref dynamic Connection, IPAddress ip, int port)
    {
        Task<bool> Connected;
        Connection.Options.SetBuffer(8192, 8192);
        Connected = EstablishConnection(Connection, ip, port);
        while (Connected.Result == false)
        {
            Connection = new ClientWebSocket();
            Connection.Options.SetBuffer(8192, 8192);
            Connected = EstablishConnection(Connection, ip, port);
        }
        return Connection;
    }
    public static async Task<bool> EstablishConnection(dynamic Connection, IPAddress ip, int port)
    {
        try
        {
            await Connection.ConnectAsync(new Uri("ws://" + ip + ":" + port), CancellationToken.None);
            Console.WriteLine("\nServer reached!");
        }
        catch
        {
            Console.WriteLine("Cannot reach destination server!");
            Thread.Sleep(500);
            return false;
        }
        return true;
    }
    public static async Task<dynamic> RecieveSocketMessage(dynamic Connection)
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
                throw new Exception("Closed WebSocket");
            }
        }
        catch
        {
            Console.WriteLine("Cannot read message!");
            Thread.Sleep(5);
            return false;
        }
        Thread.Sleep(5);
        return Encoding.UTF8.GetString(bytes);
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
    static void Main()
    {
        string input;
        IPAddress ip = IPAddress.Parse("10.0.0.111");
        int port = 11002;
        dynamic Connection = new ClientWebSocket();

        Console.WriteLine("Looking for server on {0}:{1}, Waiting for a connection...", ip, port);
        TryEstablishConnection(ref Connection, ip, port);
        SendSocketMessage(Connection, "Test-Client");

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            try
            {
                Console.WriteLine("END");
                //Connection.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch { }
        };

        Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    var recieved = RecieveSocketMessage(Connection).Result;
                    if (recieved is string)
                    {
                        Console.WriteLine("Output: {0}", recieved.TrimEnd('\0'), String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                    }
                    else
                    {
                        if (Connection.State != WebSocketState.Connecting || Connection.State != WebSocketState.Open)
                        {
                            Connection = new ClientWebSocket();
                            TryEstablishConnection(ref Connection, ip, port);
                        }
                    }
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