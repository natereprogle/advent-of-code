namespace AoC2024.Interfaces;

public interface IAdventSolution
{
    public Task SolveAsync(string inputPath, bool useProfilerForTimer = false);
}