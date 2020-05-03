﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncEnumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            await foreach(int number in GetNumbers()){
                WriteLine($"Number: {number}");
            }
        }

        static async IAsyncEnumerable<int> GetNumbers(){
            var r =new Random();
            System.Threading.Thread.Sleep(r.Next(1000,2000));
            yield return r.Next(0,101);
            System.Threading.Thread.Sleep(r.Next(1000,2000));
            yield return r.Next(0,101);
            System.Threading.Thread.Sleep(r.Next(1000,2000));
            yield return r.Next(0,101);
            // yield return 3;
            // yield return 2;
            // yield return 1;
        }
    }
}
