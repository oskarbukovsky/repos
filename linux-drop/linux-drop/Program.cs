static System.Net.IPAddress GetDefaultIPv4Address()
{
    var Interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
    foreach (var Interface in Interfaces)
    {
        if (Interface.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up)
        {
            continue;
        }
        if (Interface.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
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
        if (addresses[0].Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
        {
            continue;
        }
        return Interface.GetIPProperties().UnicastAddresses[0].Address;
    }
    return System.Net.IPAddress.Any;
}
Console.WriteLine(GetDefaultIPv4Address().ToString());
System.Diagnostics.Debugger.Break();