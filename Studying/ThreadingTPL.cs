using System.Threading.Tasks;
namespace Threading;

public static partial class ThreadingStudy
{
    static void ShowTaskInfo(Task task)
    {
        Console.WriteLine("Task AsyncState:{0}", task.AsyncState);
        Console.WriteLine("Task CurrentId:{0}", Task.CurrentId);
        Console.WriteLine("Task Id:{0}", task.Id);
        Console.WriteLine("Task Exception:{0}", task.Exception);

        // Canceled: задача отменена
        // Created: задача создана, но еще не запущена
        // Faulted: в процессе работы задачи произошло исключение
        // RanToCompletion: задача успешно завершена
        // Running: задача запущена, но еще не завершена
        // WaitingForActivation: задача ожидает активации и постановки в график выполнения
        // WaitingForChildrenToComplete: задача завершена и теперь ожидает завершения прикрепленных к ней дочерних задач
        // WaitingToRun: задача поставлена в график выполнения, но еще не начала свое выполнение
        Console.WriteLine("Task Status:{0}", task.Status);
        Console.WriteLine("Task IsCompleted:{0}", task.IsCompleted);
        Console.WriteLine("Task IsCanceled:{0}", task.IsCanceled);
        Console.WriteLine("Task IsFaulted:{0}", task.IsFaulted);
        Console.WriteLine("Task IsCompletedSuccessfully:{0}", task.IsCompletedSuccessfully);
    }

    static void SimpleTPLTask()
    {
        Console.WriteLine($"Task {Task.CurrentId} is started");
        Thread.Sleep(1000);
        Console.WriteLine($"Task {Task.CurrentId} is ended");
    }

    static int[] ShortestToCharParallel(string s, char c)
    {
        int length = s.Length;

        //задача которая возвращает значение
        Task<int[]> rightToLeft = Task.Run(() =>
        {
            int[] result = new int[length];
            int foundIndex = -length;
            for (int i = 0; i < length; i++)
            {
                if (s[i] == c)
                {
                    foundIndex = i;
                }
                result[i] = i - foundIndex;
            }
            return result;
        });

        Task<int[]> leftToRight = Task.Run(() =>
        {
            int[] result = new int[length];
            int foundIndex = length * 2;
            for (int i = length - 1; i >= 0; i--)
            {
                if (s[i] == c)
                {
                    foundIndex = i;
                }
                result[i] = foundIndex - i;
            }
            return result;
        });
        Task.WaitAll(rightToLeft, leftToRight);

        int[] rightResult = rightToLeft.Result;
        int[] leftResult = leftToRight.Result;
        int[] finalResult = new int[length];

        for (int i = 0; i < length; i++)
        {
            finalResult[i] = Math.Min(rightResult[i], leftResult[i]);
        }

        return finalResult;
    }

    static void ShowTaskParallelLibrary()
    {
        //В качестве параметра объект Task принимает делегат Action
        Task task1 = new Task(SimpleTPLTask);
        Task task2 = new Task(SimpleTPLTask);

        //Задачи продолжения или continuation task позволяют определить задачи, которые выполняются после завершения других задач
        // метод, который передается в вызов ContinueWith, должен принимать параметр типа Task
        Task task3 = task1.ContinueWith((Task t) =>
        {
            Console.WriteLine($"Task Id: {Task.CurrentId}");
            Console.WriteLine($"Previos Task Id: {t.Id}");
        });
        ShowTaskInfo(task1);
        //Task.Factory.StartNew() Этот метод также в качестве параметра принимает делегат Action, 
        //который указывает, какое действие будет выполняться. При этом этот метод сразу же запускает задачу
        //Метод Task.Run() также в качестве параметра может принимать делегат Action - выполняемое действие и возвращает объект Task.

        task1.Start();
        //Wait() блокирует вызывающий поток, в котором запущена задача, пока эта задача не завершит свое выполнение.
        task2.RunSynchronously(); //запуск синхронно в текущем потоке
        task3.Wait();
        //Если есть массив Task, то для ожидания завершения задач можно использовать  Task.WaitAll(Task[] tasks):
    
        int[] result = ShortestToCharParallel("loveleetcode", 'e');
        Console.WriteLine(string.Join(", ", result));
    }
}