using System.Diagnostics;
using AoC2024.Abstractions;
using AoC2024.Interfaces;
using AoC2024.Utils;
using JetBrains.Profiler.Api;

namespace AoC2024.Solutions;

[AdventSolution(1, 1)]
public class AoCDay1Part1(IAoCLogger logger) : AdventSolutionBase(logger)
{
    // Profiled in JetBrains Rider with dotTrace. Result: 25ms. Note that dotTrace adds overhead, stopwatch has this closer to 12ms
    public override int Solve(string[] input)
    {
        var (column1, column2) = input.Select(line => line.Split("   ").Select(int.Parse).ToArray())
            .ToArray()
            .Aggregate((new List<int>(), new List<int>()), (acc, nums) =>
            {
                acc.Item1.Add(nums[0]);
                acc.Item2.Add(nums[1]);
                return acc;
            });

        column1.Sort();
        column2.Sort();

        return column1.Zip(column2).Select(pair => Math.Abs(pair.First - pair.Second)).Sum();
    }
}

[AdventSolution(1, 2)]
public class AoCDay1Part2(IAoCLogger logger) : AdventSolutionBase(logger)
{
    // Profiled in JetBrains Rider with dotTrace. Result: 81ms. Note that dotTrace adds overhead, stopwatch has this closer to 30ms
    public override int Solve(string[] input)
    {
        var (column1, column2) = input.Select(line => line.Split("   ").Select(int.Parse).ToArray())
            .ToArray()
            .Aggregate((new List<int>(), new List<int>()), (acc, nums) =>
            {
                acc.Item1.Add(nums[0]);
                acc.Item2.Add(nums[1]);
                return acc;
            });

        var countMap = column1.ToDictionary(x => x, x => column2.Count(y => y == x));
        return countMap.Select(x => x.Key * x.Value).Sum();
    }
}