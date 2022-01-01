using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TraderMadeWebSocketTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri("ws://10.0.0.255:81"), CancellationToken.None);
                ws.EnableBroadcast = true;
                byte[] buffer = new byte[256];
                while (ws.State == WebSocketState.Open)
                {
                    var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }
                    else
                    {
                        HandleMessage(buffer, result.Count);
                    }
                }
            }
        }

        private static void HandleMessage(byte[] buffer, int count)
        {
            Console.WriteLine($"Received {BitConverter.ToString(buffer, 0, count)}");
        }
    }
}