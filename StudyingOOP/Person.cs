using System.Reflection.Metadata.Ecma335;

namespace PersonClasses;

class Person
{
    
    string name;
    string surname;
    //поле с значением по умолчанию
    int age = 18;
    //Статические поля хранят состояние всего класса / структуры. Статическое поле определяется как и обычное, только перед типом поля указывается ключевое слово static
    internal static int retirementAge = 65;


    // Свойства
    // [модификаторы] тип_свойства название_свойства
    // {
    //     get { действия, выполняемые при получении значения свойства}
    //     set { действия, выполняемые при установке значения свойства}
    // }
    //Блоки set и get не обязательно одновременно должны присутствовать в свойстве. Если свойство определяет только блок get, 
    //то такое свойство доступно только для чтения - мы можем получить его значение, но не установить.
    //Свойства необязательно связаны с определенной переменной. Они могут вычисляться на основе различных выражений

    public string FullName
    {
        get => $"{name} {surname}";
    }


    public int Age
    {
        //Можно применять модификаторы доступа не только ко всему свойству, но и к отдельным блокам get и set

        //Модификатор для блока set или get можно установить, если свойство имеет оба блока (и set, и get)
        //Только один блок set или get может иметь модификатор доступа, но не оба сразу
        //Модификатор доступа блока set или get должен быть более ограничивающим, чем модификатор доступа свойства.
        internal set
        {
            if (value < 1 || value > 120)
                Console.WriteLine("Возраст должен быть в диапазоне от 1 до 120");
            else
                //Параметр value представляет передаваемое значение, которое передается переменной name
                age = value;
        }
        get { return age; }
    }

    //Автоматические свойства
    public int Weight { get; protected set; }
    //Автосвойствам можно присвоить значения по умолчанию (инициализация автосвойств)
    public int Height { get; protected set; } = 90;

    //Для установки значений свойств с init можно использовать только инициализатор, либо конструктор, 
    //либо при объявлении указать для него значение. После инициализации значений подобных свойств их значения изменить нельзя - 
    //они доступны только для чтения. В этом плане init-свойства сближаются со свойствами для чтения. Разница состоит в том, 
    //что init-свойства мы также можем установить в инициализаторе
    public string Citizenship { get; init; } = "Undefined";

    //Если в классе не определено ни одного конструктора, то для этого класса автоматически создается пустой конструктор по умолчанию, который не принимает никаких параметров.
    //Если в классе определяются свои конструкторы, то он лишается конструктора по умолчанию.
    //Конструктор

    public Person(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        Console.WriteLine("Person Constructor 1");
    }

    public Person(string name, string surname, int age)
    {
        this.name = name;
        this.surname = surname;
        this.age = age;
        Console.WriteLine("Person Constructor 2");
    }

    public Person(string name, string surname, int age, int height, int weight): this(name, surname, age)
    {
        Height = height;
        Weight = weight;
        Console.WriteLine("Person Constructor 3");
    }

    //Деконструкторы позволяют выполнить декомпозицию объекта на отдельные части.
    public void Deconstruct(out string name, out string surname, out int age)
    {
        name = this.name;
        surname = this.surname;
        age = this.age;
    }




}