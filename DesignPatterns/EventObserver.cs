namespace EventObserver;

public class StandardUnit
{
    public event Action<StandardUnit>? Notify;
    int value = 42;
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            Notify?.Invoke(this);
        }
    }
}

public record class Person(string name, string surname);

public class Fine
{
    readonly Person person;

    public double Coeficient { get; set; }
    double? value;
    public double? Value
    {
        get
        {
            return value;
        }
    }
    public Fine(Person person, double coeficient)
    {
        this.person = person;
        this.Coeficient = coeficient;
    }
    public void Update(StandardUnit standardUnit)
    {
        this.value = standardUnit.Value * Coeficient;
        Console.WriteLine($"The value of Fine is {this.value} for {this.person.name} {this.person.surname}");
    }

    public void Subscribe(StandardUnit standardUnit)
    {
        standardUnit.Notify += Update;
        Console.WriteLine($"{this.person.name} {this.person.surname} subscribed");
        this.Update(standardUnit);
    }

    public void Unsubscribe(StandardUnit unit)
    {
        unit.Notify -= Update;
        Console.WriteLine($"{this.person.name} {this.person.surname} unsubscribed.");
    }
}

public static class EventObserverImplementation
{
    public static void Run()
    {
        StandardUnit standardUnit = new StandardUnit();
        Fine fine1 = new Fine(new Person(name: "Viktor", surname: "Giles"), 1.6);
        Fine fine2 = new Fine(new Person(name: "Kane", surname: "Benj"), 2.8);
        Fine fine3 = new Fine(new Person(name: "Jerold", surname: "Layton"), 5);

        fine1.Subscribe(standardUnit);
        fine2.Subscribe(standardUnit);
        fine3.Subscribe(standardUnit);

        Console.WriteLine("The standard unit value was changed to 52");
        standardUnit.Value += 10;
    }
}