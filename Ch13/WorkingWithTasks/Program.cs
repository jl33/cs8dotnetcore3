using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace WorkingWithTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            var timer=Stopwatch.StartNew();
            // WriteLine("Running methods synchronously on one thread.");
            // MethodA();
            // MethodB();
            // MethodC();

            WriteLine("Running methods asynchronously on multiple threads.");
            Task taskA=new Task(MethodA);
            taskA.Start();
            Task taskB=Task.Factory.StartNew(MethodB);
            Task taskC=Task.Run(new Action(MethodC));
            // Task[] tasks ={taskA,taskB,taskC};
            Task.WaitAll(new Task[]{taskA,taskB,taskC});

            WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
        }

        static void MethodA(){
            WriteLine("Starting Method A...");
            Thread.Sleep(3000);
            WriteLine("Finished Method A.");
        }

        static void MethodB(){
            WriteLine("Starting Method B...");
            Thread.Sleep(2000);
            WriteLine("Finished Method B.");
        }

        static void MethodC(){
            WriteLine("Starting Method C...");
            Thread.Sleep(1000);
            WriteLine("Finished Method C.");
        }
    }
}
