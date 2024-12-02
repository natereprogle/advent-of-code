using AoC2024.Abstractions;
using AoC2024.Interfaces;
using AoC2024.Utils;
using JetBrains.Profiler.Api;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace AoC2024.Solutions;

[AdventSolution(2, 1)]
public class AoCDay2Part1(IAoCLogger logger) : AdventSolutionBase(logger)
{
    public override int Solve(string[] input)
    {
        var rows = input.Select(line => line.Split(" ").Select(int.Parse).ToArray()).ToArray();

        return rows.Count(row =>
        {
            bool ascending = row[0] < row[1];

            for (int i = 1; i < row.Length - 1; i++)
            {
                int prev = row[i - 1];
                int current = row[i];
                int next = row[i + 1];

                bool isValid = Math.Abs(current - prev) is >= 1 and <= 3 && Math.Abs(current - next) is >= 1 and <= 3;

                if (ascending)
                {
                    if (current > next || !isValid)
                    {
                        return false;
                    }
                }
                else
                {
                    if (current < next || !isValid)
                    {
                        return false;
                    }
                }
            }

            return true;
        });
    }
}

[AdventSolution(2, 2)]
public class AoCDay2Part2(IAoCLogger logger) : AdventSolutionBase(logger)
{
    public override int Solve(string[] input)
    {
        var rows = input.Select(line => line.Split(" ").Select(int.Parse).ToArray()).ToArray();

        return rows.Count(row =>
        {
            if (IsValid(row))
            {
                return true;
            }

            for (int i = 0; i < row.Length; i++)
            {
                var newArray = row.Where((_, index) => index != i).ToArray();
                if (IsValid(newArray)) return true;
            }

            return false;
        });
    }

    private static bool IsValid(int[] row)
    {
        bool ascending = row[0] < row[1];
        for (int i = 1; i < row.Length - 1; i++)
        {
            int prev = row[i - 1];
            int current = row[i];
            int next = row[i + 1];
            bool isValid = Math.Abs(current - prev) is >= 1 and <= 3 && Math.Abs(current - next) is >= 1 and <= 3;
            if (ascending)
            {
                if (current > next || !isValid)
                {
                    return false;
                }
            }
            else
            {
                if (current < next || !isValid)
                {
                    return false;
                }
            }
        }

        return true;
    }
}