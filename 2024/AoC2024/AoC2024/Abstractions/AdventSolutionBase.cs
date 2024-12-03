using AoC2024.Interfaces;
using JetBrains.Profiler.Api;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Abstractions;

public abstract class AdventSolutionBase(IAoCLogger logger) : IAdventSolution
{
    public virtual async Task SolveAsync(string inputPath, int day, int part, bool useProfilerForTimer = false)
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
            logger.GetLogger().Information("Day {day} part {part} solution is {solution}", day, part, solution);

            if (!useProfilerForTimer)
                logger.GetLogger().Information("Day {day} part {part} took {ElapsedMilliseconds}ms", day, part, sw.ElapsedMilliseconds);
        }
        catch (FormatException e)
        {
            logger.GetLogger().Error(e, "Format invalid, is this the correct input and/or day?");
            return;
        }
    }

    public abstract int Solve(string[] input);
}
