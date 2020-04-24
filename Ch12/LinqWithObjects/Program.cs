using System;
using System.Linq;
using static System.Console;


namespace LinqWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // LinqWithArrayOfstrings();
            LinqWithArrayOfExceptions();
        }

        static void LinqWithArrayOfstrings(){
            var names = new string[] {"Michael","Pam","Jim","Dwight","Angela","Kevin","Toby","Creed"};
            // var query=names.Where(new Func<string, bool>(NameLongerThanFour));
            // var query=names.Where(NameLongerThanFour);
            var query=names
                .Where(n =>n.Length>4)
                .OrderBy(name =>name.Length)
                .ThenBy(n => n);
            foreach (string item in query){
                WriteLine(item);  
            }
        }

        static void LinqWithArrayOfExceptions(){
            var errors=new Exception[]{
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new InvalidCastException(),
                new OverflowException(),
                new DivideByZeroException(),
                new ApplicationException()
            };

            var numberErrors = errors.OfType<ArithmeticException>();
            foreach(var error in numberErrors){
                WriteLine(error);
            }
        }

        static bool NameLongerThanFour(string name){
            return name.Length>4;
        }
    }
}
