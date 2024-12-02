using Spectre.Console.Cli;

namespace AoC2024.Utils;

public class TypeResolver(IServiceProvider provider) : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _provider = provider ?? throw new ArgumentNullException(nameof(provider));

    public object? Resolve(Type? type)
    {
        return type == null ? null : _provider.GetService(type);
    }

    public void Dispose()
    {
        if (_provider is not IDisposable disposable) return;
        disposable.Dispose();
        GC.SuppressFinalize(this);
    }
}