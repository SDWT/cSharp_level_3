using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.Threading;

namespace MailSender.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest.Start();
            //SynchronizationTests.Start();
            ThreadPoolTests.Start();

            Console.ReadLine();
        }

    }
}
