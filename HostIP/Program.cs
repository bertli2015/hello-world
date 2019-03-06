using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostIP
{
    /// <summary>
    /// 获取PC的IP地址Demo
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var ip = IPUtils.GetIPv4();
            Console.WriteLine("IP: " + ip);
            var ip2 = IPUtils.GetIpv4New();
            Console.WriteLine("IPNew: " + ip2);
            Console.ReadKey();
        }
    }
}
