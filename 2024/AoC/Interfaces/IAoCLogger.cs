using Serilog.Core;

namespace AoC.Interfaces;

public interface IAoCLogger
{
    public Logger GetLogger();
}