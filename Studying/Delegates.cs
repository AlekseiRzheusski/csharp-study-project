using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;

namespace Methods;

public static partial class MethodsStudy
{
    static int Mul(int num1, int num2) => num1 * num2;
    static string ConcatString(string str1, string str2) => str1 + str2;
    static void MulWithOut(int num1, int num2, out int result) => result = num1 * num2;

    static void FirstMethod() => Console.WriteLine("First Method");
    static void SecondFMethod() => Console.WriteLine("Second Method");

    static void ShowResult<T>(T val1, T val2, GenericTypeOperation<T> op)
    {
        Console.WriteLine("result is {0}", op(val1, val2));
    }

    static void ShowDelegates()
    {
        // методы соответствуют делегату, если они имеют один и тот же возвращаемый тип и один и тот же набор параметров. 
        // Но надо учитывать, что во внимание также принимаются модификаторы ref, in и out.
        Operation opMul = Mul;

        //Operation opMul1 = MulWithOut; No overload for 'MulWithOut' matches delegate 'MethodsStudy.Operation'

        //определение с помощью конструктора
        Operation opSum = new Operation(Sum);

        Console.WriteLine($"sum: {opSum(4, 5)}, mul: {opMul.Invoke(4, 5)}");

        //делегат может указывать на множество методов, которые имеют ту же сигнатуру и возвращаемые тип.
        Print print = FirstMethod;
        print += SecondFMethod;
        print();
        print -= SecondFMethod;
        print?.Invoke();
        //метод Invoke и оператор условного null пропускает вызов делегата если он равен null
        print -= FirstMethod;
        if (print is null)
        {
            Console.WriteLine("delegate is null");
        }
        print?.Invoke();

        GenericTypeOperation<int> genericOp = Mul;
        //делегаты могут быть параметрами методов. и их можно возвращать из метода
        ShowResult<int>(3, 5, genericOp);

        GenericTypeOperation<string> genericOp1 = ConcatString;
        ShowResult<string>("Hello,", " World!", genericOp1);
    }

    static void ShowAnonimusMethods()
    {
        //Анонимный метод не может существовать сам по себе, он используется для инициализации экземпляра делегата.
        Operation opMul = delegate (int num1, int num2)
        {
            return num1 * num2;
        };
        Operation opSum = delegate (int num1, int num2)
        {
            return num1 + num2;
        };

        //При этом анонимный метод имеет доступ ко всем переменным, определенным во внешнем коде.
        int x = 3;
        GenericTypeOperation<int> opMultiplySum = delegate (int num1, int num2)
        {
            return (num1 + num2) * x;
        };

        //Лямбда-выражения представляют упрощенную запись анонимных методов
        //При определении списка параметров мы можем не указывать для них тип данных, В данном случае компилятор видит, что лямбда-выражение sum представляет тип Operation, а значит оба параметра лямбды представляют тип int.
        Operation lambdaOpMul = (num1, num2) => num1 * num2;

        //Лямбда с несколькими выражениями
        Operation lambdaSub = (num1, num2) =>
        {
            if (num1 > num2) return num1 - num2;
            return num2 - num1;
        };

        //Однако если мы применяем неявную типизацию, то у компилятора могут возникнуть трудности, чтобы вывести тип делегата, В этом случае можно указать тип параметров.

        Console.WriteLine($"anonimus methods result\n anonimus multiply: {opMul(3, 4)}\n anonumus sum: {opSum(10, 14)}\n anonimus multiply: {opMultiplySum(1, 3)}\n lambda multiply: {lambdaOpMul(10, 11)}");
        Console.WriteLine($"Lambda sub results: {lambdaSub(2, 5)}, {lambdaSub(6, 2)}");

    }
    
    //делегаты - это указатели на методы и с помощью делегатов мы можем вызвать данные методы.
    delegate int Operation(int num1, int num2);
    delegate void Print();

    //Делегаты, как и другие типы, могут быть обобщенными.
    delegate T GenericTypeOperation<T>(T val1, T val2);

    static void ShowStandardDelegates()
    {
        //Делегат Action представляет некоторое действие, которое ничего не возвращает, то есть в качестве возвращаемого типа имеет тип void
        Action action = () => Console.WriteLine("Action delegate");
        action();

        //Делегат Predicate<T> принимает один параметр и возвращает значение типа bool
        Predicate<string> predicate = line => line.Length > 12;
        Console.WriteLine("string length is more than 12: {0}", predicate("qwertyuioppasdhjk"));

        //Func возвращает результат действия и может принимать параметры.
        Func<int, int, int, int> mul = (a, b, c) => a * b * c;
        Console.WriteLine(mul(10, 5, 2));
    }
}