using System;

namespace Variables
{
  class Program
  {
    static void Main(string[] args)
    {
      object height = 1.88; // storing a double in an object
      object name = "Amir"; // storing a string in an object

      Console.WriteLine($"{name} is {height} metres tall.");

      //int length1 = name.Length; // gives compile error!
      int length2 = ((string)name).Length; // tell compiler it is a string

      Console.WriteLine($"{name} has {length2} characters.");

      // storing a string in a dynamic object
      dynamic anotherName = "Ahmed";

      // this compiles but would throw an exception at run-time
      // if you later store a data type that does not have a
      // property named Length
      int length = anotherName.Length;

      var population = 66_000_000; // 66 million in UK 
      var weight = 1.88; // in kilograms
      var price = 4.99M; // in pounds sterling
      var fruit = "Apples"; // strings use double-quotes 
      var letter = 'Z'; // chars use single-quotes
      var happy = true; // Booleans have value of true or false

      Console.WriteLine($"default(int) = {default(int)}");
      Console.WriteLine($"default(bool) = {default(bool)}");
      Console.WriteLine(
        $"default(DateTime) = {default(DateTime)}");
      Console.WriteLine(
        $"default(string) = {default(string)}");

      var i=10;
      var l=102L;
      var r=3.6;
      var f=1.3F;
      var d=2.3M;
      Console.WriteLine($"i=>{i.GetType()}"); //i=>System.Int32
      Console.WriteLine($"l=>{l.GetType()}"); //l=>System.Int64
      Console.WriteLine($"r=>{r.GetType()}"); //r=>System.Double
      Console.WriteLine($"f=>{f.GetType()}"); //f=>System.Single
      Console.WriteLine($"d=>{d.GetType()}"); //d=>System.Decimal

    }
  }
}