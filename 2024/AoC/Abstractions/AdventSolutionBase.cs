using AoC.Interfaces;
using JetBrains.Profiler.Api;
using System.Diagnostics;

namespace AoC.Abstractions;

public abstract class AdventSolutionBase(IAoCLogger logger) : IAdventSolution
{
    public virtual async Task SolveAsync(string inputPath, int year, int day, int part, bool useProfilerForTimer = false)
    {
        var input = await File.ReadAllLinesAsync(inputPath);

        try
        {
            int solution;
            Stopwatch sw = new();
            if (useProfilerForTimer)
            {
                MeasureProfiler.StartCollectingData();
                solution = Solve(input);
                MeasureProfiler.StopCollectingData();
            }
            else
            {
                sw = Stopwatch.StartNew();
                solution = Solve(input);
                sw.Stop();
            }
            logger.GetLogger().Information("{year} day {day} part {part} solution is {solution}", year, day, part, solution);

            if (!useProfilerForTimer)
                logger.GetLogger().Information("{year} day {day} part {part} took {ElapsedMilliseconds}ms", year, day, part, sw.ElapsedMilliseconds);
        }
        catch (FormatException e)
        {
            logger.GetLogger().Error(e, "Format invalid, is this the correct input, year, and/or day?");
            return;
        }
    }

    public abstract int Solve(string[] input);
}
