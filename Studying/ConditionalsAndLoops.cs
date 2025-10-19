using System.Runtime.ExceptionServices;

namespace ConditionalsAndLoops;

public static class ConditionalsAndLoopsStudy
{
    static void ShowLogicalConditions()
    {
        //В C# определены логические операторы, которые также возвращают значение типа bool.
        // | Операция логического сложения или логическое ИЛИ. Возвращает true, если хотя бы один из операндов возвращает true.
        bool logicalOrFalse = (1 > 2) | (2 > 3);
        bool logicalOrTrue = (1 < 2) | (2 > 3);
        Console.WriteLine($"false | false: {logicalOrFalse}, true | false: {logicalOrTrue}");

        //& Операция логического умножения или логическое И. Возвращает true, если оба операнда одновременно равны true.
        bool logicalAndFalse = (1 < 2) & (2 > 3);
        bool logicalAndTrue = (1 < 2) & (2 < 3);
        Console.WriteLine($"true & false: {logicalAndFalse}, true & true: {logicalAndTrue}");

        // && и || — логические операторы с коротким замыканием (short-circuit)
        // Эти операторы работают только с логическими (bool) выражениями и останавливают вычисление, если результат уже известен.
        // && не вычисляет правую часть, если левая false
        // || не вычисляет правую часть, если левая true

        bool returnTrue() { Console.Write("return True method"); return true; }
        bool returnFalse() { Console.Write("return False method"); return false; }

        Console.WriteLine();
        bool res1 = returnTrue() || returnFalse();
        Console.WriteLine();
        bool res2 = returnFalse() || returnTrue();
        Console.WriteLine();
        bool res3 = returnFalse() && returnTrue();
        Console.WriteLine($"\nresult 1: {res1}, result2: {res2}, result 3: {res3}");

        //! Операция логического отрицания. Производится над одним операндом и возвращает true, если операнд равен false. 
        //Если операнд равен true, то операция возвращает false
        bool not = !true;
        Console.WriteLine(not);

        //^ Операция исключающего ИЛИ. Возвращает true, если либо первый, либо второй операнд (но не одновременно) равны true, иначе возвращает false
        bool logicalXorFalse = true ^ true;
        bool logicalXorTrue = false ^ true;
        Console.WriteLine($"true ^ true: {logicalXorFalse}, false ^ true: {logicalXorTrue}");
    }

    static void ShowIfElseConditionals()
    {
        //Инструкция if/else проверяет истинность некоторого условия и в зависимости от результатов проверки выполняет определенный код
        //После ключевого слова if ставится условие. Условие должно представлять значение типа bool. Это может быть непосредственно значение 
        //типа bool или результат условного выражения или другого выражения, которое возвращает значение типа bool. И если это условие истинно (равно true), 
        //то срабатывает код, который помещен далее после условия внутри фигурных скобок.

        string name = "Matt";

        if (name == "Tom")
        {
            Console.WriteLine("Tom");
        }
        //Используя конструкцию else if, мы можем обрабатывать дополнительные условия
        else if (name == "Matt")
        {
            Console.WriteLine("Matt");
        }
        //Блок else выполняется, если условие после if и else if ложно.
        else
        {
            Console.WriteLine("Another name");
        }

        //Тернарная операция позволяет проверить некоторое условие и в зависимости от его истинности выполнить некоторые действия.
        //[условие] ? [если условие true] : [если условие false]
        string name1 = name == "Matt" ? "Matt" : "Another name";
        Console.WriteLine(name1);
    }

    static void ShowSwitchConditionals()
    {
        //Конструкция switch/case оценивает некоторое выражение и сравнивает его значение с набором значений. 
        //И при совпадении значений выполняет определенный код.
        //В конце каждого блока сase должен ставиться один из операторов перехода: break, goto case, return или throw. 
        //Как правило, используется оператор break. При его применении другие блоки case выполняться не будут.

        string name = "Matt";
        switch (name)
        {
            case "Matt":
                Console.WriteLine("Matt");
                break;
            case "Peter":
                Console.WriteLine("Peter");
                break;
            case "Mat":
                Console.WriteLine("Mistake in the name");
                //если мы хотим, чтобы после выполнения текущего блока case выполнялся другой блок case, то мы можем использовать вместо break оператор goto case
                goto case "Matt";
            //если переменная не совпадает ни с одним значением case, то выполнится необязательный блок default
            default:
                Console.WriteLine("Another name");
                break;
        }

        //Конструкция switch позволяет возвращать некоторое значение. Для возвращения значения в блоках case может применятся оператор return.
        //Также можно сократить и получить результат неосредственно из конструкции switch;

        int operation = 2;
        (int firstOperand, int secondOperand) = (1, 2);
        int result = operation switch
        {
            //не требуется оператор case, а после сравниваемого значения ставится оператор стрелка =>. 
            //Значение справа от стрелки выступает в качестве возвращаемоего значения
            1 => firstOperand + secondOperand,
            2 => firstOperand - secondOperand,
            3 => firstOperand * secondOperand,
            //вместо оператора default используется прочерк _.
            _ => 0
        };
        Console.WriteLine("Result of the short switch {0}", result);
    }


    static void ShowLoops()
    {
        //Цикл for имеет следующее формальное определение:
        // for ([действия которые выполняются один раз до выполнения цикла]; [условие, при котором будет выполняться цикл]; [действия, которые выполняются после завершения блока цикла])
        for (int i = 1; i < 4; i++)
        {
            Console.WriteLine(i);
        }

        int j = 1;
        for (Console.WriteLine("Начало выполнения цикла"); j < 4; Console.WriteLine($"j = {j}"))
        {
            j++;
        }
        // Необязательно указывать все условия при объявлении цикла, можно опустить любой из блоков или вообще все
        // Можно определять несколько переменных в объявлении цикла:

        for (int num1 = 1, num2 = 1; num1 < 10; num1++, num2++)
            Console.WriteLine($"{num1 * num2}");

        //Цикл do..while.В цикле do сначала выполняется код цикла, а потом происходит проверка условия в инструкции while. 
        //И пока это условие истинно, цикл повторяется.
        int intForDoWhile = 6;
        do
        {
            Console.WriteLine(intForDoWhile);
            intForDoWhile--;
        }
        while (intForDoWhile > 0);

        //Цикл while. В отличие от цикла do цикл while сразу проверяет истинность некоторого условия, и если условие истинно, то код цикла выполняется:
        int intForWhile = 6;

        while (intForWhile > 0)
        {
            Console.WriteLine(intForWhile);
            intForWhile--;
        }

        //Цикл foreach предназначен для перебора набора или коллекции элементов.
        foreach (char character in "Foreach")
        {
            Console.WriteLine(character);
        }
        
        //Иногда возникает ситуация, когда требуется выйти из цикла, не дожидаясь его завершения. В этом случае мы можем воспользоваться оператором break.
        //чтобы при проверке цикл не завершался, а просто пропускал текущую итерацию используется оператор continue
        for (int num3 = 1; num3<12; num3++)
        {
            if (num3 % 2 == 0)
                continue;
            Console.WriteLine(num3);
        }
    }

    public static void Run()
    {
        ShowLogicalConditions();
        ShowIfElseConditionals();
        ShowSwitchConditionals();
        ShowLoops();
    }
}