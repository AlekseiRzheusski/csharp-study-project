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
    public static void Run()
    {
        Person person1 = new Person(name: "Kris", surname: "Camron");
        Person person2 = new Person(name: "Liam", surname: "Douglas", age: 13);

        (string name, string surname, int age) = person2;
        Console.WriteLine($"Person 2: {name}, {surname}, {age}, retirement age: {Person.retirementAge}");

        ShowConversion();
    }
}