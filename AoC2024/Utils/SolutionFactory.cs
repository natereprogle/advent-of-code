using Microsoft.Extensions.DependencyInjection;

namespace AoC2024.Utils;

public class SolutionFactory(IServiceProvider serviceProvider)
{
    public object CreateSolutionInstance(Type solutionType)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, solutionType);
    }
}