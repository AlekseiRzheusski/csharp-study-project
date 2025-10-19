namespace DataTypes;

public static class DataTypesStudy
{

    static void ShowDataTypes()
    {
        //bool хранит логические литералы true или false
        bool isNumber = true;
        Console.WriteLine("variable isNumber has {0} datatype, and {1} value with size {2} bytes", isNumber.GetType(), isNumber, sizeof(bool));

        //byte хранит целое число от 0 до 255
        byte byteNumber = 114;
        Console.WriteLine("variable byteNumber has {0} datatype, and {1} value with size {2} bytes", byteNumber.GetType(), byteNumber, sizeof(byte));

        //sbyte (байт со знаком) хранит целое число от -128 до 127
        sbyte sByteNumber = -14;
        Console.WriteLine("variable sByteNumber has {0} datatype, and {1} value with size {2} bytes", sByteNumber.GetType(), sByteNumber, sizeof(sbyte));

        //short хранит целое число от -32768 до 32767
        short shortNumber = -32111;
        Console.WriteLine("variable shortNumber has {0} datatype, and {1} value with size {2} bytes", shortNumber.GetType(), shortNumber, sizeof(short));

        //ushort (short без знака) хранит целое число от 0 до 65535
        ushort uShortNumber = 55012;
        Console.WriteLine("variable uShortNumber has {0} datatype, and {1} value with size {2} bytes", uShortNumber.GetType(), uShortNumber, sizeof(ushort));

        //int хранит целое число от -2147483648 до 2147483647
        int intNumber = 215651613;
        Console.WriteLine("variable intNumber has {0} datatype, and {1} value with size {2} bytes", intNumber.GetType(), intNumber, sizeof(int));

        //uint (int без знака) хранит целое число от 0 до 4294967295
        uint uIntNumber = 465164648;
        Console.WriteLine("variable uIntNumber has {0} datatype, and {1} value with size {2} bytes", uIntNumber.GetType(), uIntNumber, sizeof(uint));

        //long хранит целое число от –9 223 372 036 854 775 808 до 9 223 372 036 854 775 807
        long longNumber = -6516565156161;
        Console.WriteLine("variable longNumber has {0} datatype, and {1} value with size {2} bytes", longNumber.GetType(), longNumber, sizeof(long));

        //ulong (long без знака) хранит целое число от 0 до 18 446 744 073 709 551 615
        ulong uLongNumber = 15165654656151601202;
        Console.WriteLine("variable uLongNumber has {0} datatype, and {1} value with size {2} bytes", uLongNumber.GetType(), uLongNumber, sizeof(ulong));

        //float хранит число с плавающей точкой от -3.4*1038 до 3.4*1038. Для присвоения переменной типа float значения, необходимо использовать суффикс F/f
        float floatNumber = 3.14f;
        Console.WriteLine("variable floatNumber has {0} datatype, and {1} value with size {2} bytes", floatNumber.GetType(), floatNumber, sizeof(float));

        //double хранит число с плавающей точкой от ±5.0*10-324 до ±1.7*10308. При присвоениии суффикс не нужен, т.к дробные значения double по умолчанию
        double doubleNumber = 15.284;
        Console.WriteLine("variable doubleNumber has {0} datatype, and {1} value with size {2} bytes", doubleNumber.GetType(), doubleNumber, sizeof(double));

        //decimal хранит десятичное дробное число. Если употребляется без десятичной запятой, имеет значение от ±1.0*10-28 до ±7.9228*1028, может хранить 28
        //знаков после запятой и занимает 16 байт. Представлен системным типом System.Decimal. 
        //Для присвоения переменной типа decimal значения, необходимо использовать суффикс M/m
        decimal decimalNumber = 25965451.15151M;
        Console.WriteLine("variable decimalNumber has {0} datatype, and {1} value with size {2} bytes", decimalNumber.GetType(), decimalNumber, sizeof(decimal));

        //char хранит одиночный символ в кодировке Unicode.
        // в одинарных кавычках указываются символы '\x', после которых идет шестнадцатеричный код символа из таблицы ASCII.
        //  одинарных кавычках указываются символы '\u', после которых идет шестнадцатеричный код Unicode.
        char charLetter = 'A';
        Console.WriteLine("variable charLetter has {0} datatype, and {1} value with size {2} bytes", charLetter.GetType(), charLetter, sizeof(char));

        //string хранит набор символов Unicode. Двойные кавычки указывают на тип string, одинарные на char.
        string stringPhrase = "Hello World!";
        Console.WriteLine("variable charLetter has {0} datatype, and {1} value", stringPhrase.GetType(), stringPhrase);

        // object может хранить значение любого типа данных. object — это базовый тип всех типов в C#.
        // Компилятор не знает, что внутри, поэтому при доступе к членам типа требуется приведение (cast).
        // Может вызывать накладные расходы на упаковку/распаковку (boxing/unboxing).
        object a = 22;
        object b = 3.14;
        object c = "hello code";
        Console.WriteLine($"object type variables values: {a}, {b}, {c}. Size of object type depends on system bit. Types: {a.GetType()}, {b.GetType()}, {c.GetType()}");

        //Также можно можно указывать системные типы вместо сокращенного 
        System.Int32 systemTypeNumber = 4;
        Console.WriteLine($"System type variable: {systemTypeNumber}");

        //Для неявной типизации вместо названия типа данных используется ключевое слово var. Затем уже при компиляции компилятор сам выводит тип данных исходя из присвоенного значения.
        // var — это не тип, а синтаксический сахар для автоматического вывода типа компилятором.Тип переменной определяется на этапе компиляции, не во время выполнения.
        //мы не можем сначала объявить неявно типизируемую переменную, а затем инициализировать:
        var var1 = 20;
        var var2 = 'A';
        Console.WriteLine($"var: {var1}, {var2}. Types: {var1.GetType()}, {var2.GetType()}");
    }

