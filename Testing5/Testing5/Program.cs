using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.IO;


namespace PingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostname = Dns.GetHostName();
            int timeout = 10000;
            Ping ping = new Ping();
            Console.WriteLine(Dns.GetHostName());
            PingReply pingreply;
            try
            {
                pingreply = ping.Send(hostname, timeout);
                if (pingreply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Address: {0}", pingreply.Address);
                    Console.WriteLine("status: {0}", pingreply.Status);
                    Console.WriteLine("Round trip time: {0}", pingreply.RoundtripTime);
                }
            }
            catch (PingException ex)
            {
                Console.WriteLine(ex);
            }
            var tmp = GetLocalIPAddress();
            foreach (string i in tmp)
            {
                Console.WriteLine();
                Console.WriteLine("IPv4 Adress:\t\t" + i);
                Console.WriteLine("Subnet Mask:\t\t" + GetSubnetMask(IPAddress.Parse(i)));
                Console.WriteLine("Default Gateway:\t" + GetDefaultGateway());
            }

            //Find used ip adress by one known host in subnet
            Console.WriteLine("\nIP adress with reach for rpimaster:\n");
            using (var s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                s.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));
                try
                {
                    s.Connect("rpimaster", 0);
                }
                catch { }
                var ipaddr = s.LocalEndPoint as System.Net.IPEndPoint;
                var addr = ipaddr?.Address.ToString();
                Console.WriteLine(addr);
            }

            //Find all ips with configured gateway
            Console.WriteLine("\nIPs with gateway adress:\n");
            NetworkInterface[] allNICs = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var nic in allNICs)
            {
                var ipProp = nic.GetIPProperties();
                var gwAddresses = ipProp.GatewayAddresses;
                if (gwAddresses.Count > 0 &&
                    gwAddresses.FirstOrDefault(g => g.Address.AddressFamily == AddressFamily.InterNetwork) != null)
                {
                    var localIP = ipProp.UnicastAddresses.First(d => d.Address.AddressFamily == AddressFamily.InterNetwork).Address;
                    Console.WriteLine(localIP.ToString());
                }
            }

            Console.WriteLine("\nPrimary IP adress:\n");
            Console.WriteLine(GetDefaultIPV4Address(GetDefaultInterface()));

            Console.WriteLine("Port: 81 status: " + (PortInUse(81) ? "in use" : "not in use"));

            Console.ReadKey();
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

        public static ArrayList GetLocalIPAddress()
        {
            ArrayList result = new ArrayList();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    result.Add(ip.ToString());
                }
            }
            return result;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static IPAddress GetSubnetMask(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException(string.Format("Can't find subnetmask for IP address '{0}'", address));
        }
        public static IPAddress? GetDefaultGateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties().GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault();
        }

        //Best!!!!
        public static NetworkInterface? GetDefaultInterface()
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
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
                if ((gateways == null) || (gateways.Count == 0))
                {
                    continue;
                }
                var addresses = properties.UnicastAddresses;
                if ((addresses == null) || (addresses.Count == 0))
                {
                    continue;
                }
                return intf;
            }
            return null;
        }

        public static IPAddress? GetDefaultIPV4Address(NetworkInterface? intf)
        {
            if (intf == null)
            {
                return null;
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
                if (address.Address.ToString() == "192.168.42.75"){
                    continue;
                }
                return address.Address;
            }
            return null;
        }
    }
}
