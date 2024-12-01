using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AoC2024.Utils;

public static class SolutionResolver
{
    public static Type? FindAdventSolution(int day, int part)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var solutionType = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttributes(typeof(AdventSolutionAttribute), false).Length != 0)
            .FirstOrDefault(t => t.GetCustomAttribute<AdventSolutionAttribute>()?.Day == day &&
                                 t.GetCustomAttribute<AdventSolutionAttribute>()?.Part == part);
        return solutionType;
    }

    public static async Task RunSolutionAsync(Type? solutionType, string inputPath, SolutionFactory factory,
        bool useProfilerForTimer = false)
    {
        if (solutionType == null)
            throw new NotImplementedException(
                "Could not find class with AdventSolutionAttribute with given day and/or part");

        var instance = factory.CreateSolutionInstance(solutionType);
        var solveMethod = solutionType.GetMethod("SolveAsync");

        if (solveMethod == null) throw new TargetException("Could not find SolveAsync method in solution class");

        if (solveMethod.Invoke(instance, [inputPath, useProfilerForTimer]) is Task task)
        {
            await task;
        }
    }
}