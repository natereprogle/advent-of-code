using System.ComponentModel;
using System.Reflection;
using AoC2024.Interfaces;
using AoC2024.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC2024.Commands;

// This is instantiated by DI. If it's abstract then it fails because it can't be instantiated.
// ReSharper disable once ClassNeverInstantiated.Global
public class RunCommand(IAoCLogger logger, SolutionFactory factory) : AsyncCommand<RunCommand.Settings>
{
    // Same reason
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<DAY>")] public int Day { get; set; }
        [CommandArgument(1, "<PART>")] public int Part { get; set; }

        [CommandArgument(2, "<INPUT>")] public required string Input { get; set; }

        [CommandOption("-p|--use-profiler", IsHidden = true)]
        [DefaultValue(false)]
        public bool Profiler { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        if (!File.Exists(Path.GetFullPath(settings.Input)) || Path.GetExtension(settings.Input) != ".txt")
        {
            logger.GetLogger().Error("The input file {Input} does not exist or is not a text file", settings.Input);
            return 1;
        }

        logger.GetLogger().Information("Running day {Day}, part {Part} using input {Input}", settings.Day,
            settings.Part, settings.Input);
        try
        {
            var solution = SolutionResolver.FindAdventSolution(settings.Day, settings.Part);
            await SolutionResolver.RunSolutionAsync(solution, settings.Input, factory, settings.Profiler);
        }
        catch (NotImplementedException e)
        {
            logger.GetLogger().Error(e, "Solution not found for day {Day}, part {Part}", settings.Day,
                settings.Part);
            logger.GetLogger().Warning("This is not an error! You provided invalid arguments for the day and/or part");
            return 1;
        }
        catch (TargetException e)
        {
            logger.GetLogger().Error(e,
                "Solution found for day {Day} and part {Part} but it does not have a solve method!", settings.Day,
                settings.Part);
            logger.GetLogger()
                .Error(
                    "This *is* an error! The solution class is not properly defined. Make sure it implements IAdventSolution");
            return 1;
        }
        catch (Exception e)
        {
            logger.GetLogger().Error(e, "An unknown error occurred");
        }

        return 0;
    }
}