using System.Runtime.CompilerServices;
namespace OOP;

abstract class Human
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public Human(string name, string surname)
    {
        this.Name = name;
        this.Surname = surname;
    }

    public void Print()
    {
        Console.WriteLine($"Class Human: {this.Name} {this.Surname}");
    }

    public virtual void PrintCurrentClassName()
    {
        Console.WriteLine("Current class is Person");
    }
}

class Adult : Human
{
    string socialSecurityNumber = null!;
    public string SocialSecurityNumber
    {
        get => socialSecurityNumber;
        set
        {
            if (!value.All(char.IsDigit))
            {
                throw new ArgumentException("Social Security Number must contains only digits");
            }
            socialSecurityNumber = value;
        }
    }

    public Adult(string name, string surname, string socialSecurityNumber) : base(name, surname)
    {
        this.SocialSecurityNumber = socialSecurityNumber;
    }
    //переопределение
    public new void Print()
    {
        Console.WriteLine($"Class Person: {this.Name} {this.Surname} with social securiy number: {SocialSecurityNumber}");
    }

    public override void PrintCurrentClassName()
    {
        Console.WriteLine("Current class is Adult");
    }
}


