using System;
using System.Linq;
using mars_robot.core.Application.UseCases;
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
        Assert.Equal('N', roverOne.Cardinal.Key);
        Assert.Equal(1, roverOne.X);
        Assert.Equal(3, roverOne.Y);

        var roverTwo = plateau.Rovers.Last();
        Assert.Equal("MMRMMRMRRM", roverTwo.Commands);
        Assert.Equal('E', roverTwo.Cardinal.Key);
        Assert.Equal(5, roverTwo.X);
        Assert.Equal(1, roverTwo.Y);
    }
    
}