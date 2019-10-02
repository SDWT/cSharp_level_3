using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class SynchronizationTests
    {
        private readonly static List<string> _Messages = new List<string>();
        public static void Start()
        {
            var threads = new Thread[10];

            for (int i = 0; i < threads.Length; i++)
            {
                //threads[i] = new Thread(() => Printer($"Message {i}")); // Замыкание

                var i0 = i;
                threads[i] = new Thread(() => Printer($"Message {i0}")); // Замыкание
            }

            Array.ForEach(threads, thread => thread.Start());

        }

        private static readonly object __SyncRoot = new object();

        private static void Printer(string message)
        {
            ThreadTest.CheckThread();

            for (int i = 0; i < 20; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write("id:{0}", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("- msg ({0})", i);
                    Console.WriteLine("\"{0}\"", message);
                    _Messages.Add(message);
                }
                Thread.Sleep(100);
            }
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);

        }
    }

    internal class Logger
    {
        private string _FilePath;

        public Logger(string FilePath) => _FilePath = FilePath;

        public void Log(string Message)
        {
            File.AppendAllText(_FilePath, Message);
        }
    }
}
