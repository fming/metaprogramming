using System;
using System.Reflection;

public static class ObjectExtensions
{
    public static object Call(this object @this,
        string methodName, params object[] parameters)
    {
        var method = @this.GetType().GetMethod(methodName, BindingFlags.Instance |
            BindingFlags.Public, null, Array.ConvertAll<object, Type>(parameters,
            target => target.GetType()), null);

        return method.Invoke(@this, parameters);
    }
}

class ReflectionDemo
{
    static void Main(string[] args)
    {
        Console.Out.WriteLine(new Golfer().Call("Drive", "Reflection"));
        Console.Out.WriteLine(new RaceCarDriver().Call("Drive", "Reflection"));

        dynamic caller = new Golfer();
        Console.Out.WriteLine(caller.Drive("Dynamic"));

        var rangeOne = new Range(-10d, 10d);
        var rangeTwo = new Range(-5d, 15d);
        Console.Out.WriteLine(rangeOne + rangeTwo);


        Console.ReadLine();
    }
}

sealed class Range
{
    public double Maximum { get; private set; }
    public double Minimum { get; private set; }

    public static Range operator +(Range a, Range b)
    {
        return new Range(Math.Min(a.Minimum, b.Minimum), Math.Max(a.Maximum, b.Maximum));
    }

    public Range(double minimum, double maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    public override string ToString()
    {
        return string.Format("{0} :{1}", this.Minimum, this.Maximum);
    }

}

sealed class Golfer
{
    public string Drive(string technique)
    {
        return technique + " - 300 yards";
    }
}

sealed class RaceCarDriver
{
    public string Drive(string technique)
    {
        return technique + " - 200 miles an hour";
    }
}