    static void ShowArithmeticalOperations()
    {
        //Сложение двух чисел
        int sum = 11 + 12;

        //Вычитание двух чисел
        int sub = 14 - 11;

        //Умножение двух чисел
        int mul = 13 * 12;

        //деление двух чисел. При делении стоит учитывать, что если оба операнда представляют целые числа, то результат будет представлять собой только целую часть без дробной
        int div = 81 / 9;

        // Операция получение остатка от целочисленного деления двух чисел:
        double remainder = 10.0 % 4.0;

        Console.WriteLine($"Results:\n\tsum: {sum}, subtraction: {sub}, multiplication: {mul}, division: {div}, remainder of a division {remainder}");

        // Операция инкремента
        // Инкремент бывает префиксным: ++x - сначала значение переменной x увеличивается на 1, а потом ее значение возвращается в качестве результата операции.
        // И также существует постфиксный инкремент: x++ - сначала значение переменной x возвращается в качестве результата операции, а затем к нему прибавляется 1.
        int x1 = 5;
        int z1 = ++x1;
        Console.WriteLine($"prefix increment first value was 5: {x1} - {z1}");

        int x2 = 5;
        int z2 = x2++;
        Console.WriteLine($"postfix increment first value was 5: {x2} - {z2}");

        //Операция декремента или уменьшения значения на единицу. Также существует префиксная форма декремента (--x) и постфиксная (x--).

        //При выполнении сразу нескольких арифметических операций следует учитывать порядок их выполнения. Приоритет операций от наивысшего к низшему:
        // Инкремент, декремент
        // Умножение, деление, получение остатка
        // Сложение, вычитание
    }

    static void ShowBitwiseOperations()
    {
        // &(логическое умножение)
        // Умножение производится поразрядно, и если у обоих операндов значения разрядов равно 1, то операция возвращает 1, иначе возвращается число 0.
        int logicalAnd = 0b11011 & 0b10101;
        Console.WriteLine($"logocal AND result: {Convert.ToString(logicalAnd, 2)}");

        //|(логическое сложение)
        //возвращается единица, если хотя бы у одного числа в данном разряде имеется единица.
        int logicalOr = 0b010 | 0b101;
        Console.WriteLine($"logocal OR result: {Convert.ToString(logicalOr, 2)}");

        // ^ (логическое исключающее ИЛИ)
        // Если у нас значения текущего разряда у обоих чисел разные, то возвращается 1, иначе возвращается 0
        int xor = 0b101101 ^ 0b1100110;
        Console.WriteLine($"XOR result: {Convert.ToString(xor, 2)}");

        //~ (логическое отрицание или инверсия)
        // инвертирует все разряды: если значение разряда равно 1, то оно становится равным нулю, и наоборот.
        sbyte logicalNot = ~0b1000110;
        Console.WriteLine($"logical NOT result: {Convert.ToString(logicalNot, 2)}");

        //Для записи чисел со знаком в C# применяется дополнительный код (two’s complement), 
        //при котором старший разряд является знаковым. Если его значение равно 0, то число положительное, 
        //и его двоичное представление не отличается от представления беззнакового числа.
        int x = 12;
        int y = ~x;
        y += 1;
        Console.WriteLine($"Convert to negative, decimal result {y}, binary result {Convert.ToString(logicalNot, 2)}");

        //Операции сдвига также производятся над разрядами чисел. Сдвиг может происходить вправо и влево.
        //x<<y - сдвигает число x влево на y разрядов.
        //x>>y - сдвигает число x вправо на y разрядов
        int leftShift = 0b10011 << 2;
        Console.WriteLine($"Left shift result: {Convert.ToString(leftShift, 2)}");

        int rightShift = 0b101101 >> 2;
        Console.WriteLine($"Right shift result: {Convert.ToString(rightShift, 2)}");
    }

