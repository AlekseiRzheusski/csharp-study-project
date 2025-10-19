using PersonClasses;

using System.Security.Cryptography;

namespace OOP;

//Для инициализации объектов классов можно применять инициализаторы. Инициализаторы представляют передачу в фигурных скобках значений доступным полям и свойствам объекта
//С помощью инициализатора объектов можно присваивать значения всем доступным полям и свойствам объекта в момент создания.

public static class OOPStudy
{
    public static void Run()
    {
        Person person1 = new Person(name: "Kris", surname: "Camron");
        Person person2 = new Person(name: "Liam", surname: "Douglas", age: 13);

        (string name, string surname, int age) = person2;
        Console.WriteLine($"Person 2: {name}, {surname}, {age}, retirement age: {Person.retirementAge}");

    }
}