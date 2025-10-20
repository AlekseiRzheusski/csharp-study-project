
using System.Diagnostics;

namespace Threading;

public static partial class ThreadingStudy
{
    static Stopwatch stopWatch = new Stopwatch();
    //Возвращение объекта типа Task
    async static Task SimpleAsyncMethod(string name)
    {
        Console.WriteLine($"async method {name} started");
        await Task.Delay(4000);
        Console.WriteLine($"async method {name} is ended");
    }

    static void SimpleMethod(string name)
    {
        Console.WriteLine($"method {name} started");
        Thread.Sleep(4000);
        Console.WriteLine($"method {name} is ended");
    }

    //Метод может возвращать некоторое значение. Тогда возвращаемое значение оборачивается в объект Task, а возвращаемым типом является Task<T>
    async static Task<int> MulAsync(int num1, int num2)
    {
        // имитация нагрузки
        await Task.Delay(1000);
        return num1 * num2;
    }
    async static Task<int> SumAsync(int num1, int num2)
    {
        // имитация доп нагрузки
        await Task.Delay(1000);
        return num1 + num2;
    }

    public async static Task RunAsync()
    {
        stopWatch.Start();
        SimpleMethod("first");
        SimpleMethod("second");
        SimpleMethod("third");
        stopWatch.Stop();

        Console.WriteLine($"time passed {stopWatch.ElapsedMilliseconds} ms");

        stopWatch.Reset();

        stopWatch.Start();
        var firstMethod = SimpleAsyncMethod("first");
        var secondMethod = SimpleAsyncMethod("second");
        var thirdMethod = SimpleAsyncMethod("third");
        //метод принимает набор асинхронных задач и ожидает завершения всех этих задач
        //Так же есть метод Task.WhenAny(). Это аналог метода Task.WaitAny() - он завершает выполнение, когда завершается хотя бы одна задача.
        await Task.WhenAll(firstMethod, secondMethod, thirdMethod);
        stopWatch.Stop();
        Console.WriteLine($"time passed {stopWatch.ElapsedMilliseconds} ms");

        int sumResult = await SumAsync(3, 5);
        Console.WriteLine($"Async Sum Result = {sumResult}");

        var sum1 = SumAsync(4, 12);
        var sum2 = SumAsync(65, 90);
        var mul1 = MulAsync(12, 14);
        var mul2 = MulAsync(14, 20);
        int[] result = await Task.WhenAll(sum1, sum2, mul1, mul2);

        Console.WriteLine(string.Join(", ", result));
    }
}