    static void ShowArrays()
    {
        //Массив представляет набор однотипных данных.
        //После определения переменной массива мы можем присвоить ей определенное значение:
        //используя операцию new, мы выделили память для 4 элементов массива: new int[4]. 
        //Число 4 еще называется длиной массива. При таком определении все элементы получают значение по умолчанию, 
        //которое предусмотренно для их типа. Для типа int значение по умолчанию - 0.

        int[] numbers = new int[4];
        bool[] bools = new bool[4];
        char[] chars = new char[4];
        float[] floats = new float[4];
        string[] strings = new string[4];

        Console.WriteLine("Default values for: ");
        Console.WriteLine($"\t integers: {string.Join(", ", numbers)}");
        Console.WriteLine($"\t booleans: {string.Join(", ", bools)}");
        Console.WriteLine($"\t chars: {string.Join(", ", chars)}");
        Console.WriteLine($"\t floats: {string.Join(", ", floats)}");
        Console.WriteLine($"\t string: {string.Join(", ", strings)}");

        //можно сразу указать значения для массива
        int[] nums = new int[4] { 1, 2, 3, 5 };
        string[] people = { "Tom", "Sam", "Bob" };

        //Для обращения к элементам массива используются индексы. Индекс представляет номер элемента в массиве, 
        //при этом нумерация начинается с нуля, поэтому индекс первого элемента будет равен 0, индекс четвертого элемента - 3.
        Console.WriteLine($"element with index 3:{nums[3]}");
        Console.WriteLine($"element with index 2:{people[2]}");


        //При попытке вызвать вписать не существующий индекс будет выброшено исключение IndexOutOfRangeException
        try
        {
            Console.WriteLine(nums[10]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.GetType()} {ex.Message}");
        }
        //Каждый массив имеет свойство Length, которое хранит длину массива.
        Console.WriteLine($"Length of nums:{nums.Length}");

        //начиная, с версии C# 8.0 в язык был добавлен специальный оператор ^, 
        //с помощью которого можно задать индекс относительно конца коллекции.
        Console.WriteLine($"{people[^1]}");

        //Массивы которые имеют два измерения (ранг равен 2) называют двухмерными. 
        //Поскольку массив nums2 двухмерный, он представляет собой простую таблицу.
        //Массивы могут иметь и большее количество измерений.
        //Определенную сложность может представлять перебор многомерного массива. 
        //Прежде всего надо учитывать, что длина такого массива - это совокупное количество элементов.
        int[,] nums2 = { { 0, 1, 2 }, { 3, 4, 5 } };
        foreach (int i in nums2)
            Console.Write($"{i} ");
        Console.WriteLine();

        //Чтобы пройтись по каждой строке надо получить количество элементов в размерности. 
        //У каждого массива есть метод GetUpperBound(номер_размерности), 
        //который возвращает индекс последнего элемента в определенной размерности. 
        int rows = nums2.GetUpperBound(0) + 1;
        int columns = nums2.Length / rows;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{nums2[i, j]} \t");
            }
            Console.WriteLine();
        }

        //От многомерных массивов надо отличать массив массивов или так называемый "зубчатый массив":
        //Здесь две группы квадратных скобок указывают, что это массив массивов, то есть такой массив,
        //который в свою очередь содержит в себе другие массивы. Причем длина массива указывается только
        //в первых квадратных скобках, все последующие квадратные скобки должны быть пусты: new int[3][]..
        int[][] nums3 = new int[3][];
        nums3[0] = new int[2] { 1, 2 };
        nums3[1] = new int[3] { 1, 2, 3 };
        nums3[2] = new int[5] { 1, 2, 3, 4, 5 };
        foreach (int[] row in nums3)
        {
            foreach (int number in row)
            {
                Console.Write($"{number} \t");
            }
            Console.WriteLine();
        }
        // обращение к массиву массивов происходит через [i][j], а не [i,j] как к многоменрным
        Console.WriteLine(nums3[1][2]);
    }

    static void ShowTypeConversation()
    {
        //Преобразования могут быть сужающие (narrowing) и расширяющие (widening).
        //Расширяющие преобразования расширяют размер объекта в памяти.
        //В случае с расширяющими преобразованиями компилятор за нас выполнял все преобразования данных, 
        //то есть преобразования были неявными (implicit conversion). 
        //Если производится преобразование от безнакового типа меньшей разрядности к безнаковому типу 
        //большой разрядности, то добавляются дополнительные биты, которые имеют значение 0.
        //Если производится преобразование к знаковому типу, то битовое представление дополняется нулями,
        //если число положительное, и единицами, если число отрицательное. 
        //Последний разряд числа содержит знаковый бит - 0 для положительных и 1 для отрицательных чисел. 
        //При расширении в добавленные разряды компируется знаковый бит.
        byte byteNumber = 4;
        ushort wideningByteNumber = byteNumber;
        Console.WriteLine($"orinal byte {Convert.ToString(byteNumber, 2).PadLeft(8, '0')}, after widening {Convert.ToString(wideningByteNumber, 2).PadLeft(16, '0')}");

        //Сужающие преобразования, наоборот, сужают значение до типа меньшей разядности.
        //При явных преобразованиях (explicit conversion) мы сами должны применить операцию преобразования (операция ())
        //в случае сужающих преобразований старшие биты усекаются, что приводит к потере точности
        int intNumber = 6541;
        byte narrowedIntNumber = (byte)intNumber;
        Console.WriteLine($"orinal byte {Convert.ToString(intNumber, 2)}, after widening {Convert.ToString(narrowedIntNumber, 2)}, decimal {narrowedIntNumber}");
    }

    //Перечисления представляют набор логически связанных констант.
    //Каждое перечисление фактически определяет новый тип данных, с помощью которых мы также,
    //как и с помощью любого другого типа, можем определять переменные, константы, параметры методов и т.д. 
    enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    //Константы перечисления могут иметь тип. Тип указывается после названия перечисления через двоеточие
    //Тип перечисления обязательно должен представлять целочисленный тип (byte, sbyte, short, ushort, int, uint, long, ulong). 
    //Если тип явным образом не указан, то по умолчанию используется тип int.
    enum Time : byte
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }
    //В то же время, несмотря на то, что каждая константа сопоставляется с определенным числом, 
    //мы НЕ можем присвоить ей числовое значение Operation op = 1; Cannot implicitly convert type 'int' to 'DataTypes.DataTypesStudy.Operation'

    //Можно также явным образом указать значения элементов, либо указав значение первого элемента
    //можно и для всех элементов явным образом указать значения
    //При этом константы перечисления могут иметь одинаковые значения, 
    //либо даже можно присваивать одной константе значение другой константы
    enum DayTime
    {
        Morning = 1,
        Afternoon = Morning,
        Evening = 2,
        Night = 2
    }

    static void ShowEnum()
    {

        DoOperation operation = (double x, double y, Operation op) =>
        {
            return op switch
            {
                //Зачастую переменная перечисления выступает в качестве хранилища состояния, 
                //в зависимости от которого производятся некоторые действия
                Operation.Add => x + y,
                Operation.Subtract => x - y,
                Operation.Multiply => x * y,
                Operation.Divide => x / y,
                _ => throw new InvalidOperationException("Unknown operation")
            };
        };

        Console.WriteLine(operation(12, 14, Operation.Add));
        Console.WriteLine(operation(16, 8, Operation.Divide));

        //Тип влияет на значения, которые могут иметь константы. По умолчанию каждому элементу перечисления 
        //присваивается целочисленное значение, причем первый элемент будет иметь значение 0, второй - 1 и так далее.
        Time now = Time.Morning;
        //Мы можем использовать операцию приведения, чтобы получить целочисленное значение константы перечисления
        Console.WriteLine($"number of the daytime = {(int)now}");
        Console.WriteLine($"Time.night number value: {(int)Time.Night}");
    }

    delegate double DoOperation(double x, double y, Operation op);

    static void ShowTuple()
    {
        //Кортежи предоставляют удобный способ для работы с набором значений.
        var tuple = ("Billy", 5, 10);

        //мы можем обращаться к каждому из значений кортежа через поля с названиями Item[порядковый_номер_поля_в_кортеже]
        Console.WriteLine(tuple.Item2);
        tuple.Item1 = "Henry";
        Console.WriteLine(tuple.Item1);
        tuple.Item3 += 27;
        Console.WriteLine(tuple.Item3);

        //В данном случае тип определяется неявно. Но мы также можем явным образом указать для переменной кортежа тип
        (int, int) tuple1 = (25, 651);

        //Также можно давать названия полям кортежа и по ним вызывать значения
        var tuple3 = (firstNum: 123, firstString: "123", secondNum: 123.001);
        Console.WriteLine($"{tuple3.firstNum} {tuple3.secondNum}");

        //Мы даже можем выполнить декомпозицию кортежа на отдельные переменные.
        var (name, age) = ("Tom", 23);

        //Одной из задач, которую позволяет элегантно решить кортеж - это обмен значениями
        string main = "Java";
        string second = "C#";
        (main, second) = (second, main);
        Console.WriteLine($"C# main: {main}, second: {second}");
    }

    public static void Run()
    {
        ShowDataTypes();
        ShowArithmeticalOperations();
        ShowBitwiseOperations();
        ShowArrays();
        ShowTypeConversation();
        ShowEnum();
        ShowTuple();
    }
}