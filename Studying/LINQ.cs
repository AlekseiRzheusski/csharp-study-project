using System.Numerics;
using System.Threading.Tasks;

namespace LINQ;

//Есть два способа выполнения запроса LINQ: отложенное (deferred) и немедленное (immediate) выполнение.
//При отложенном выполнении LINQ-выражение не выполняется, пока не будет произведена итерация или перебор по выборке. Отложенные операции:
// AsEnumerable
// Cast
// Concat
// DefaultIfEmpty
// Distinct
// Except
// GroupBy
// GroupJoin
// Intersect
// Join
// OfType
// OrderBy
// OrderByDescending
// Range
// Repeat
// Reverse
// Select
// SelectMany
// Skip
// SkipWhile
// Take
// TakeWhile
// ThenBy
// ThenByDescending
// Union
// Where

//Немедленные выполнение возвращает результат сразу
// Aggregate
// All
// Any
// Average
// Contains
// Count
// ElementAt
// ElementAtOrDefault
// Empty
// First
// FirstOrDefault
// Last
// LastOrDefault
// LongCount
// Max
// Min
// SequenceEqual
// Single
// SingleOrDefault
// Sum
// ToArray
// ToDictionary
// ToList
// ToLookup

record class Person
{
    internal required string Name { get; set; }
    internal required int Age { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}

record class Company(string Name, Person[] personel);
public static class LinqStudy
{
    static Person[] people = {
        new Person{ Name = "Peter", Age = 23},
        new Person{ Name = "Pavel", Age = 36},
        new Person{ Name ="Lucas" , Age = 36},
        new Person{ Name = "John", Age = 12},
        new Person{ Name = "Marcus", Age = 55 },
        new Person{ Name = "Pavel", Age = 55}
    };

    static List<Company> companies = new List<Company>
    {
        new Company("Neversoft", new Person[]{new Person{ Name = "Peter", Age = 23}, new Person{ Name = "John", Age = 12}}),
        new Company("Solana", new Person[]{ new Person{ Name = "Peter", Age = 22}, new Person{ Name = "Marcus", Age = 25 }}),
        new Company("Belaruskali", new Person[]{ new Person{ Name = "Igor", Age = 22}, new Person{ Name = "Victor", Age = 25 }})
    };

    static void ShowSelect()
    {
        //выбрать только имена. ToList вместо foreach
        var names = people.Select(p => p.Name).ToList();
        Console.WriteLine(string.Join(", ", names));

        //создание объектов анонимного типа
        var newPeople = from person in people
                        select new
                        {
                            FullName = $"Mr. {person.Name}",
                            Year = DateTime.Now.Year - person.Age
                        };
        //Так как операция отложенная
        foreach (var person in newPeople)
            Console.WriteLine($"{person.FullName} - {person.Year}");


        //SelectMany делает то же, что и Select, но "расплющивает" (flatten) результат — превращает "коллекцию коллекций" в одну плоскую коллекцию.
        var employees = companies.SelectMany(c => c.personel).ToList();
        Console.WriteLine(string.Join(", ", employees));
    }

    static void ShowWhere()
    {
        var oldPeople = from person in people
                        where person.Age > 30
                        select person;

        foreach (var ogPerson in oldPeople)
            Console.WriteLine(ogPerson);

        var oldPeople1 = people.Where(p => p.Age > 30).ToList();
        Console.WriteLine(string.Join(", ", oldPeople1));

        var selectedCompanies = from company in companies
                                from person in company.personel
                                where person.Name == "Peter"
                                select company;

        foreach (var company in selectedCompanies)
            Console.WriteLine(company.Name);

        var selectedCompanies1 = companies.SelectMany(company => company.personel, (company, person) => new { company, person })
                                .Where(x => x.person.Name == "Peter")
                                .Select(x => x.company).ToList();

        foreach (var company in selectedCompanies1)
            Console.WriteLine(company.Name);
    }

    static void ShowOrderBy()
    {
        var sortedPeople = people.OrderBy(p => p.Age);
        foreach (var sortedPerson in sortedPeople)
            Console.WriteLine(sortedPerson);

        var sortedPeopleDescending = from person in people
                                     orderby person.Age descending
                                     select person;

        foreach (var sortedPerson in sortedPeopleDescending)
            Console.WriteLine(sortedPerson);
    }

    static void ShowSetOperations()
    {
        string[] namesGroup1 = { "Marcus", "Thomas", "Victor", "John", "Marcus", "Lucas" };
        string[] namesGroup2 = { "Marcus", "Victor", "John", "Kevin" };

        var exceptResult = namesGroup1.Except(namesGroup2);
        Console.WriteLine("except result: {0}", string.Join(", ", exceptResult));

        var intersectResult = namesGroup1.Intersect(namesGroup2);
        Console.WriteLine("intersect result: {0}", string.Join(", ", intersectResult));

        //Последовательное применение методов Concat и Distinct будет подобно действию метода Union
        var unionResult = namesGroup1.Union(namesGroup2);
        Console.WriteLine("union result: {0}", string.Join(", ", unionResult));
    }

    static void ShowAggregateOperations()
    {
        int[] numbers = { 10, 12, 22, 31, 14 };
        //В качестве условия агрегации используется выражение (x,y)=> x - y, 
        //то есть вначале из первого элемента вычитается второй, потом из получившегося значения вычитается третий и так далее.
        int sub = numbers.Aggregate((x, y) => x - y);
        int mul = numbers.Aggregate((x, y) => x * y);
        int count = numbers.Count();
        int max = numbers.Max();

        Console.WriteLine($"Sub: {sub}, Mul: {mul}, Count: {count}, Max: {max}");
    }

    static void ShowGroupping()
    {
        var peopleGroupedByAge = from person in people
                                 group person by person.Age;
        foreach (var age in peopleGroupedByAge)
        {
            Console.WriteLine("{0}:", age.Key);
            foreach (var person in age)
            {
                Console.WriteLine("\t{0}", person.Name);
            }
        }

        var peopleGroupedByAge1 = people.GroupBy(p => p.Age).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var (key, value) in peopleGroupedByAge1)
            Console.WriteLine($"Age: {key}, People: {string.Join(", ", value)}");
    }
    
    static void PLinq()
    {
        int Square(int n)
        {
            Console.WriteLine($"Process {n} int the thread {Thread.CurrentThread.ManagedThreadId}");
            return n * n;
        }
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //Метод AsParallel() позволяет распараллелить запрос к источнику данных. 
        //Он реализован как метод расширения LINQ у массивов и коллекций. 
        //При вызове данного метода источник данных разделяется на части (если это возможно) 
        //и над каждой частью отдельно производятся операции.
        var square = from number in numbers.AsParallel()
                     select Square(number);

        foreach (var number in square)
            Console.WriteLine($"Parallel linq result: {number}");

        //вместо foreach можно использовать метод ForAll() который выводит данные в том же потоке, в котором они обрабатываются.
        //Метод ForAll() в качестве параметра принимает делегат Action, который указывает на выполняемое действие
        (numbers.AsParallel().Select(n => Square(n))).ForAll(Console.WriteLine);

        //для упорядоченного вывода в соответствии с исходной последовательностью
        var square1 = from number in numbers.AsParallel().AsOrdered()
                      select number*number;
        foreach (var number in square)
            Console.Write($"{number}, ");

        Console.WriteLine();
    }

    public static void Run()
    {
        ShowSelect();
        ShowWhere();
        ShowOrderBy();
        ShowSetOperations();
        ShowAggregateOperations();
        ShowGroupping();
        PLinq();
    }
}