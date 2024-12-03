using AoC2024.Abstractions;
using AoC2024.Interfaces;
using AoC2024.Logging;
using AoC2024.Utils;
using System.Text.RegularExpressions;

namespace AoC2024.Solutions;

// This one was a doozy, so I added more comments than normal to explain it

[AdventSolution(3, 1)]
public partial class AoCDay3Part1(IAoCLogger logger) : AdventSolutionBase(logger)
{
    private readonly IAoCLogger logger = logger;

    // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to 6ms
    public override int Solve(string[] input)
    {
        var str = string.Join("", input);
        Regex regex = Part1Regex();
        var matches = regex.Matches(str);

        var finalResult = 0;

        if (matches == null)
        {
            logger.GetLogger().Warning("No matches found in the input string");
            return 0;
        }

        // A simple loop over all regex matches to multiply their values and add them to the final result. Nothing special, nothing difficult
        foreach (Match match in matches)
        {
            finalResult += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return finalResult;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex Part1Regex();
}

[AdventSolution(3, 2)]
public partial class AoCDay3Part2(IAoCLogger logger) : AdventSolutionBase(logger)
{
    private readonly IAoCLogger logger = logger;

    // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to 11ms
    public override int Solve(string[] input)
    {
        var str = string.Join("", input);
        Regex regex1 = Part2Regex1();
        Regex regex2 = Part2Regex2();
        var switches = regex2.Matches(str);

        // The first string won't be caught in the for loop below, so we add it here
        var strings = new List<KeyValuePair<bool, string>>
        {
            new(true, str[..switches[0].Index])
        };

        // Add all strings between the switches. Whether or not the switch was a "do()" or "don't()" is stored in the key
        for (var i = 0; i < switches.Count - 1; i++)
        {
            // We want to get the strings between each regex match
            var beginIndex = switches[i].Index + switches[i].Length;
            var endIndex = switches[i + 1].Index;

            strings.Add(new(switches[i].Value == "do()", str[beginIndex..endIndex]));
        }

        // Add the last element in the string since it also won't be caught
        strings.Add(new(switches[^1].Value == "do()", str[switches[^1].Index..]));

        var finalResult = 0;

        // Loop through all substrings and calculate the result
        foreach (var strs in strings)
        {
            // If the switch is true, meaning it was a "do()", add the products of all matches within the substring to the final result
            if (strs.Key)
            {
                var matches = regex1.Matches(strs.Value);
                if (matches == null)
                {
                    logger.GetLogger().Warning("No matches found in the input string");
                    return 0;
                }
                foreach (Match match in matches)
                {
                    finalResult += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
        }

        return finalResult;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex Part2Regex1();

    [GeneratedRegex(@"do(n't){0,1}\(\)")]
    private static partial Regex Part2Regex2();
}
