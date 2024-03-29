﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class ConverterCSVTXT
    {
        private string _DestinationPath;
        private Dictionary<int, byte[]> _Buff = new Dictionary<int, byte[]>();
        private int _NextString = 0;


        private static readonly object __SyncRoot = new object();

        public void ConvertCsv2Txt(string sourcePath, string destinationPath)
        {
            var sr = new StreamReader(sourcePath);
            _DestinationPath = destinationPath;

            var semaphore = new Semaphore(10, 20);

            int i = 0;
            string str;
            while (!((str = sr.ReadLine()) is null))
            {
                var tmp = string.Format("{0}\n", str);
                int i0 = i;
                semaphore.WaitOne();
                ThreadPool.QueueUserWorkItem(o => WriteTXT(i0,
                    Convert(tmp),
                    semaphore));
                i++;
            }

            //Thread.Sleep(2000);
            
            //for (int i = 0; i < 10000; i++)
            //{
            //    var i0 = i;
            //    semaphore.WaitOne();
            //    ThreadPool.QueueUserWorkItem(o => WriteTXT(i0,
            //        Encoding.UTF8.GetBytes($"N{i0}\n"),
            //        semaphore));
            //}
        }

        private byte[] Convert(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void WriteTXT(int NumberString, byte[] array, Semaphore semaphore)
        {
            var destination = File.Open(_DestinationPath, FileMode.OpenOrCreate, FileAccess.Write);

            if (_NextString == NumberString)
            {
                _NextString++;
                // Write
                destination.Write(array, 0, array.Length);
                semaphore.Release();
                //Console.WriteLine($"{NumberString} : {_Buff.Count}");

                //if (_NextString >= 99)
                //    _Destination.Close();
            }
            else
                _Buff.Add(NumberString, array);

            int next = _NextString;
            byte[] value;
            while (_Buff.TryGetValue(next, out value))
            {
                destination.Write(value, 0, value.Length);
                //Console.WriteLine($"{next} : {_Buff.Count}");
                semaphore.Release();
                _Buff.Remove(next);
                next++;
            }
            _NextString = next;

            destination.Close();
            //if (_NextString >= 99)
            //    _Destination.Close();
        }

        //public void TestWrite()
        //{
        //    lock (__SyncRoot)
        //    {
        //        _Destination = File.Open("TestWrite.txt", FileMode.OpenOrCreate, FileAccess.Write);
        //        var semaphore = new Semaphore(10, 20);
        //        for (int i = 0; i < 10000; i++)
        //        {
        //            var i0 = i;
        //            semaphore.WaitOne();
        //            ThreadPool.QueueUserWorkItem(o => WriteTXT(i0, 
        //                Encoding.UTF8.GetBytes($"N{i0}\n"),
        //                semaphore));
        //        }
        //    }
        //}

    }
}
