using Serilog.Core;

namespace AoC2024.Interfaces;

public interface IAoCLogger
{
    public Logger GetLogger();
}