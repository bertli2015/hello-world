using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HostIP
{
    public class IPUtils
    {
        public static string GetIPv4()
        {
            var ipA = FindLanAddress();
            if (ipA != null)
            {
                return ipA.ToString();
            }
            return null;
        }

        public static string GetIpv4New()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return localIP;
        }
        //http://stackoverflow.com/questions/1069103/how-to-get-the-ip-address-of-the-server-on-which-my-c-sharp-application-is-runni
        private static IPAddress FindLanAddress()
        {
            IPAddress gateway = FindGetGatewayAddress();
            if (gateway == null)
                return null;

            IPAddress[] pIPAddress = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in pIPAddress)
                if (IsAddressOfGateway(address, gateway))
                    return address;
            return null;
        }

        static bool IsAddressOfGateway(IPAddress address, IPAddress gateway)
        {
            if (address != null && gateway != null)
                return IsAddressOfGateway(address.GetAddressBytes(), gateway.GetAddressBytes());
            return false;
        }

        static bool IsAddressOfGateway(byte[] address, byte[] gateway)
        {
            if (address != null && gateway != null)
            {
                int gwLen = gateway.Length;
                if (gwLen > 0)
                {
                    if (address.Length == gateway.Length)
                    {
                        --gwLen;
                        int counter = 0;
                        for (int i = 0; i < gwLen; i++)
                        {
                            if (address[i] == gateway[i])
                                ++counter;
                        }
                        return (counter == gwLen);
                    }
                }
            }
            return false;

        }

        static IPAddress FindGetGatewayAddress()
        {
            //IPGlobalProperties ipGlobProps = IPGlobalProperties.GetIPGlobalProperties();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties ipInfProps = ni.GetIPProperties();
                foreach (GatewayIPAddressInformation gi in ipInfProps.GatewayAddresses)
                    return gi.Address;
            }
            return null;
        }
    }
}
