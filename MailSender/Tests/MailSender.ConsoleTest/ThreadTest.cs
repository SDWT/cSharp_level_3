using System;

using System.Threading;

namespace MailSender.ConsoleTest
{
    class ThreadTest
    {
        public static void CheckThread()
        {
            var current_thread = Thread.CurrentThread;
            Console.WriteLine("Поток \"{0}\"(id:{1}) запущен.", current_thread.Name, current_thread.ManagedThreadId);
        }

        public static void Start()
        {
            //System.Threading.Thread
            Thread current_thread = Thread.CurrentThread;
            current_thread.Name = "Консольный поток";

            // Частичные инструменты управления потоками своими и чужими
            //System.Diagnostics.Process
            //Environment.ProcessorCount - кол-во ядер процессора

            CheckThread();


            //var clock_tread = new Thread(new ThreadStart(ClockUpdater));
            var clock_tread = new Thread(ClockUpdater);

            clock_tread.Priority = ThreadPriority.Highest;
            clock_tread.Name = "Поток часов";
            clock_tread.IsBackground = true;
            clock_tread.Start();



            var message = "Hello world!";
            //var printer_tread = new Thread(new ParameterizedThreadStart(CrazyPrinter));

            //var printer_tread = new Thread(CrazyPrinter);
            //printer_tread.Name = "Поток принтера";
            //printer_tread.IsBackground = true;
            //printer_tread.Start(message);


            //var printer2_tread = new Thread(() => CrazyPrinter(message));
            //printer2_tread.Name = "Поток принтера2";
            //printer2_tread.IsBackground = true;
            //printer2_tread.Start();

            var printer3 = new Printer3(message);
            var printer3_tread = new Thread(printer3.Print);
            printer3_tread.Name = "Поток принтера2";
            printer3_tread.IsBackground = true;
            printer3_tread.Start();

            //ClockUpdater();
            Console.ReadLine();
            // Консоль не закроется, пока все потоки не будут закрыты, к примеру
            // Не безопасно
            //clock_tread.Abort();



            // Оба плохи
            //clock_tread.Interrupt(); // Мягкое прерывание - Исключинтельно в sleep
            //clock_tread.Abort(); // Жёсткое прерывания - битые данные


            //clock_tread.Join();
            _IsClockEnable = false;
            if (!clock_tread.Join(200))
                clock_tread.Interrupt();
        }

        private static bool _IsClockEnable = true;

        private static void ClockUpdater()
        {
            try
            {
                CheckThread();
                while (_IsClockEnable)
                {

                    Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);


        }

        private static void CrazyPrinter(object obj)
        {
            CheckThread();

            var message = (string)obj;

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("id:{0} - msg \"{1}\"", Thread.CurrentThread.ManagedThreadId, message);
            }
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);
        }

        private static void CrazyPrinter(string message)
        {
            CheckThread();

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("id:{0} - msg \"{1}\"", Thread.CurrentThread.ManagedThreadId, message);
            }
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);
        }
    }

    internal class Printer3
    {
        private string _Message;

        public Printer3(string Message) => _Message = Message;

        public void Print()
        {
            ThreadTest.CheckThread();

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("id:{0} - msg \"{1}\"", Thread.CurrentThread.ManagedThreadId, _Message);
            }
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
