namespace AoC.Interfaces;

public interface IAdventSolution
{
    public Task SolveAsync(string inputPath, int year, int day, int part, bool useProfilerForTimer = false);
}