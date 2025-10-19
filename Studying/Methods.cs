namespace Methods;

public static partial class MethodsStudy
{
    //Если метод в качестве тела определяет только одну инструкцию, то можно сократить определение метода.
    static void HelloWorld() => Console.WriteLine("Hello World");

    //Сокращенная версия методов с результатом.
    static int Sum(int num1, int num2) => num1 + num2;

    // И в языке C# мы можем создавать в классе несколько методов с одним и тем же именем, но разной сигнатурой. Что такое сигнатура? Сигнатура складывается из следующих аспектов:
    // Имя метода
    // Количество параметров
    // Типы параметров
    // Порядок параметров
    // Модификаторы параметров

    static int Sum(int num1, int num2, int num3) => num1 + num2 + num3;

    //По умолчанию при вызове метода необходимо предоставить значения для всех его параметров. Но C# также позволяет использовать 
    //необязательные параметры. Для таких параметров нам необходимо объявить значение по умолчанию. Также следует учитывать, 
    //что после необязательных параметров все последующие параметры также должны быть необязательными
    static void MethodWithDefaultParameters(int number, string name = "Viktor", string surname = "Ivanov") => Console.WriteLine($"number: {number}, name {name}, surname: {surname}");

    //При передаче аргументов параметрам по значению параметр метода получает не саму переменную, а ее копию и далее работает с этой копией независимо от самой переменной.
    //При передаче параметров по ссылке перед параметрами используется модификатор ref.

    static void Increment(ref int number)
    {
        number++;
    }

    //Чтобы сделать параметр выходным, перед ним ставится модификатор out.
    //методы, использующие такие параметры, обязательно должны присваивать им определенное значение.
    //по сути мы можем вернуть из метода не одно значение, а несколько.
    static void GetRectangleData(int width, int height, out int rectArea, out int rectPerimetr)
    {
        rectArea = width * height;
        rectPerimetr = (width + height) * 2;
    }

    //Кроме выходных параметров с модификатором out метод может использовать входные параметры с модификатором in. 
    //Модификатор in указывает, что данный параметр будет передаваться в метод по ссылке, однако внутри метода его значение параметра нельзя будет изменить.
    static void Multiply(in int num1, in int num2, out int result)
    {
        //num1 = 12; error CS8331: Cannot assign to variable 'num1' or use it as the right hand side of a ref assignment because it is a readonly variable
        result = num1 * num2;
    }

    static void ShowReferenceParameters()
    {
        //ref
        int number = 10;
        for (int i = 0; i < 5; i++)
            //модификатор ref указывается как перед параметром при объявлении метода
            Increment(ref number);

        //При передаче значений параметрам по ссылке метод получает адрес переменной в памяти. И, таким образом, если в методе изменяется значение параметра, 
        //передаваемого по ссылке, то также изменяется и значение переменной, которая передается на его место..
        Console.WriteLine(number);

        //out
        //ключевое слово out используется как при определении метода, так и при его вызове.
        //При этом можно определять переменные, которые передаются out-параметрам в непосредственно при вызове метода.
        GetRectangleData(5, 10, out int rectArea, out var rectPerimetr);
        Console.WriteLine($"area: {rectArea}, perimetr: {rectPerimetr}");

        //in
        Multiply(7, 8, out int result);
        Console.WriteLine(result);

    }

    //используя ключевое слово params, можно передавать неопределенное количество параметров
    //Сам параметр с ключевым словом params при определении метода должен представлять одномерный массив того типа, данные которого нужно использовать
    //после параметра с модификатором params нельзя указывать другие параметры. 
    static void ShowParametersArray(string current_name, params string[] names)
    {
        foreach (string name in names)
        {
            bool isCurrentName = name == current_name;
            Console.WriteLine($"{current_name} is {name}: {isCurrentName}");
        }
    }

    static void ShowInternalFunc()
    {
        int[] numbers = { 4, 5, 6, 3, 2, 13 };

        Console.WriteLine(Mul());
        Console.WriteLine(StaticMul(numbers));

        //Локальные функции представляют функции, определенные внутри других методов. Локальная функция, как правило, содержит действия, которые применяются только в рамках ее метода.
        int Mul()
        {
            int result = 1;
            foreach (var i in numbers)
            {
                result *= i;
            }
            return result;
        }

        //Локальноые функции определяются с помощью ключевого слова static. Их особенностью является то, что они не могут обращаться к переменным окружения, 
        //то есть метода, в котором статическая функция определена.

        static int StaticMul(int[] numbers)
        {
            int result = 1;
            foreach (var i in numbers)
            {
                result *= i;
            }
            return result;
        }
    }

    //Замыкание (closure) представляет объект функции, который запоминает свое лексическое окружение даже в том случае, когда она выполняется вне своей области видимости.
    //внешняя функция, которая определяет некоторую область видимости и в которой определены некоторые переменные и параметры - лексическое окружение
    static Action Outer()
    {
        //переменные и параметры (лексическое окружение),
        int i = 5;

        //вложенная функция, которая использует переменные и параметры внешней функции
        void Inner()
        {
            i++;
            Console.WriteLine(i);
        }
        return Inner;
    }

    static void ShowClosure()
    {
        Action closureFunc = Outer();
        closureFunc();
        closureFunc();
        closureFunc();
    }


    //Кортежи могут выступать в качестве результата как параметр и результат метода.
    static (string, string) Swap((string val1, string val2) values)
    {
        return (values.val2, values.val1);
    }


    public static void Run()
    {
        HelloWorld();
        Console.WriteLine(Sum(1, 2));


        //Для передачи значений параметрам о имени при вызове метода указывается имя параметра и через двоеточие его значение
        MethodWithDefaultParameters(3, surname: "Vasiliev");
        ShowReferenceParameters();

        //При вызове метода на место параметра с модификатором params можно передать как отдельные значения, так и массив значений, либо вообще не передавать параметры.
        string[] names = { "Alexey", "Viacheslav", "Tom", "Matt" };
        ShowParametersArray("Tom", names);
        ShowParametersArray("Tom", "Dmitri", "Vicktor", "Tom", "Denis");

        ShowInternalFunc();
        ShowClosure();
        Console.WriteLine(Swap(("first", "second")));

        ShowDelegates();
        ShowAnonimusMethods();
        ShowStandardDelegates();
    }
}