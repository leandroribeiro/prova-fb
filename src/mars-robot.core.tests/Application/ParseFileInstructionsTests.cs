using System;
using System.Linq;
using mars_robot.core.Application.UseCases;
using mars_robot.core.Domain.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace mars_robot.core.tests.Application;

public class ParseFileInstructionsTests
{
    private readonly ITestOutputHelper _output;

    public ParseFileInstructionsTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Parse_Two_Rovers_Commands_Test()
    {
        // ARRANGE
        var path = Environment.CurrentDirectory;
        var filePath = $"{path}/Data/Demo1.txt";

        var parser = new ParseFileInstructions();

        // ACT
        var plateau = parser.Execute(filePath);

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
    public void Parse_With_Invalid_Movement_Test()
    {
        // ARRANGE
        var path = Environment.CurrentDirectory;
        var filePath = $"{path}/Data/Demo2.txt";

        var parser = new ParseFileInstructions();

        // ACT
        // ASSERT
        Assert.Throws<InvalidMovementException>("Y", () => parser.Execute(filePath));
        Assert.NotNull(parser.CurrentRover);
        Assert.NotNull(parser.CurrentRover.StartPosition);
        Assert.NotEmpty(parser.CurrentRover.Commands);
        
        
    }
    
}