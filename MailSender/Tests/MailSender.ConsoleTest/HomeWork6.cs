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
                bool Conditions = false;
                int[,] first, second, third;
                List<string> lines;

                switch (command.ToLower())
                {
                    case "1":

                        
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

                        var act = new ActionDirectoryFiles(sourceDirectoryPath);
                        ThreadPool.QueueUserWorkItem(o => act.Start());
                        break;
                    case "3":
                        first = new int[,]
                        {
                            { 2, 5 },
                            { 7, 11 }
                        };
                        lines = Matrix2Lines(first);
                        foreach (var line in lines)
                        {
                            Console.WriteLine(line);
                        }
                        second = new int[,]
                        {
                            { 1, 2 },
                            { 3, 4 }
                        };
                        lines = Matrix2Lines(second);
                        foreach (var line in lines)
                        {
                            Console.WriteLine(line);
                        }

                        third = Multiply(first, second).GetAwaiter().GetResult();
                        lines = Matrix2Lines(third);

                        foreach (var line in lines)
                        {
                            Console.WriteLine(line);
                        }
                        Console.WriteLine();
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

        private static void Help()
        {
            Console.WriteLine("To use this functions enter number and press \"Enter\" button on your keyboard.");
            //Console.WriteLine("1 - Matrix parallel async summary;");
            Console.WriteLine("2 - Dirrectories with files...");
            Console.WriteLine("3 - Test matrix parallel async summary;");
            Console.WriteLine("help - this message;");
            Console.WriteLine("exit - close program.");
        }
        private static List<string> GetMatrix(int[,] matrix)
        {
            var lines = new List<string>();
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    strB.Append($"{matrix[i, j]}");
                }
                //strB.Append("\n");
                lines.Add(strB.ToString());
                strB.Clear();
            }
            return lines;
        }

        private static List<string> Matrix2Lines(int[,] matrix)
        {
            var lines = new List<string>();
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    strB.Append($"{matrix[i, j]}");
                }
                //strB.Append("\n");
                lines.Add(strB.ToString());
                strB.Clear();
            }
            return lines;
        }

        static async Task<int[,]> Multiply(int[,] First, int[,] Second)
        {
            if (First.Rank != 2 || Second.Rank != 2)
                throw new ArgumentException();

            int CntRowsA = First.GetLength(0);
            int CntColomnsB = Second.GetLength(1);

            if (First.GetLength(1) != Second.GetLength(0))
            {
                throw new ArgumentException("Rows count of first Matrix must equal colomn count of secon matrix");
            }

            var length = First.GetLength(1);

            var messages = Enumerable.Range(1, 100).Select(i => $"Message {i}").ToArray();

            var processing_tasks = new List<Task<int>>();

            for (int i = 0; i < CntRowsA; i++)
                for (int j = 0; j < CntColomnsB; j++)
                {
                    processing_tasks.Add(MultiplyRowColomn(First, Second, i, j));
                }


            Console.WriteLine("Все задачи сформированы. Ждём их завершения");
            //Task.WaitAll(processing_tasks.ToArray());
            var awaiting_all_task = Task.WhenAll(processing_tasks);
            await awaiting_all_task;

            int[,] result = new int[CntRowsA, CntColomnsB];

            var results = awaiting_all_task.Result;


            for (int i = 0; i < CntRowsA; i++)
                for (int j = 0; j < CntColomnsB; j++)
                {
                    result[i, j] = results[i  * CntColomnsB + j];
                }

            //awaiting_all_task.Result;

            Console.WriteLine("Все задачи завершились.");

            return result;
        }

        static async Task<int> MultiplyRowColomn(int[,] A, int[,] B, int Row, int Colomn)
        {
            var processing_tasks = new List<Task<int>>();

            for (int i = 0; i < A.GetLength(1); i++)
            {
                processing_tasks.Add(MultiplyElements(A[Row, i], B[i, Colomn]));
            }

            var awaiting_all_task = Task.WhenAll(processing_tasks);
            await awaiting_all_task;
            return awaiting_all_task.Result.Sum();
        }

        static async Task<int> MultiplyElements(int first, int second)
        {
            return first * second;
        }

    }
}
