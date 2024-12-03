using Microsoft.Extensions.DependencyInjection;

namespace AoC.Utils;

public class SolutionFactory(IServiceProvider serviceProvider)
{
    public object CreateSolutionInstance(Type solutionType)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, solutionType);
    }
}