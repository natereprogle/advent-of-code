using AoC.Commands;
using AoC.Interfaces;
using AoC.Logging;
using AoC.Utils;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();
registrations.AddTransient<IAoCLogger, AoCLogger>();
registrations.AddSingleton<SolutionFactory>();
var registrar = new TypeRegistrar(registrations);

var app = new CommandApp<RunCommand>(registrar);

app.Configure(config =>
{
    config.AddCommand<RunCommand>("run")
        .WithDescription("Runs a solution for a given day and part")
        .WithExample("run", "1", "1", "path/to/myinput.txt");

    config.AddCommand<ScaffoldCommand>("scaffold")
        .WithDescription("Scaffolds a solution for a given day and part. If no day or year is provided, today's day or year is used")
        .WithExample("scaffold")
        .WithExample("scaffold", "2024", "1");

    config.SetExceptionHandler((ex, _) => AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything));
});

return app.Run(args);