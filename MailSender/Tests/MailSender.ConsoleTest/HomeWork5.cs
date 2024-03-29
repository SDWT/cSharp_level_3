﻿using System;
using System.Collections.Generic;
using System.IO;
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

                int num, cntThreads;
                bool Conditions = false;
                switch (command.ToLower())
                {
                    case "1":
                        Console.WriteLine("Enter Number (1 <= Number <= 20)");
                        do
                        {
                            Conditions = !(int.TryParse(Console.ReadLine(), out num));
                            Conditions = false;
                            if (num < 1)
                            {
                                Conditions = true;
                                Console.WriteLine("Number less 1");
                            }
                            else if (num > 20)
                            {
                                Conditions = true;
                                Console.WriteLine("Number higher 20");
                            }
                            else if (Conditions)
                                Console.WriteLine("It's not a number");
                        } while (Conditions);

                        Console.WriteLine("Enter threads count (count >= 1)");
                        do
                        {
                            Conditions = !(int.TryParse(Console.ReadLine(), out cntThreads));
                            Conditions = false;
                            if (cntThreads < 1)
                            {
                                Conditions = true;
                                Console.WriteLine("Number less 1");
                            }
                            else if (Conditions)
                                Console.WriteLine("It's not a number");
                        } while (Conditions);

                        //Factorial(num, cntThreads);
                        ThreadPool.QueueUserWorkItem(o => Factorial(num, cntThreads));
                        break;
                    case "2":
                        Console.WriteLine("Enter Number (1 <= Number <= 1000000000)");
                        do
                        {
                            Conditions = !(int.TryParse(Console.ReadLine(), out num));
                            if (num < 1)
                            {
                                Conditions = true;
                                Console.WriteLine("Number less 1");
                            }
                            //else if (num > 20)
                            //{
                            //    Conditions = false;
                            //    Console.WriteLine("Number higher 20");
                            //}
                            else if (Conditions)
                                Console.WriteLine("It's not a number");
                        } while (Conditions);

                        Console.WriteLine("Enter threads count (count >= 1)");
                        do
                        {
                            Conditions = !(int.TryParse(Console.ReadLine(), out cntThreads));
                            if (cntThreads < 1)
                            {
                                Conditions = true;
                                Console.WriteLine("Number less 1");
                            }
                            else if (Conditions)
                                Console.WriteLine("It's not a number");
                        } while (Conditions);

                        //Factorial(num, cntThreads);
                        ThreadPool.QueueUserWorkItem(o => Summary(num, cntThreads));
                        break;
                    case "3":
                        var cCsvTxt = new ConverterCSVTXT();
                        string destinationFileName;
                        string sourceFileName;

                        Console.WriteLine("Enter source file name");
                        do
                        {
                            Conditions = false;
                            sourceFileName = Console.ReadLine();
                            if (!File.Exists(sourceFileName))
                            {
                                Console.WriteLine("File does not exisit");
                                Conditions = true;
                            }
                        } while (Conditions);

                        Console.WriteLine("Enter destination file name");
                        do
                        {
                            Conditions = false;
                            destinationFileName = Console.ReadLine();
                            if (!File.Exists(destinationFileName))
                                destinationFileName = $"{sourceFileName}.txt";
                        } while (Conditions);

                        cCsvTxt.ConvertCsv2Txt(sourceFileName, destinationFileName);
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
                //Thread.Sleep(2000);
            }

            buf[Step] = result;
            lock (__SyncRoot)
            {
                count--;
                //Console.WriteLine($"Offset: {Step}, Step: {count}, Result: {result}");
            }
            //semaphore.Release();
        }

        static void Summary(int Number, int CntThreads)
        {
            long[] buf;

            if (Number < 1)
            {
                buf = new long[1];
                buf[0] = 0;
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
                ThreadPool.QueueUserWorkItem(o => Summary(Number - i0, CntThreads, i0,
                    ref count, ref buf));
            }

            //semaphore.WaitOne();

            while (count > 0)
            {
                progressBar.Value = CntThreads - count;
                continue;
            }
            progressBar.Value = CntThreads;

            long result = 0;
            for (int i = 0; i < CntThreads; i++)
            {
                result = result + buf[i];
            }
            buf[0] = result;
            Console.WriteLine($"Summary Result: {result}");
        }

        private static void Summary(int Number, int CntThreads, int Step,
            ref int count, ref long[] buf)
        {
            long result = 0;
            for (int i = Number; i >= 1; i -= CntThreads)
            {
                result = result + i;
                //Thread.Sleep(2000);
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
