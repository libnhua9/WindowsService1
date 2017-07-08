using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {

            Service1 s = new Service1();
            s.onLoad();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

        }
      
    }
}
