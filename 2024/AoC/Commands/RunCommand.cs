using System.ComponentModel;
using System.Reflection;
using AoC.Interfaces;
using AoC.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Commands;

// This is instantiated by DI. If it's abstract then it fails because it can't be instantiated.
// ReSharper disable once ClassNeverInstantiated.Global
public class RunCommand(IAoCLogger logger, SolutionFactory factory) : AsyncCommand<RunCommand.Settings>
{
    // Same reason
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[YEAR]")] public int Year { get; set; } = DateTime.Now.Year;
        [CommandArgument(1, "[DAY]")] public int Day { get; set; } = DateTime.Now.Day;
        [CommandArgument(2, "[PART]")] public int Part { get; set; } = 1;

        [CommandArgument(3, "[INPUT]")] public string Input { get; set; } = "input.txt";

        [CommandOption("-p|--use-profiler", IsHidden = true)]
        [DefaultValue(false)]
        public bool Profiler { get; set; }

        public override ValidationResult Validate()
        {
            if (Input == "input.txt")
            {
                Input = $"Solutions/{Year}/input.txt";
            }
            else if (!File.Exists(Path.GetFullPath(Input)) || Path.GetExtension(Input) != ".txt")
            {
                return ValidationResult.Error("The input file does not exist or is not a text file");
            }

            if (Year > DateTime.Now.Year) return ValidationResult.Error("Year cannot be in the future");
            if (Day < 1 || Day > 25) return ValidationResult.Error("Day must be between 1 and 25");
            if (Year == DateTime.Now.Year && Day > DateTime.Now.Day) return ValidationResult.Error("Cannot run the current day before it has arrived");
            if (Year == DateTime.Now.Year && DateTime.Now.Month < 12) return ValidationResult.Error("Cannot run the current year before December");

            return base.Validate();
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        logger.GetLogger().Information("Running {Year} day {Day}, part {Part} using input {Input}", settings.Year, settings.Day,
            settings.Part, settings.Input);
        try
        {
            var solution = SolutionResolver.FindAdventSolution(settings.Year, settings.Day, settings.Part);
            await SolutionResolver.RunSolutionAsync(solution, factory, settings.Input, settings.Year, settings.Day, settings.Part, settings.Profiler);
        }
        catch (NotImplementedException e)
        {
            logger.GetLogger().Warning("Solution not found for {year} day {Day}, part {Part}", settings.Year, settings.Day,
                settings.Part);

            var scaffold = AnsiConsole.Prompt(
                new TextPrompt<bool>("Would you like to scaffold this solution?")
                    .AddChoice(true)
                    .AddChoice(false)
                    .DefaultValue(true)
                    .WithConverter(choice => choice ? "y" : "n"));

            if (scaffold)
            {
                await ScaffoldCommand.ScaffoldProject(settings.Year, settings.Day);
                logger.GetLogger().Information("Scaffolded solution for {year} day {Day}", settings.Year, settings.Day);
                return 0;
            }

            logger.GetLogger().Error(e, "Please scaffold this solution to continue. The below error was caused by you, and is simply for debugging");

            return 1;
        }
        catch (Exception e)
        {
            logger.GetLogger().Error(e, "An unknown error occurred");
        }

        return 0;
    }
}