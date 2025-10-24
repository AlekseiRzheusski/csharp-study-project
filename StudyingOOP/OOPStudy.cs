using System.Reflection;
using PersonClasses;

namespace OOP;

//Для инициализации объектов классов можно применять инициализаторы. Инициализаторы представляют передачу в фигурных скобках значений доступным полям и свойствам объекта
//С помощью инициализатора объектов можно присваивать значения всем доступным полям и свойствам объекта в момент создания.

public static class OOPStudy
{
    static void ShowConversion()
    {
        //Неявное (implicit) преобразование — вверх по иерархии
        Adult adult = new Adult("Archie", "Quinn", "15616461650");
        Human human = adult;

        Console.Write("Method shadowing: ");
        human.Print();
        Console.Write("Method overriding: ");
        human.PrintCurrentClassName();
        // human.SocialSecurityNumber не работает тк класс Human

        //Явное (explicit) преобразование — вниз по иерархии
        Adult adult1 = (Adult)human;
        Console.Write("Method shadowing: ");
        adult1.Print();
        Console.Write("Method overriding: ");
        adult1.PrintCurrentClassName();

        // нельзя Adult adult2 = new Human()
    }

    static void ShowComponents(Type type)
    {
        //Метод GetMembers() возвращает все доступные компоненты типа в виде объекта MemberInfo. Этот объект позволяет извлечь некоторую информацию о компоненте типа. В частности, некоторые его свойства:
        // DeclaringType: возвращает полное название типа.
        // MemberType: возвращает значение из перечисления MemberTypes:
        // MemberTypes.Constructor
        // MemberTypes.Method
        // MemberTypes.Field
        // MemberTypes.Event
        // MemberTypes.Property
        // MemberTypes.NestedType
        // Name: возвращает название компонента
        foreach (MemberInfo member in type.GetMembers(BindingFlags.DeclaredOnly
            | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            Console.WriteLine($"\t{member.DeclaringType} {member.MemberType} {member.Name}");
        }
    }

    //В C# через рефлексию вполне реально вызвать приватный метод, если у тебя есть доступ к экземпляру класса (или самому типу, если метод статический).
    static void InvokePrivateMethod(Person person, string methodName)
    {
        Type type = typeof(Person);
        MethodInfo? method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            // Выводим модификаторы
            Console.WriteLine("Method found:");
            Console.WriteLine($"Name: {method.Name}");
            Console.WriteLine($"IsPublic: {method.IsPublic}");
            Console.WriteLine($"IsPrivate: {method.IsPrivate}");
            Console.WriteLine($"IsFamily (protected): {method.IsFamily}");
            Console.WriteLine($"IsAssembly (internal): {method.IsAssembly}");
            Console.WriteLine($"IsStatic: {method.IsStatic}");

            method.Invoke(person, null);
        }
        //Для получения обобщенной версии метода, которая типизирована определенным типом, 
        //у объекта MethodInfo вызывается метод MakeGenericMethod - в него передает тип, которым типизируется метод.
        // var printStringValue = printValue?.MakeGenericMethod(typeof(string));
        // printStringValue?.Invoke(myPrinter, new object[] {"Hello world"});
    }
    public static void Run()
    {
        Person person1 = new Person(name: "Kris", surname: "Camron");
        Person person2 = new Person(name: "Liam", surname: "Douglas", age: 13);

        (string name, string surname, int age) = person2;
        Console.WriteLine($"Person 2: {name}, {surname}, {age}, retirement age: {Person.retirementAge}");

        ShowConversion();
        OperatorOverloadingStudy.Run();

        Console.WriteLine("Components of the different classes");
        // Console.WriteLine(typeof(OperatorOverloadingStudy).Name);
        // ShowComponents(typeof(OperatorOverloadingStudy));

        Console.WriteLine(typeof(Person).Name);
        ShowComponents(typeof(Person));

        // Console.WriteLine(typeof(string));
        // ShowComponents(typeof(string));
        InvokePrivateMethod(person1, "SimplePrivateMethod");
        Console.WriteLine("Unless?");
    }
}