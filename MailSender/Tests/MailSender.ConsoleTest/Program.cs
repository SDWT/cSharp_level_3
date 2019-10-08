using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MailSender.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Lesson5
            //ThreadTest.Start();
            //SynchronizationTests.Start();
            //ThreadPoolTests.Start();

            //Console.ReadLine();
            //HomeWork5.Start();


            //var cCsvTxt = new ConverterCSVTXT();
            //cCsvTxt.ConvertCsv2Txt("test.csv", "outtest.txt");

            #region CSV_TXT

            //StreamReader sr = new StreamReader("");

            //if (!File.Exists("test.txt"))
            //    File.Create("test.txt").Close();
            //var fs = File.Open("test.txt", FileMode.Open, FileAccess.ReadWrite);

            //var str = "10 words\n";
            //var str2 = "New Horizon\n";

            //var arr1 = Encoding.UTF8.GetBytes(str);
            //var arr2 = Encoding.UTF8.GetBytes(str2);
            //int offset = arr1.Length;
            //fs.Write(arr1, 0, arr1.Length);
            //fs.Seek(0, SeekOrigin.Begin);
            //fs.Seek(arr1.Length, SeekOrigin.Begin);
            //fs.Write(arr2, 0, arr2.Length);
            //fs.Close();
            #endregion


            //new Thread(() => Console.WriteLine("Async parallel code")).Start();

            //Action<string> printer = str => Console.WriteLine(str);
            //printer("Hello World!");
            //printer.Invoke("Hello World!2");

            //// async in threadPool
            //printer.BeginInvoke("Hello World!3", result => Console.WriteLine("... completed!"), null); 

            #region History
            ////history
            //Func<string, int> string_transform = str =>
            //{
            //    Thread.Sleep(500);
            //    return str.Length;
            //};

            //var data = "Hello World!";
            //string_transform.BeginInvoke("Hello World!",
            //    result => { var lenght = string_transform.EndInvoke(result);
            //        Console.WriteLine("Lenght of {0} of {1}", (string)result.AsyncState, lenght);
            //}, data);
            #endregion

            #endregion

            //TPLTests.Start();
            TaskTests.Start();
            Console.ReadLine();
        }


    }
}
