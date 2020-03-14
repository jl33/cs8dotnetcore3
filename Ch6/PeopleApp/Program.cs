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

            Employee john = new Employee{
                Name="John Jones",
                DateOfBirth = new DateTime(1990,7,28)
            };
            john.WriteToConsole();
            john.EmployeeCode="JJ001";
            john.HireDate=new DateTime(2014,11,23);
            WriteLine($"{john.Name} was hired on {john.HireDate:yyyy/MMM/dd}");
            john.WriteToConsole();
            WriteLine(john.ToString());

            Employee aliceInEmployee = new Employee{
                Name="Alice",EmployeeCode="AA123"
            };
            Person aliceInPerson = aliceInEmployee;
            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();
            WriteLine(aliceInEmployee.ToString());
            WriteLine(aliceInPerson.ToString());

        
            if (aliceInPerson is Employee){
                WriteLine($"{nameof(aliceInPerson)} is an Employee");
                Employee explicitAlice = (Employee)aliceInPerson;
            }

            Employee aliceAsEmployee = aliceInPerson as Employee;
            if(aliceAsEmployee != null){
                WriteLine($"{nameof(aliceAsEmployee)} as an Employee");
            }

            try{
                john.TimeTravel(new DateTime(1999,12,31));
                john.TimeTravel(new DateTime(1950,12,20));

            }catch(PersonException ex){
                WriteLine(ex.Message);

            }

            string email1="partes@test.com";
            string email2="inst&test.com";
            WriteLine("{0} is a valid e-mail address: {1}",
            arg0: email1, arg1: StringExtension.IsValidEmail(email1));
            WriteLine("{0} is a valid e-mail address: {1}",
            arg0: email2, arg1: StringExtension.IsValidEmail(email2));
            WriteLine("{0} is a valid e-mail address: {1}",
            arg0: email1,arg1: email1.IsValidEmail());
            WriteLine("{0} is a valid e-mail address: {1}",
            arg0:email2,arg1: email2.IsValidEmail());
        }
    }
}
