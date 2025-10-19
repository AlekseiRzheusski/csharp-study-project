using System.Threading;
namespace Threading;

public record class ThreadParams
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public override string ToString()
    {
        return $"{Name} {Surname} {Thread.CurrentThread.Name}";
    }
}

public static partial class ThreadingStudy
{
    // объект-заглушка
    //Пока один поток держит «замок» на lock, другие потоки, пытающиеся войти в тот же lock(obj), будут ждать, пока первый не выйдет
    static object locker = new();
    //Передавая в конструктор значение true, указывается, что создаваемый объект изначально будет в сигнальном состоянии.
    static AutoResetEvent waitHandler = new AutoResetEvent(true);
    // Изначально мьютекс свободен, поэтому его получает один из потоков.
    static Mutex mutexObj = new Mutex();
    //Семафоры позволяют ограничить количество потоков, которые имеют доступ к определенным ресурсам
    //параметр initialCount задает начальное количество потоков, а maximumCount - максимальное количество потоков, которые имеют доступ к общим ресурсам
    static Semaphore semaphoreObj = new Semaphore(initialCount: 2, maximumCount: 2);

    static void SimpleTask()
    {
        string name = Thread.CurrentThread.Name ?? "unnamed";
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Thread {name}, {i}");
            Thread.Sleep(300);
        }
    }

    //При использовании ParameterizedThreadStart существует ограничение: можно запускать во втором потоке только такой метод, 
    //который в качестве единственного параметра принимает объект типа object?
    static void SimpleTaskWithParams(object? obj)
    {
        if (obj is ThreadParams)
        {
            Console.WriteLine(obj);
        }
    }

    static void SimpleLockedTask()
    {
        //Оператор lock определяет блок кода, внутри которого весь код блокируется и становится недоступным для других потоков до завершения работы текущего потока
        lock (locker)
        {
            string name = Thread.CurrentThread.Name ?? "unnamed";
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Locked Thread {name}, {i}");
                Thread.Sleep(300);
            }
        }
    }

    static void SimpleLockedTaskWithAutoResetEvent()
    {
        //Событие синхронизации может находиться в сигнальном и несигнальном состоянии. 
        //Если состояние события несигнальное, поток, который вызывает метод WaitOne, 
        //будет заблокирован, пока состояние события не станет сигнальным. Метод Set, 
        //наоборот, задает сигнальное состояние события.
        waitHandler.WaitOne();
        string name = Thread.CurrentThread.Name ?? "unnamed";
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"AutoResetEvent Thread {name}, {i}");
            Thread.Sleep(300);
        }
        waitHandler.Set();
    }

    static void SimpleLockedTaskWithMutex()
    {
        //Метод mutexObj.WaitOne() приостанавливает выполнение потока до тех пор, пока не будет получен мьютекс mutexObj.
        mutexObj.WaitOne();
        string name = Thread.CurrentThread.Name ?? "unnamed";
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"Mutex Thread {name}, {i}");
            Thread.Sleep(300);
        }
        //После выполнения всех действий, когда мьютекс больше не нужен, поток освобождает его с помощью метода mutexObj.ReleaseMutex().
        mutexObj.ReleaseMutex();
    }

    static void SimpleLockedTaskSemaphore()
    {
        //ожидание освобождения места в семафоре
        semaphoreObj.WaitOne();
        string name = Thread.CurrentThread.Name ?? "unnamed";
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"Semaphore Thread {name}, {i}");
            Thread.Sleep(300);
        }
        //освобождение места в семафоре
        semaphoreObj.Release();
    }

    static void ShowThreadInfo(Thread thread)
    {
        Console.WriteLine($"Thread name: {thread.Name}");
        Console.WriteLine($"Thread ExecutionContext: {thread.ExecutionContext}");
        Console.WriteLine($"Thread IsBackground: {thread.IsBackground}");
        Console.WriteLine($"Thread ManagedThreadId: {thread.ManagedThreadId}");

        //Lowest,BelowNormal,Normal,AboveNormal,Highest
        //По умолчанию потоку задается значение Normal. Можно изменить приоритет в процессе работы программы
        //Среда CLR будет считывать и анализировать значения приоритета и на их основании выделять данному потоку то или иное количество времени.
        Console.WriteLine($"Thread Priority: {thread.Priority}");

        // Aborted: поток остановлен, но пока еще окончательно не завершен
        // AbortRequested: для потока вызван метод Abort, но остановка потока еще не произошла
        // Background: поток выполняется в фоновом режиме
        // Running: поток запущен и работает (не приостановлен)
        // Stopped: поток завершен
        // StopRequested: поток получил запрос на остановку
        // Suspended: поток приостановлен
        // SuspendRequested: поток получил запрос на приостановку
        // Unstarted: поток еще не был запущен
        // WaitSleepJoin: поток заблокирован в результате действия методов Sleep или Join
        Console.WriteLine($"ThreadState: {thread.ThreadState}");
    }

    static void RunFiveThreads(ThreadStart task)
    {
        for (int i = 1; i < 6; i++)
        {
            Thread myThread = new(task);
            myThread.Name = $"Thread {i}";
            myThread.Start();
        }
    }

    public static void Run()
    {
        Thread mainThread = Thread.CurrentThread;
        ShowThreadInfo(mainThread);

        //Thread(ThreadStart): в качестве параметра принимает объект делегата ThreadStart, который представляет выполняемое в потоке действие
        Thread thread1 = new Thread(SimpleTask);
        thread1.Name = "Parallel thread 1";
        //Для запуска нового потока применяется метод Start класса Thread
        thread1.Start();
        //Параллельно запуск из главного потока
        SimpleTask();

        Thread threadWithParams1 = new Thread(SimpleTaskWithParams);
        threadWithParams1.Name = "Parallel thread with params 1";
        Thread threadWithParams2 = new Thread(SimpleTaskWithParams);
        threadWithParams2.Name = "Parallel thread with params 2";

        threadWithParams1.Start(new ThreadParams { Name = "Tom", Surname = "Ocean" });
        threadWithParams2.Start(new ThreadParams { Name = "Jeremy", Surname = "Mort" });

        //запуск без lock
        // RunFiveThreads(SimpleTask);

        //запуск с lock
        // RunFiveThreads(SimpleLockedTask);
        // RunFiveThreads(SimpleLockedTaskWithAutoResetEvent);
        // RunFiveThreads(SimpleLockedTaskWithMutex);
        // RunFiveThreads(SimpleLockedTaskSemaphore);

        ShowTaskParallelLibrary();
    }
}