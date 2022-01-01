using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TraderMadeWebSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Any, 9050);
            Socket WinSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WinSocket.Bind(ServerEndPoint);

            int PORT = 9876;
            UdpClient udpClient = new UdpClient();
            //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
            var from = new IPEndPoint(0, 0);

            Console.Write("Waiting for client");
            while (true)
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)(sender);
                byte[] data = new byte[1024];
                int recv = WinSocket.ReceiveFrom(data, ref Remote);

                Console.WriteLine("Message received from {0}:", Remote.ToString());
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

                var recvBuffer = udpClient.Receive(ref from);
                Console.WriteLine(Encoding.UTF8.GetString(recvBuffer));
            }
        }
    }
}