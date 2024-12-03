using AoC.Interfaces;
using AoC.Utils;
using Microsoft.VisualBasic;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Commands;

// This is instantiated by DI. If it's abstract then it fails because it can't be instantiated.
// ReSharper disable once ClassNeverInstantiated.Global
public class ScaffoldCommand(IAoCLogger logger) : AsyncCommand<ScaffoldCommand.Settings>
{

    // Same reason
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[YEAR]")]
        public int Year { get; set; } = DateTime.Now.Year;
        [CommandArgument(1, "[DAY]")]
        public int Day { get; set; } = DateTime.Now.Day;

        public override ValidationResult Validate()
        {
            if (Year > DateTime.Now.Year) return ValidationResult.Error("Year cannot be in the future");
            if (Day < 1 || Day > 25) return ValidationResult.Error("Day must be between 1 and 25");
            if (Year == DateTime.Now.Year && Day > DateTime.Now.Day) return ValidationResult.Error("Cannot run the current day before it has arrived");
            if (Year == DateTime.Now.Year && DateTime.Now.Month < 12) return ValidationResult.Error("Cannot run the current year before December");

            return base.Validate();
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        try
        {
            await ScaffoldProject(settings.Year, settings.Day);
            logger.GetLogger().Information("Downloaded today's input and scaffolded solution for {year} day {Day}", settings.Year, settings.Day);

        }
        catch (ArgumentException e)
        {
            logger.GetLogger().Warning(e.Message);
        }
        return 1;
    }

    private static string Session
    {
        get
        {
            if (!Environment.GetEnvironmentVariables().Contains("SESSION"))
            {
                throw new ApplicationException("Specify SESSION environment variable", null);
            }
            return Environment.GetEnvironmentVariable("SESSION")!;
        }
    }

    public static async Task ScaffoldProject(int year, int day)
    {
        if (File.Exists($"{Directory.GetCurrentDirectory()}/Solutions/{year}/AoCDay{day}.cs"))
        {
            throw new ArgumentException($"Solution for {year} day {day} already exists");
        }

        var handler = new HttpClientHandler
        {
            CookieContainer = new()
        };
        var cookie = new Cookie("session", Session, "/", ".adventofcode.com");
        handler.CookieContainer.Add(cookie);

        var client = new HttpClient(handler);
        var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        var content = await response?.Content?.ReadAsStringAsync()!;

        if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/Solutions"))
        {
            throw new DirectoryNotFoundException("Could not find the Solutions folder. Please use dotnet run from within the AoC2024 folder (i.e. do not use --project)");
        }

        Directory.CreateDirectory($"Solutions/{year}");

        await File.WriteAllTextAsync($"{Directory.GetCurrentDirectory()}/Solutions/{year}/input.txt", content);
        await File.WriteAllTextAsync($"{Directory.GetCurrentDirectory()}/Solutions/{year}/AoCDay{day}.cs", GenerateSolution(year, day));
    }

    private static string GenerateSolution(int year, int day)
    {
        return $$"""
            using AoC.Abstractions;
            using AoC.Interfaces;
            using AoC.Utils;

            namespace AoC.Solutions;

            [AdventSolution({{year}}, {{day}}, 1)]
            public class AoC{{year}}Day{{day}}Part1(IAoCLogger logger) : AdventSolutionBase(logger)
            {
                // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to ##ms
                public override int Solve(string[] input)
                {
                    return 0;
                }
            }

            [AdventSolution({{year}}, {{day}}, 2)]
            public class AoC{{year}}Day{{day}}Part2(IAoCLogger logger) : AdventSolutionBase(logger)
            {
                // Profiled in JetBrains Rider with dotTrace. Result: ##ms. Note that dotTrace adds overhead, stopwatch has this closer to ##ms
                public override int Solve(string[] input)
                {
                    return 0;
                }
            }
            """;
    }

}