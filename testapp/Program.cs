using DataTypes;

class Program
{
    //Асинхронный вариант входа (C# 7.1+)
    static async Task Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("1.DataTypesStudy.Run()\n2.ConditionalsAndLoopsStudy.Run()\n3.MethodsStudy.Run()\n4.OOPStudy.Run()\n5.ThreadingStudy.Run()\n6.Linq\n7.Exit");
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
                    running = false;
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }

        }
    }
}

