using AoC.Abstractions;
using AoC.Interfaces;
using AoC.Utils;
using MoreLinq;
using Serilog.Core;

namespace AoC.Solutions;

[AdventSolution(2024, 4, 1)]
public class AoC2024Day4Part1(IAoCLogger logger) : AdventSolutionBase(logger)
{
    private readonly IAoCLogger logger = logger;

    //                            R DR  D  DL   L  UL  U UR
    private readonly int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
    private readonly int[] dy = { 0, -1, -1, -1, 0, 1, 1, 1 };

    // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to ##ms
    public override int Solve(string[] input)
    {
        var foundWords = 0;

        string[][] splitInput = input.Select(line => line.Select(c => c.ToString()).ToArray()).ToArray();
        for (int x = 0; x < splitInput.Length; x++)
        {
            for (int y = 0; y < splitInput[0].Length; y++)
            {
                if (splitInput[x][y] == "X")
                {
                    if (RecursiveFind(splitInput, x, y, 0, 0, 0))
                    {
                        logger.GetLogger().Information("Found XMAS at " + x + ", " + y);
                        foundWords += 1;
                    }
                }
            }
        }

        return foundWords;
    }

    /// <summary>
    /// Recursively finds the word "XMAS" in the input
    /// </summary>
    /// <param name="input">The input matrix</param>
    /// <param name="x">The current "x" value in the matrix</param>
    /// <param name="y">The current "y" value in the matrix</param>
    /// <param name="pass">The pass count. 0 = x, 1 = m, 2 = a, 3 = s</param>
    /// <param name="dirX">The direction that we're currently moving in on the X axis</param>
    /// <param name="dirY">The direction that we're currently moving in on the Y axis</param>
    /// <returns>True if a match was found</returns>
    public bool RecursiveFind(string[][] input, int x, int y, int pass, int dirX, int dirY)
    {

        if (pass == 4)
        {
            return true;
        }

        if (x < 0 || x >= input.Length || y < 0 || y >= input[0].Length)
        {
            return false;
        }

        if (input[x][y] != "XMAS"[pass].ToString())
        {
            return false;
        }

        if (pass == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                if (RecursiveFind(input, x + dx[i], y + dy[i], pass + 1, dx[i], dy[i]))
                {
                    return true;
                }
            }
        }
        else
        {
            return RecursiveFind(input, x + dirX, y + dirY, pass + 1, dirX, dirY);
        }

        return false;
    }
}

[AdventSolution(2024, 4, 2)]
public class AoC2024Day4Part2(IAoCLogger logger) : AdventSolutionBase(logger)
{
    // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to ##ms
    public override int Solve(string[] input)
    {
        string[][] splitInput = input.Select(line => line.Select(c => c.ToString()).ToArray()).ToArray();
        return 0;
    }
}