using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchornizingResourceAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();
            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);
            Task.WaitAll(new Task[] { a, b });
            WriteLine("");
            WriteLine($"Result: {Message}.");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed millisencods.");
            WriteLine($"{Counter} string modifications.");
        }

        static Random r = new Random();
        static string Message;
        static object conch = new object();
        static int Counter;

        static void MethodA()
        {
            // lock (conch)
            // {
            //     for (int i = 0; i < 5; i++)
            //     {
            //         Thread.Sleep(r.Next(2000));
            //         Message += "A";
            //         Write(".");
            //     }
            // }
            try
            {
                Monitor.TryEnter(conch,TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "A";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }            
            finally{
                Monitor.Exit(conch);
            }
        }
        static void MethodB()
        {
            // lock (conch)
            // {
            //     for (int i = 0; i < 5; i++)
            //     {
            //         Thread.Sleep(r.Next(2000));
            //         Message += "B";
            //         Write(".");
            //     }
            // }
            try
            {
                // Monitor.Enter(conch);
                Monitor.TryEnter(conch,TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "B";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }
    }
}
