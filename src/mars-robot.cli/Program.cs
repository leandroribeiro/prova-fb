// See https://aka.ms/new-console-template for more information

using mars_robot.core.Application.Exceptions;
using mars_robot.core.Application.UseCases;
using mars_robot.core.Domain.Exceptions;
using mars_robot.core.Infrastructure;

var parser = new ParseFileInstructions();

void Run()
{
    try
    {
        Console.WriteLine(Text.Green("Type the instructions filepath:"));

        var input = Console.ReadLine() ?? "";
        var output = parser.Execute(input);
        
        output.Rovers.ForEach(x => Console.WriteLine(x.ToString()));
    }
    catch (FileNotFoundException ex)
    {
        Console.Clear();
        Console.WriteLine(Text.Yellow($"Ops! The filepath '{ex.FileName}' informed doesn't exist, try again?"));
        Run();
    }
    catch (InvalidLineException ex)
    {
        Console.WriteLine(Text.Red("Ops! We find an error during parse!"));
        Console.WriteLine(Text.Red($"{ex.Message}"));
    }
    catch (Exception ex) when (ex is InvalidCardinalException or InvalidDirectionException or InvalidMovementException)
    {
        Console.WriteLine(Text.Red("Ops! We find an error in the instructions!"));
        Console.WriteLine(Text.Red($"Rover Start: {parser.CurrentRover.StartPosition.ToString()}"));
        Console.WriteLine(Text.Red($"Rover Last: {parser.CurrentRover.ToString()}"));
        Console.WriteLine(Text.Red($"Rover Commands: {parser.CurrentRover.Commands}"));
        Console.WriteLine(Text.Red($"{ex.Message}"));
    }
    catch (Exception e)
    {
        Console.WriteLine(Text.Red($"{e}"));
        throw;
    }
}

Run();