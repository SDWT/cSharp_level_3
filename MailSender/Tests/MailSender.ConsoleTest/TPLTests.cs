using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    static class TPLTests
    {
        public static void Start()
        {

            //Parallel.For(0, 100, i => Console.WriteLine("Iter #{0}", i));

            var messages = Enumerable.Range(1, 100).Select(i => $"Message {i}").ToArray();

            //Parallel.For(0, messages.Length, 
            //    new ParallelOptions{ MaxDegreeOfParallelism = 3 }, 
            //    i =>
            //    {
            //        Thread.Sleep(250);
            //        Console.WriteLine("ThId {1} - Iter #{0}", i, Thread.CurrentThread.ManagedThreadId, messages[i]);
            //    });

            //Parallel.For(0, messages.Length,
            //    new ParallelOptions { MaxDegreeOfParallelism = 3 },
            //    (i, state) =>
            //    {
            //        Thread.Sleep(250);
            //        if (messages[i].EndsWith("15"))
            //            state.Break();

            //        Console.WriteLine("ThId {1} - Iter #{2}", i, Thread.CurrentThread.ManagedThreadId, messages[i]);
            //    });

            //var for_result = Parallel.For(0, messages.Length,
            //    new ParallelOptions { MaxDegreeOfParallelism = 3 },
            //    (i, state) =>
            //    {
            //        Thread.Sleep(250);
            //        if (messages[i].EndsWith("15"))
            //            state.Break();

            //        Console.WriteLine("ThId {1} - Iter #{2}", i, Thread.CurrentThread.ManagedThreadId, messages[i]);
            //    });

            //Console.WriteLine("{0}", for_result.LowestBreakIteration);

            //Parallel.Invoke(/*new ParallelOptions { MaxDegreeOfParallelism = 2}*/
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("And this"));


            #region Total messages lenght Parallel summary
            //var messages_lenghts = new List<int>(messages.Length);

            //Parallel.ForEach(messages, msg =>
            //{
            //    var lenght = MessageProcessor(msg);
            //    lock (messages_lenghts)
            //        messages_lenghts.Add(lenght);
            //});

            //var total_lenght = messages_lenghts.Sum();
            //Console.WriteLine("Total messages lenght = {0}", total_lenght);

            #endregion

            #region PLINQ

            //Console.WriteLine("Total messages length = {0}", messages
            //   .AsParallel()
            //   .WithDegreeOfParallelism(15)
            //    //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            //   .Select(MessageProcessor)
            //    //.AsSequential()
            //   .Sum());

            var total_length = messages
               .AsParallel()
               .WithDegreeOfParallelism(15)
               //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
               .Select(MessageProcessor)
               //.AsSequential()
               .Sum();

            Console.WriteLine("Total messages length = {0}", total_length);

            #endregion

        }
        private static void ParallelInvokeMethod()
        {
            Console.WriteLine("ThId {0} started", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(250);
            Console.WriteLine("ThId {0} ended", Thread.CurrentThread.ManagedThreadId);
        }

        private static int MessageProcessor(string msg)
        {
            Console.WriteLine("ThId {0} msg {1} started", Thread.CurrentThread.ManagedThreadId, msg);
            Thread.Sleep(250);
            Console.WriteLine("ThId {0} msg {1} ended", Thread.CurrentThread.ManagedThreadId, msg);
            return msg.Length;
        }
    }
}
