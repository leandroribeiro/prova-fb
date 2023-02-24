using System.Text.RegularExpressions;
using mars_robot.core.Domain.Models;

namespace mars_robot.core.Application.UseCases;

public class ParseStringInstructions : IParseInstructions
{
    private Plateau Plateau { set; get; }

    public Plateau Execute(string source)
    {
        var lines = source
            .Split(Environment.NewLine);

        ParseHeader(lines);

        ParseLines(lines);

        Plateau.Rovers.ForEach(r => r.Run());

        return Plateau;
    }
    
    private void ParseHeader(string[] lines)
    {
        var head = lines.First();

        this.Plateau = ParsePlateau(head);
    }

    private void ParseLines(string[] lines)
    {
        for (var i = 1; i < lines.Length; i += 2)
        {
            var roverPoints = lines[i];
            var roverCommands = lines[i + 1];

            ParseRover(roverPoints, roverCommands);

        }
    }

    private Plateau ParsePlateau(string head)
    {
        var match = Regex.Match(head, "(?<x>[0-9])\\s+(?<y>[0-9])");

        if (!match.Success)
            throw new InvalidDataException();

        var xAxis = int.Parse(match.Groups["x"].Value);
        var yAxis = int.Parse(match.Groups["y"].Value);

        return new Plateau(xAxis, yAxis);
    }

    private Rover ParseRover(string roverPoints, string roverCommands)
    {
        var roverPointsLine = roverPoints;
        var roverPointsReg = $"(?<x>[0-9])\\s+(?<y>[0-9])\\s+(?<card>{CardinalPoint.REGEX_PATTERN})";
        var roverPointsMatch = Regex.Match(roverPointsLine, roverPointsReg, RegexOptions.IgnoreCase);

        if (!roverPointsMatch.Success)
            throw new InvalidDataException();

        var xAxis = int.Parse(roverPointsMatch.Groups["x"].Value);
        var yAxis = int.Parse(roverPointsMatch.Groups["y"].Value);
        var cardinal = char.Parse(roverPointsMatch.Groups["card"].Value);
        var commands = ParseRoverCommands(roverCommands);

        return new Rover(xAxis, yAxis, cardinal, commands, Plateau);
    }

    private string ParseRoverCommands(string line)
    {
        var commands = "";
        var roverCommandLine = line;
        var roverCommandReg = $"{Direction.REGEX_PATTERN}+";
        var roverCommandMatch = Regex.Match(roverCommandLine, roverCommandReg, RegexOptions.IgnoreCase);
        if (roverCommandMatch.Success)
            commands = roverCommandMatch.Value;

        return commands;
    }
}