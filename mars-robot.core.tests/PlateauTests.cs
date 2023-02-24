using System.Linq;
using mars_robot.core.Exceptions;
using mars_robot.core.Models;
using Xunit;

namespace mars_robot.core.tests;

public class PlateauTests
{
    [Theory]
    [InlineData(1, 2, 'N', "LMLMLMLMM", 1, 3, CardinalPoint.CARDINAL_NORTH)]
    [InlineData(3, 3, 'E', "MMRMMRMRRM", 5, 1, CardinalPoint.CARDINAL_EAST)]
    public void Plateau_and_Single_Rover_Test(int roverX, int roverY, char roverCardinal, string roverCommands,
        int targetX, int targetY, char targetCardinal)
    {
        var plateau = new Plateau(5, 5);
        var roverOne = new Rover(roverX, roverY, roverCardinal, roverCommands, plateau);

        plateau.AddRover(ref roverOne);

        foreach (var rover in plateau.Rovers)
        {
            rover.Run();
        }

        Assert.Equal(targetCardinal, roverOne.Cardinal.Key);
        Assert.Equal(targetX, roverOne.X);
        Assert.Equal(targetY, roverOne.Y);
    }

    [Fact]
    public void Plateau_and_Multiple_Rover_Test()
    {
        var plateau = new Plateau(5, 5);
        var rovers = new[]
        {
            new Rover(1, 2, 'N', "LMLMLMLMM", plateau),
            new Rover(3, 3, 'E', "MMRMMRMRRM", plateau),
        };

        for (var i = 0; i < rovers.Length; i++)
            plateau.AddRover(ref rovers[i]);

        for (var i = 0; i < rovers.Length; i++)
            rovers[i].Run();

        var roverOne = rovers.First();
        var roverTwo = rovers.Last();

        Assert.Equal(CardinalPoint.CARDINAL_NORTH, roverOne.Cardinal.Key);
        Assert.Equal(1, roverOne.X);
        Assert.Equal(3, roverOne.Y);

        Assert.Equal(CardinalPoint.CARDINAL_EAST, roverTwo.Cardinal.Key);
        Assert.Equal(5, roverTwo.X);
        Assert.Equal(1, roverTwo.Y);

        var plateauRoverOne = plateau.Rovers.First();
        Assert.Equal(CardinalPoint.CARDINAL_NORTH, plateauRoverOne.Cardinal.Key);
        Assert.Equal(1, plateauRoverOne.X);
        Assert.Equal(3, plateauRoverOne.Y);

        var plateauRoverTwo = plateau.Rovers.Last();
        Assert.Equal(CardinalPoint.CARDINAL_EAST, plateauRoverTwo.Cardinal.Key);
        Assert.Equal(5, plateauRoverTwo.X);
        Assert.Equal(1, plateauRoverTwo.Y);
    }


    [Theory]
    [InlineData(1, 2, 'N', "LMLMLMLMMMMMMM", "Y")]
    [InlineData(3, 3, 'E', "MMRMMRMRRMMMMM", "X")]
    public void Rover_Runs_Out_of_Plateau_Limits(int roverX, int roverY, char roverCardinal, string roverCommands, string axiosError)
    {
        var plateau = new Plateau(5, 5);
        var roverOne = new Rover(roverX, roverY, roverCardinal, roverCommands, plateau);

        plateau.AddRover(ref roverOne);

        Assert.Throws<InvalidMovimentException>(axiosError, () => plateau.Rovers.First().Run());
    }
}