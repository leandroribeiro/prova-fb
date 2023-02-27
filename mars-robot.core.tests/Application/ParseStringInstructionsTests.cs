using System;
using System.Linq;
using mars_robot.core.Application.Exceptions;
using mars_robot.core.Application.UseCases;
using mars_robot.core.Domain.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace mars_robot.core.tests.Application;

public class ParseStringInstructionsTests
{
    private readonly ITestOutputHelper _output;
    private readonly ParseStringInstructions _parser;

    public ParseStringInstructionsTests(ITestOutputHelper output)
    {
        _output = output;
        _parser =  new ParseStringInstructions();
    }

    [Fact]
    public void Parse_Two_Rovers_Commands_Test()
    {
        // ARRANGE
        var input = $"5 5 {Environment.NewLine}" +
                    $"1 2 N {Environment.NewLine}" +
                    $"LMLMLMLMM {Environment.NewLine}" +
                    $"3 3 E {Environment.NewLine}" +
                    $"MMRMMRMRRM";

        // ACT
        var plateau = _parser.Execute(input);

        // ASSERT
        plateau.Rovers.ForEach(r => _output.WriteLine(r.ToString()));
        
        Assert.Equal(5, plateau.AxisXMax);
        Assert.Equal(5, plateau.AxisYMax);
        Assert.Equal(2, plateau.Rovers.Count);

        var roverOne = plateau.Rovers.First();
        Assert.Equal("LMLMLMLMM", roverOne.Commands);
        Assert.Equal('N', roverOne.CurrentPosition.Cardinal.Key);
        Assert.Equal(1, roverOne.CurrentPosition.X);
        Assert.Equal(3, roverOne.CurrentPosition.Y);

        var roverTwo = plateau.Rovers.Last();
        Assert.Equal("MMRMMRMRRM", roverTwo.Commands);
        Assert.Equal('E', roverTwo.CurrentPosition.Cardinal.Key);
        Assert.Equal(5, roverTwo.CurrentPosition.X);
        Assert.Equal(1, roverTwo.CurrentPosition.Y);
    }
    
    
    [Fact]
    public void Parse_Incomplete_Rovers_Test()
    {
        // ARRANGE
        var input = $"5 5 {Environment.NewLine}" +
                    $"1 2 {Environment.NewLine}" +
                    $"LMLMLMLMM {Environment.NewLine}" +
                    $"3 E {Environment.NewLine}" +
                    $"MMRMMRMRRM";

        // ACT
        Assert.Throws<InvalidLineException>("Line", () => _parser.Execute(input));
    }
    
    [Fact]
    public void Parse_Invalid_Plateau_Test()
    {
        // ARRANGE
        var input = $"5 AAA 5 {Environment.NewLine}" +
                    $"1 2 N {Environment.NewLine}" +
                    $"LMLMLMLMM {Environment.NewLine}" +
                    $"3 3 E {Environment.NewLine}" +
                    $"MMRMMRMRRM";

        // ACT
        Assert.Throws<InvalidLineException>("Line", () => _parser.Execute(input));
    }
}