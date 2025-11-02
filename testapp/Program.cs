using System.Reflection;
using DataTypes;

class Program
{
    static Assembly GetAssemblyDLL()
    {
        string exeDir = AppDomain.CurrentDomain.BaseDirectory;
        string parentDir = Path.GetFullPath(Path.Combine(exeDir, "..", "..", "..", ".."));
        string dllPath = Path.Combine(parentDir, "DesignPatterns", "bin", "Debug", "net8.0", "DesignPatterns.dll");
        return Assembly.LoadFrom(dllPath);
    }

    static MethodInfo? GetMethod(Assembly asm, string typeName, string methodName)
    {
        MethodInfo? run = null;
        Type? observerImplementation = asm.GetType(typeName);
        if (observerImplementation is not null)
        {
            run = observerImplementation.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            return run;
        }
        else
        {
            Console.WriteLine("Type was not found");
            return null;
        }
    }

    //Асинхронный вариант входа (C# 7.1+)
    static async Task Main(string[] args)
    {
        // InterfaceObserver.ShowImplementation.Run();
        bool running = true;
        // динамическая загрузка сборки
        Assembly asm = GetAssemblyDLL();
        MethodInfo? showInterfaceObserver = GetMethod(asm, "InterfaceObserver.ObserverImplementation", "Run");
        MethodInfo? showEventObserver = GetMethod(asm, "EventObserver.EventObserverImplementation", "Run");

        while (running)
        {
            Console.WriteLine("1.DataTypesStudy.Run()\n2.ConditionalsAndLoopsStudy.Run()\n3.MethodsStudy.Run()\n4.OOPStudy.Run()\n5.ThreadingStudy.Run()\n6.Linq\n7.DesingPatterns\n8.FileSystem\n9.Exit");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char choice = keyInfo.KeyChar;

            switch (choice)
            {
                case '1':
                    DataTypesStudy.Run();
                    break;
                case '2':
                    ConditionalsAndLoops.ConditionalsAndLoopsStudy.Run();
                    break;
                case '3':
                    Methods.MethodsStudy.Run();
                    break;
                case '4':
                    OOP.OOPStudy.Run();
                    break;
                case '5':
                    Threading.ThreadingStudy.Run();
                    await Threading.ThreadingStudy.RunAsync();
                    break;
                case '6':
                    LINQ.LinqStudy.Run();
                    break;
                case '7':
                    showInterfaceObserver?.Invoke(null, null);
                    showEventObserver?.Invoke(null, null);
                    break;
                case '8':
                    FileSystem.FileSystemStudy.Run();
                    break;
                case '9':
                    running = false;
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }

        }
    }
}

