using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        public delegate int DelegateWithMatchingSignature(string s);
        private static void Harry_Shout(object sender,EventArgs e){
            Person p=(Person)sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
        }
     
        static void Main(string[] args)
        {
            var harry= new Person{Name="Harry"};
            var mary=new Person{Name="Mary"};
            var jill=new Person{Name="Jill"};

            //call instance method
            var baby1=mary.ProcreateWith(harry);

            //call static method
            var baby2=Person.Procreate(harry,jill);

            //call an operator
            var baby3=harry * mary;


            WriteLine($"{harry.Name} has {harry.Children.Count} children.");
            WriteLine($"{mary.Name} has {mary.Children.Count} children.");
            WriteLine($"{jill.Name} has {jill.Children.Count} children.");

            WriteLine(
                format:"{0}'s first child is named \"{1}\".",
                arg0: harry.Name,
                arg1:harry.Children[0].Name);

            WriteLine($"5! is {Person.Factorial(5)}");

            var p1=new Person{Name="Joe"};
            int answer=p1.MethodIWantToCall("Frog");
            WriteLine($"instance->{answer}");

            var d=new DelegateWithMatchingSignature(p1.MethodIWantToCall);            

            int answer2=d("Frog");
            WriteLine($"delegate->{answer2}");

            harry.Shout+=Harry_Shout;
            harry.Poked();
            harry.Poked();
            harry.Poked();
            harry.Poked();

            Person[] people={
                new Person{Name="Simon"},
                new Person{Name="Cathy"},
                new Person{Name="Judy"},
                new Person{Name="Adam"}
            };

            WriteLine("initial list of people:");
            foreach(var person in people){
                WriteLine($"{person.Name}");
            }

            WriteLine("Use Person's IComparable to implementation to sort:");
            Array.Sort(people);
            foreach(var person in people){
                WriteLine($"{person.Name}");
            }

            WriteLine("Use PersonComparer's IComparer to implementation to sort:");
            Array.Sort(people,new PersonComparer());
            foreach(var person in people){
                WriteLine($"{person.Name}");
            }

            var t1 = new Thing();
            t1.Data = 42;
            WriteLine($"Thing with an integer: {t1.Process(42)}");

            var t2 = new Thing();
            t2.Data = "apple";
            WriteLine($"Thing with a string: {t2.Process("apple")}");

            var gt1 =  new GenericThing<int>();
            WriteLine($"{gt1.ToString()}");
            gt1.Data = 42;
            WriteLine($"GenericThing with an integer: {gt1.Process(42)}");

            var gt2 = new GenericThing<string>();
            gt2.Data = "apple";
            WriteLine($"GenericThing with a string: {gt2.Process("apple")}");

            string number1 = "4";
            WriteLine("{0} squuared is {1}",arg0: number1,arg1:Squarer.Square<string>(number1));

            byte number2 = 3;
            WriteLine("{0} squared is {1}",arg0:number2,arg1:Squarer.Square(number2));

            var dv1=new DisplacementVector(2,5);
            var dv2=new DisplacementVector(-1,7);
            var dv3=dv1+dv2;
            WriteLine($"({dv1.X},{dv1.Y}) + ({dv2.X},{dv2.Y}) = ({dv3.X},{dv3.Y})");

        }
    }
}
