using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class ThreadPoolTests
    {
        public static void Start()
        {
            var messages = Enumerable.Range(0, 100).Select(i => $"Message {i}").ToArray();

            //for (var i = 0; i < messages.Length; i++)
            //{
            //    var msg = messages[i];
            //    new Thread(() => ProcessMessage(msg)) { IsBackground = true }.Start();
            //}
            int max, min, maxAIO, minAIO;

            ThreadPool.GetMaxThreads(out max, out maxAIO);
            ThreadPool.GetMinThreads(out min, out minAIO);

            // Изменять макс. и мин. кол-во потоков не рекомендуется
            // 
            //ThreadPool.SetMinThreads(5, 5);
            //ThreadPool.SetMaxThreads(15, 15);

            foreach (var msg in messages)
            {

                //ThreadPool.QueueUserWorkItem(o => ProcessMessage(msg));
                ThreadPool.QueueUserWorkItem(ProcessPoolMessage, msg);
            }


        }

        private static readonly object __SyncRoot = new object();

        private static void ProcessPoolMessage(object Parameter)
        {
            ProcessMessage((string)Parameter);
        }

        private static void ProcessMessage(string message)
        {
            ThreadTest.CheckThread();

            for (int i = 0; i < 3; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write("id:{0}", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("- msg ({0})", i);
                    Console.WriteLine("\"{0}\"", message);
                }
                Thread.Sleep(200);
            }
            //Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
