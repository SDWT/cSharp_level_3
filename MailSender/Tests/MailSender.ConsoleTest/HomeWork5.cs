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
        private static long[] _Buf;
        private static int _Count;
        private static int _Value;

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
                        int num, cntTreads;
                        bool Conditions = true;
                        Console.WriteLine("Enter Number (Number >= 1)");
                        while (!(int.TryParse(Console.ReadLine(), out num)) && Conditions)
                        {
                            Conditions = true;
                            if (num < 1)
                            {
                                Conditions = false;
                                Console.WriteLine("Number less 1");
                            }
                            else
                                Console.WriteLine("It's not a number");
                        }

                        Console.WriteLine("Enter threads count (count >= 1)");
                        while (!(int.TryParse(Console.ReadLine(), out cntTreads)) && Conditions)
                        {
                            Conditions = true;
                            if (cntTreads < 1)
                            {
                                Conditions = false;
                                Console.WriteLine("Number less 1");
                            }
                            else
                                Console.WriteLine("It's not a number");
                        }

                        Factorial(num, cntTreads);
                        break;
                    case "2":
                        LoadBarStart();
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


        public static void LoadBarStart()
        {
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;

            char chr = ' ';
            _Value = 0;
            Console.WriteLine($"[{chr, 10}]");
        }
        public static void LoadBarValue()
        {

        }




        static void Factorial(int Number, int CntThreads)
        {
            if (Number <= 1)
            {
                _Buf = new long[1];
                _Buf[0] = 1;
                Console.WriteLine($"Factorial Result: 1");
                return;
            }
            _Buf = new long[CntThreads];


            //var semaphore = new Semaphore((int)CntThreads, 1);
            _Count = CntThreads;
            var progressBar = new ConsoleProgressBar(_Count);

            for (int i = 0; i < CntThreads; i++)
            {
                int i0 = i;
                ThreadPool.QueueUserWorkItem(o => Factorial(Number - i0, CntThreads, i0));
            }

            //semaphore.WaitOne();

            while (_Count > 0)
            {
                progressBar.Value = CntThreads - _Count;
                continue;
            }
            progressBar.Value = CntThreads;

            long result = 1;
            for (int i = 0; i < CntThreads; i++)
            {
                result = result * _Buf[i];
            }
            _Buf[0] = result;
            Console.WriteLine($"Factorial Result: {result}");
        }

        static void Factorial(int Number, int CntThreads, int Step)
        {
            long result = 1;
            for (int i = Number; i > 1; i -= CntThreads)
            {
                result = result * i;
                Thread.Sleep(2000);
            }

            _Buf[Step] = result;
            _Count--;
            //Console.WriteLine($"Offset: {Step}, Step: {_Count}, Result: {result}");
            //semaphore.Release();
        }

        //static ulong Factorial(ushort Number, int level)
        //{
        //    if (Number <= 1)
        //        return 1;

        //    var manual_event_right = new ManualResetEvent(false);
        //    var manual_event_left = new ManualResetEvent(false);

        //    Factorial((ushort)(Number - 1), 2 * level, manual_event_left);
        //    Factorial((ushort)(Number - 2), 2 * level + 1, manual_event_right);

        //    ulong result = Number;
        //    manual_event_left.WaitOne();
        //    result *= _Buf[2 * level];
        //    manual_event_right.WaitOne();
        //    result *= _Buf[2 * level + 1];

        //    return result;
        //}

        //static ulong Factorial(ushort Number, int level, ManualResetEvent ParrentEvent)
        //{
        //    if (Number <= 1)
        //        return 1;

        //    var manual_event_right = new ManualResetEvent(false);
        //    var manual_event_left = new ManualResetEvent(false);

        //    Factorial((ushort)(Number - 1), 2 * level, manual_event_left);
        //    Factorial((ushort)(Number - 2), 2 * level + 1, manual_event_right);

        //    ulong result = Number;
        //    manual_event_left.WaitOne();
        //    result *= _Buf[2 * level];
        //    manual_event_right.WaitOne();
        //    result *= _Buf[2 * level + 1];

        //    return 0;
        //}
    }
}
