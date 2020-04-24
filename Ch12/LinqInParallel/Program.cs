using System;
using static System.Console;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            var watch=Stopwatch.StartNew();
            Write("Press ENTER to start: ");
            ReadLine();
            watch.Start();

            IEnumerable<int> numbers=Enumerable.Range(1,200_000_000);
            var squares=numbers.AsParallel()
                .Select(n => n * n).ToArray();

            watch.Stop();
            WriteLine("{0:#,##0} elapsed milliseconds.",
                watch.ElapsedMilliseconds);
        }
    }
}
