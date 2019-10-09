using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class HomeWork6
    {
        public static void Start()
        {
            bool IsWork = true;
            Help();
            while (IsWork)
            {
                var command = Console.ReadLine();

                int num, cntThreads;
                bool Conditions = false;
                string path;
                switch (command.ToLower())
                {
                    case "1":
                        int[,] A = new int[,]
                        {
                            { 2, 5 },
                            { 7, 11 }
                        };
                        int[,] B = new int[,]
                        {
                            { 1, 0 },
                            { 0, 1 }
                        };

                        //var C = Multiply(A, B);

                        //foreach (var item in C)
                        //{
                        //    Console.Write($"{item} ");
                        //}
                        //Console.WriteLine();
                        break;
                    case "2":
                        string sourceDirectoryPath;
                        DirectoryInfo dirInfo;
                        Console.WriteLine("Enter source file name");
                        do
                        {
                            Conditions = false;
                            sourceDirectoryPath = Console.ReadLine();
                            if (!Directory.Exists(sourceDirectoryPath))
                            {
                                Console.WriteLine("Directory does not exisit");
                                Conditions = true;
                            }
                        } while (Conditions);

                        dirInfo = new DirectoryInfo(sourceDirectoryPath);

                        DirStart(sourceDirectoryPath);
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
            Console.WriteLine("1 - Matrix parallel async summary;");
            Console.WriteLine("2 - Dirrectories with files...");
            Console.WriteLine("help - this message;");
            Console.WriteLine("exit - close program.");
        }

        //static async int[,] Multiply(int[,] First, int[,] Second)
        //{
        //    if (First.Rank != 2 || Second.Rank != 2)
        //        throw new ArgumentException();

        //    int CntRowsA = First.GetLength(0);
        //    int CntColomnsB = Second.GetLength(1);

        //    if (First.GetLength(1) != Second.GetLength(0))
        //    {
        //        throw new ArgumentException("Rows count of first Matrix must equal colomn count of secon matrix");
        //    }

        //    var length = First.GetLength(1);

        //    var messages = Enumerable.Range(1, 100).Select(i => $"Message {i}").ToArray();

        //    var processing_tasks = new List<Task<int>>();

        //    for (int i = 0; i < CntRowsA; i++)
        //        for (int j = 0; j < CntColomnsB; j++)
        //        {

        //            processing_tasks.Add(MultiplyRowColomn());
        //        }


        //    Console.WriteLine("Все задачи сформированы. Ждём их завершения");
        //    //Task.WaitAll(processing_tasks.ToArray());
        //    var awaiting_all_task = Task.WhenAll(processing_tasks);
        //    await awaiting_all_task;

        //    //awaiting_all_task.Result;

        //    Console.WriteLine("Все задачи завершились.");

        //    return First;
        //}

        //static async Task<int> MultiplyElements(int[,] A, int[,] B, int Row, int Colomn)
        //{


        //}

        static void DirStart(string DirectoryPath)
        {
            var dirInfo = new DirectoryInfo(DirectoryPath);
            var Files = dirInfo.GetFiles();
            var outPath = Path.Combine(DirectoryPath, "result.data");

            var processing_tasks = new List<Task<int>>();


            try
            {
                Parallel.ForEach(Files, (file) => DirAction(file.FullName, outPath));
            }
            catch (AggregateException ae)
            {
                foreach (var exception in ae.InnerExceptions)
                {
                    DirEnd(outPath, exception.Message);
                }
            }
            



            //foreach (var file in Files)
            //{
            //    await DirAction(file.FullName, Path.Combine(DirectoryPath, outPath));
            //}
        }

        private static void DirAction(string FilePath, string outPath)
        {
            double result = 0, first, second;

            var lines = File.ReadAllLines(FilePath);

            var data = lines.First().Trim().Split(' ');

            double.TryParse(data[1], out first);
            double.TryParse(data[2], out second);


            switch (data[0])
            {
                case "1":
                    result = first * second;
                    break;
                case "2":
                    if (second == 0)
                        throw new DivideByZeroException($"Error: In {FilePath} divide by zero error.");
                    result = first / second;
                    break;
            }

            DirEnd(outPath, $"File: {FilePath} Result: {result}");
        }

        private static readonly object __SyncRoot = new object();

        static void DirEnd(string outPath, string msg)
        {
            lock (__SyncRoot)
            {
                File.AppendAllText(outPath, msg);
                Console.WriteLine(msg);
            }
        }
    }
}
