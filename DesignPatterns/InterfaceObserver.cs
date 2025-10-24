namespace InterfaceObserver;

public interface IObserver
{
    void Update(ISubject subject);
}

public interface ISubject
{
    void Add(IObserver observer);
    void Remove(IObserver observer);
    void Notify();
}

public record class Person(string name, string surname);

public class StandardUnit : ISubject
{
    int value = 42;
    public int Value {
        get { return value; }
        set
        {
            this.value = value;
            Notify();
        } 
    }

    private List<IObserver> _observers = new List<IObserver>();
    public void Add(IObserver observer)
    {
        this._observers.Add(observer);
        observer.Update(this);
    }
    public void Remove(IObserver observer)
    {
        this._observers.Remove(observer);
    }
    public void Notify()
    {
        Console.WriteLine("Notifying observers");

        try
        {
            Parallel.ForEach(_observers, observer => observer.Update(this));
        }
        catch (AggregateException ex)
        {
            foreach (var inner in ex.InnerExceptions)
            {
                Console.WriteLine($"Observer failed: {inner.Message}");
            }
        }    
    }

}

public class Fine : IObserver
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
    public void Update(ISubject subject)
    {
        Console.WriteLine($"Updating in Task: {Task.CurrentId}");
        if (subject is StandardUnit)
        {
            StandardUnit standardUnit = (StandardUnit)subject;
            this.value = standardUnit.Value * Coeficient;
            Console.WriteLine($"The value of Fine is {this.value} for {this.person.name} {this.person.surname}");
        }
        else
        {
            throw new NotImplementedException("Fine class can be updated only by StandardUnit.Value");
        }
    }

}

public static class ObserverImplementation
{
    public static void Run()
    {
        StandardUnit standardUnit = new StandardUnit();
        Fine fine1 = new Fine(new Person(name: "Viktor", surname: "Giles"), 1.6);
        Fine fine2 = new Fine(new Person(name: "Kane", surname: "Benj"), 2.8);
        Fine fine3 = new Fine(new Person(name: "Jerold", surname: "Layton"), 5);

        standardUnit.Add(fine1);
        standardUnit.Add(fine2);
        standardUnit.Add(fine3);

        Console.WriteLine("The standard unit value was changed to 52");
        standardUnit.Value = 52;
    }
}