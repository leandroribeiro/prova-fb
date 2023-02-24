using System;
using System.Linq;
using mars_robot.core.UseCases;
using Xunit;
using Xunit.Abstractions;

namespace mars_robot.core.tests;

public class ParseStringInstructionsTests
{
    private readonly ITestOutputHelper _output;

    public ParseStringInstructionsTests(ITestOutputHelper output)
    {
        _output = output;
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

        var parser = new ParseStringInstructions();

        // ACT
        parser.Execute(input);

        // ASSERT
        var plateau = parser.Plateau;
        
        plateau.Rovers.ForEach(r => _output.WriteLine(r.ToString()));
        
        Assert.Equal(5, plateau.AxisXMax);
        Assert.Equal(5, plateau.AxisYMax);
        Assert.Equal(2, plateau.Rovers.Count);

        var roverOne = plateau.Rovers.First();
        Assert.Equal("LMLMLMLMM", roverOne.Commands);
        Assert.Equal('N', roverOne.CardinalPoint.Key);
        Assert.Equal(1, roverOne.X);
        Assert.Equal(3, roverOne.Y);

        var roverTwo = plateau.Rovers.Last();
        Assert.Equal("MMRMMRMRRM", roverTwo.Commands);
        Assert.Equal('E', roverTwo.CardinalPoint.Key);
        Assert.Equal(5, roverTwo.X);
        Assert.Equal(1, roverTwo.Y);
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

        var parser = new ParseStringInstructions();

        // ACT
        parser.Execute(input);

        // ASSERT
        var plateau = parser.Plateau;
        
        plateau.Rovers.ForEach(r => _output.WriteLine(r.ToString()));
        
        Assert.Equal(5, plateau.AxisXMax);
        Assert.Equal(5, plateau.AxisYMax);
        Assert.Equal(2, plateau.Rovers.Count);

        var roverOne = plateau.Rovers.First();
        Assert.Equal("LMLMLMLMM", roverOne.Commands);
        Assert.Equal('N', roverOne.CardinalPoint.Key);
        Assert.Equal(1, roverOne.X);
        Assert.Equal(3, roverOne.Y);

        var roverTwo = plateau.Rovers.Last();
        Assert.Equal("MMRMMRMRRM", roverTwo.Commands);
        Assert.Equal('E', roverTwo.CardinalPoint.Key);
        Assert.Equal(5, roverTwo.X);
        Assert.Equal(1, roverTwo.Y);
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

        var parser = new ParseStringInstructions();

        // ACT
        parser.Execute(input);

        // ASSERT
        var plateau = parser.Plateau;
        
        plateau.Rovers.ForEach(r => _output.WriteLine(r.ToString()));
        
        Assert.Equal(5, plateau.AxisXMax);
        Assert.Equal(5, plateau.AxisYMax);
        Assert.Equal(2, plateau.Rovers.Count);

        var roverOne = plateau.Rovers.First();
        Assert.Equal("LMLMLMLMM", roverOne.Commands);
        Assert.Equal('N', roverOne.CardinalPoint.Key);
        Assert.Equal(1, roverOne.X);
        Assert.Equal(3, roverOne.Y);

        var roverTwo = plateau.Rovers.Last();
        Assert.Equal("MMRMMRMRRM", roverTwo.Commands);
        Assert.Equal('E', roverTwo.CardinalPoint.Key);
        Assert.Equal(5, roverTwo.X);
        Assert.Equal(1, roverTwo.Y);
    }
}