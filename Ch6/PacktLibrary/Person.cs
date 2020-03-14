using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public class Person:IComparable<Person>
    {
        //fields
        public string Name;
        public DateTime DateOfBirth;
        public List<Person> Children = new List<Person>();

        //methods
        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        //static method
        public static Person Procreate(Person p1,Person p2)
        {
            var baby = new Person{
                Name=$"Baby of {p1.Name} and {p2.Name}"
            };

            p1.Children.Add(baby);
            p2.Children.Add(baby);
            return baby;

        }

        //instance method
        public Person ProcreateWith(Person partner){
            return Procreate(this, partner);
        }

        //operator
        public static Person operator *(Person p1,Person p2){
            return Person.Procreate(p1,p2);
        }

        //method with local function
        public static int Factorial(int number){
            if (number <0){
                throw new ArgumentException(
                    $"{nameof(number)} cannot be less than zero."
                );
            }
            return localFactorial(number);

            //local function
            int localFactorial(int localNumber){
                if(localNumber<1) return 1;
                return localNumber * localFactorial(localNumber -1);

            }
        }

        //delegate
        public int MethodIWantToCall(string input){
            return input.Length;
        }

        public event EventHandler Shout;
        public int AngerLevel;
        public void Poked(){
            AngerLevel++;
            if (AngerLevel>=3){
                // if(Shout != null){
                    // Shout(this,EventArgs.Empty);
                // }
                Shout?.Invoke(this,EventArgs.Empty);
            }
        }

        public int CompareTo(Person other)
        {
            // throw new NotImplementedException();
            return Name.CompareTo(other.Name);
        }

        public override string ToString(){
            return $"{Name} is a {base.ToString()}";
        }

        public void TimeTravel(DateTime when){
            if (when <= DateOfBirth){
                throw new PersonException("If you travel back in time to a date earlier than your own barth, then the universe will explode!");
            }
            else{
                WriteLine($"Welcome to {when:yyyy}!");
            }
        }
    }

    public class PersonComparer: IComparer<Person>{
        public int Compare(Person x, Person y){
            int result=x.Name.Length.CompareTo(y.Name.Length);
            if (result==0){
                return x.Name.CompareTo(y.Name);
            }else{
                return result;
            }
        }
    }
}
