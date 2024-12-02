namespace AoC2024.Interfaces;

public interface IAdventSolution
{
    public Task SolveAsync(string inputPath, int day, int part, bool useProfilerForTimer = false);
}