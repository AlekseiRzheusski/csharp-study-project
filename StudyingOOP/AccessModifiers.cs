namespace StudyingOOP;


//Все классы и структуры могут иметь только модификаторы public, file или internal. По умолчанию internal.
public class AccessModifiers
{
    //private доступен только в рамках своего класса или структуры.
    private string privateField;

    //private protected доступен из любого места в своем классе или в производных классах, которые определены в той же сборке.
    private protected string privateProtectedField;

    //file доступ только в рамках файла
    //file string fileField;

    //protected доступен из любого места в своем классе или в производных классах, при этом производные классы могут располагаться в других сборках
    protected string protectedField;

    //internal компоненты класса или структуры доступен из любого места кода в той же сборке
    internal string internalField;

    //protected internal компонент класса доступен из любого места в текущей сборке и из производных классов, которые могут располагаться в других сборках
    protected internal string protectedInternalField;

    //public такой компонент доступен из любого места в коде, а также из других программ и сборок.
    public string publicField;

    //без модификатора private
}
