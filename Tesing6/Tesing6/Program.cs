using System;
using System.Net;
using System.Net.Sockets;
using static System.Net.Sockets.Socket;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse("10.0.0.66"), 9050);
 //IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Broadcast, 9050);
 Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
 string welcome = "Hello, are you there?";
 byte[] data = Encoding.ASCII.GetBytes(welcome);
 while (true)
 {
     server.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
 }*/
namespace TraderMadeWebSocketTest
{
    class Program
    {
        public static string ReadLine()
        {
            var input = System.Console.ReadLine();
            if (input == null)
            {
                return String.Empty;
            }
            return input;
        }
        static void Main(string[] args)
        {
            string input;
            Task<bool> Connected;


            byte[] bytes = new byte[8192];
            Task<byte[]>.Run(() =>
            {
                while (true)
                {
                    Task<WebSocketReceiveResult> recvBuffer;
                    try
                    {
                    }
                    catch { }
                }
            });

            while (true)
            {
                Console.WriteLine("\nEnter your message:");
                input = ReadLine();
            }
        }
        public static async Task<bool> EstablishConnection(ClientWebSocket connection)
        {
            var tokenoff = new CancellationToken(true);
            CancellationTokenSource source = new CancellationTokenSource();
            try
            {
                await connection.ConnectAsync(new Uri("ws://10.0.0.64:81"), CancellationToken.None);
                Console.WriteLine("\nServer reached!");
            }
            catch
            {
                Console.WriteLine("Cannot reach destination server!");
                return false;
            }
            return true;
        }
        public static async void SendSocketMessage(ClientWebSocket Socket, string input)
        {
            try
            {
                await Socket.SendAsync(Encoding.UTF8.GetBytes(input), WebSocketMessageType.Text, false, CancellationToken.None);
            }
            catch
            {
                Console.WriteLine("Cannot send message!");
            }
            Thread.Sleep(5);
        }
    }
}
