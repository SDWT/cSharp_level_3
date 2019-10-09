using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class ActionDirectoryFiles
    {
        string _DirectoryPath;
        int _Count;

        public ActionDirectoryFiles(string DirectoryPath)
        {
            _DirectoryPath = DirectoryPath;
        }

        public void Start()
        {
            var dirInfo = new DirectoryInfo(_DirectoryPath);
            var Files = dirInfo.GetFiles();
            var outPath = Path.Combine(_DirectoryPath, "result.data");

            var processing_tasks = new List<Task<int>>();


            _Count = Files.Length;
            int Max = Files.Length;
            var progressBar = new ConsoleProgressBar(Max);

            try
            {
                Parallel.ForEach(Files, (file) => DirAction(file.FullName, outPath));

                while (_Count >= 0)
                {
                    progressBar.Value = Max - _Count;
                    continue;
                }
            }
            catch (AggregateException ae)
            {
                foreach (var exception in ae.InnerExceptions)
                {
                    DirEnd(outPath, exception.Message);
                }
            }


            while (_Count >= 0)
            {
                progressBar.Value = Max - _Count;
                continue;
            }
            //foreach (var file in Files)
            //{
            //    await DirAction(file.FullName, Path.Combine(DirectoryPath, outPath));
            //}
        }

        private void DirAction(string FilePath, string outPath)
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

        void DirEnd(string outPath, string msg)
        {
            lock (_DirectoryPath)
            {
                File.AppendAllText(outPath, msg);
                Console.WriteLine(msg);
                _Count--;
            }
        }
    }
}
