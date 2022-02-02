using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine("Closed");
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
}