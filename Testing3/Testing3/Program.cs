using System;
using System.Net;
using System.Net.Sockets;
using static System.Net.Sockets.Socket;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;

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

        static ClientWebSocket TryEstablishConnection(ref ClientWebSocket Connection)
        {
            Task<bool> Connected;
            Connection.Options.SetBuffer(8192, 8192);
            Connected = EstablishConnection(Connection);
            while (Connected.Result == false)
            {
                Connection = new ClientWebSocket();
                Connection.Options.SetBuffer(8192, 8192);
                Connected = EstablishConnection(Connection);
            }
            return Connection;
        }
        private static byte[] GenerateRandomData()
        {
            byte[] data = new byte[8192];
            using (var gen = RandomNumberGenerator.Create())
                gen.GetBytes(data);

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
        static void Main()
        {
            string input;
            var rand = new Random();
            Console.WriteLine("{0}-{1}-|-{2}_#_{3}", Guid.NewGuid(), new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString("x"), rand.Next(1, int.MaxValue).ToString("x"), String.Join("", GenerateRandomData()));

            List<string> sharedToken = new List<string>(); 
            string EpochSeed = BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + Hash(BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2") + BitConverter.ToInt32(GenerateRandomData(), 0).ToString("x2"));

            ClientWebSocket Connection = new ClientWebSocket();
            TryEstablishConnection(ref Connection);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Connection.CloseAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None); ;

            /*byte[] bytes = new byte[8192];
            Task<byte[]>.Run(() =>
            {
                while (true)
                {
                    Task<WebSocketReceiveResult> recvBuffer;
                    try
                    {
                        recvBuffer = Socket.ReceiveAsync(bytes, CancellationToken.None);
                        Console.WriteLine(recvBuffer.Result);
                    }
                    catch { }
                }
            });*/

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var recieved = RecieveSocketMessage(Connection).Result;
                        Console.WriteLine("Output: {0}\nRaw: {1}", recieved, String.Join(" ", Encoding.UTF8.GetBytes(recieved)));
                    }
                    catch { }
                }
            });

            while (true)
            {
                Console.WriteLine("\nEnter your message:");
                input = ReadLine();
                SendSocketMessage(Connection, input);
            }
        }
        public static async Task<string> RecieveSocketMessage(ClientWebSocket Connection)
        {
            byte[] bytes = new byte[8192];
            try
            {
                await Connection.ReceiveAsync(bytes, CancellationToken.None);
            }
            catch
            {
                Console.WriteLine("Cannot read message!");
                Connection = new ClientWebSocket();
                Main();
                Thread.Sleep(5);
                return null;
            }
            Thread.Sleep(5);
            return Encoding.UTF8.GetString(bytes);
        }
        public static async Task<bool> EstablishConnection(ClientWebSocket Connection)
        {
            try
            {
                await Connection.ConnectAsync(new Uri("ws://10.0.0.64:11002"), CancellationToken.None);
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
        public static async void SendSocketMessage(ClientWebSocket Connection, string input)
        {
            //try
            //{
            await Connection.SendAsync(Encoding.UTF8.GetBytes(input), WebSocketMessageType.Text, false, CancellationToken.None);
            /*}
            catch
            {
                Console.WriteLine("Cannot send message!");
            }*/
            Thread.Sleep(5);
        }
    }
}
