using System.Diagnostics.Metrics;

namespace OOP;

class Kilometers
{
    internal required double Value { get; set; }

    const string suffix = "km";

    public override string ToString()
    {
        return $"{Value} {suffix}";
    }

    //неявное преобразование из метров
    public static implicit operator Kilometers(Meters m)
    {
        return new Kilometers { Value = m.Value / 1000 };
    }

    // неявное преобразование в double
    public static implicit operator double(Kilometers km)
    {
        return km.Value;
    }

    // явное преобразование из double в Kilometers
    public static explicit operator Kilometers(double num)
    {
        return new Kilometers { Value = num };
    }

    public static Kilometers operator +(Kilometers km1, Kilometers km2)
    {
        return new Kilometers { Value = km1.Value + km2.Value };
    }

    public static Kilometers operator -(Kilometers km1, Kilometers Km2)
    {
        double result = km1.Value - Km2.Value;
        return new Kilometers { Value = result > 0 ? result : 0 };
    }
}

class Meters
{
    internal required double Value { get; set; }

    const string suffix = "m";

    public override string ToString()
    {
        return $"{Value} {suffix}";
    }

    //явное преобразование из километров
    public static explicit operator Meters(Kilometers km)
    {
        return new Meters { Value = km.Value * 1000 };
    }
}

public static class OperatorOverloadingStudy
{
    public static void Run()
    {
        double number = 12;
        Kilometers km1 = (Kilometers)number;
        Console.WriteLine($"{km1}");

        Meters m1 = new Meters { Value = 14 };
        Kilometers km2 = m1;
        Console.WriteLine("{0} was transformed into {1}", m1, km2);

        var sum = km1 + km2;
        var sub = km2 - km1;
        Console.WriteLine($"{sum}, {sub}");

        Meters m2 = (Meters)sum;
        Console.WriteLine("{0} was transformed into {1}", sum, m2);
    }
}