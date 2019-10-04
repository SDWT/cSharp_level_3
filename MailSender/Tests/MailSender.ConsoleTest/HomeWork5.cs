using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    static class HomeWork5
    {
        private static readonly object __SyncRoot = new object();

        public static void Start()
        {
            bool IsWork = true;
            Help();
            while (IsWork)
            {
                var command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "1":
                        int num, cntThreads;
                        bool Conditions = true;
                        Console.WriteLine("Enter Number (1 <= Number <= 20)");
                        while (!(int.TryParse(Console.ReadLine(), out num)) && Conditions)
                        {
                            Conditions = true;
                            if (num < 1)
                            {
                                Conditions = false;
                                Console.WriteLine("Number less 1");
                            }
                            else if (num > 20)
                            {
                                Conditions = false;
                                Console.WriteLine("Number higher 20");
                            }
                            else
                                Console.WriteLine("It's not a number");
                        }

                        Console.WriteLine("Enter threads count (count >= 1)");
                        while (!(int.TryParse(Console.ReadLine(), out cntThreads)) && Conditions)
                        {
                            Conditions = true;
                            if (cntThreads < 1)
                            {
                                Conditions = false;
                                Console.WriteLine("Number less 1");
                            }
                            else
                                Console.WriteLine("It's not a number");
                        }

                        //Factorial(num, cntThreads);
                        ThreadPool.QueueUserWorkItem(o => Factorial(num, cntThreads));
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "help":
                        Help();
                        break;
                    case "exit":
                        IsWork = false;
                        break;
                    default:
                        Console.WriteLine("This command not aviable, please check.");
                        break;
                }
            }
        }

        public static void Help()
        {
            Console.WriteLine("To use this functions enter number and press \"Enter\" button on your keyboard.");
            Console.WriteLine("1 - Facktorial N;");
            Console.WriteLine("2 - Sum from 1 to N;");
            Console.WriteLine("3 - CSV-TXT converter;");
            Console.WriteLine("help - this message;");
            Console.WriteLine("exit - close program.");
        }

        static void Factorial(int Number, int CntThreads)
        {
            long[] buf;

            if (Number <= 1)
            {
                buf = new long[1];
                buf[0] = 1;
                Console.WriteLine($"Factorial Result: 1");
                return;
            }
            int count = new int();
            buf = new long[CntThreads];


            //var semaphore = new Semaphore((int)CntThreads, 1);
            count = CntThreads;
            var progressBar = new ConsoleProgressBar(count);

            for (int i = 0; i < CntThreads; i++)
            {
                int i0 = i;
                ThreadPool.QueueUserWorkItem(o => Factorial(Number - i0, CntThreads, i0,
                    ref count, ref buf));
            }

            //semaphore.WaitOne();

            while (count > 0)
            {
                progressBar.Value = CntThreads - count;
                continue;
            }
            progressBar.Value = CntThreads;

            long result = 1;
            for (int i = 0; i < CntThreads; i++)
            {
                result = result * buf[i];
            }
            buf[0] = result;
            Console.WriteLine($"Factorial Result: {result}");
        }

        private static void Factorial(int Number, int CntThreads, int Step, 
            ref int count, ref long[] buf)
        {
            long result = 1;
            for (int i = Number; i > 1; i -= CntThreads)
            {
                result = result * i;
                Thread.Sleep(2000);
            }

            buf[Step] = result;
            lock (__SyncRoot)
            {
                count--;
                //Console.WriteLine($"Offset: {Step}, Step: {count}, Result: {result}");
            }
            //semaphore.Release();
        }

        
    }
}
