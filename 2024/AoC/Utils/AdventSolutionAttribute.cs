namespace AoC.Utils;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AdventSolutionAttribute(int year, int day, int part) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
    public int Part { get; } = part;
}