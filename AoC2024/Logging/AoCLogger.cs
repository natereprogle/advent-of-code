using AoC2024.Interfaces;
using Serilog;
using Serilog.Core;

namespace AoC2024.Logging;

public class AoCLogger : IAoCLogger
{
    private readonly Logger _logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();

    public Logger GetLogger() => _logger;
}