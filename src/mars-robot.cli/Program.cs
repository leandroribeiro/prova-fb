// See https://aka.ms/new-console-template for more information

using mars_robot.core.Application.Exceptions;
using mars_robot.core.Application.UseCases;
using mars_robot.core.Domain.Exceptions;
using mars_robot.core.Infrastructure;

var parser = new ParseFileInstructions();

string GetInput(string[] parameters, bool forceUI)
{
    string input;
    
    if (forceUI == false && parameters.Length == 1)
    {
        input = parameters[0];
    }
    else
    {
        Console.WriteLine(Text.Green("Type the instructions filepath:"));
        input = Console.ReadLine() ?? "";
    }

    return input;
}

void Run(bool forceUI = false)
{
    try
    {
        var input = GetInput(args, forceUI);
        var output = parser.Execute(input);

        for (var index = 0; index < output.Rovers.Count; index++)
        {
            var roverID = index + 1;

            Console.WriteLine($"Rover {roverID}");
            Console.WriteLine(output.Rovers[index].ToString());
            Console.WriteLine("");
        }
    }
    catch (FileNotFoundException ex)
    {
        Console.Clear();
        Console.WriteLine(Text.Yellow($"Ops! The filepath '{ex.FileName}' informed doesn't exist, try again?"));
        Run(true);
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