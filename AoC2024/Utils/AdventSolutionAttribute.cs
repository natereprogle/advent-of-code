namespace AoC2024.Utils;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AdventSolutionAttribute(int day, int part) : Attribute
{
    public int Day { get; } = day;
    public int Part { get; } = part;
}