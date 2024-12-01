// See https://aka.ms/new-console-template for more information

using AoC2024.Commands;
using AoC2024.Interfaces;
using AoC2024.Logging;
using AoC2024.Utils;
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

    config.SetExceptionHandler((ex, _) => AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything));
});

return app.Run(args);