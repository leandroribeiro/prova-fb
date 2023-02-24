using System;
using mars_robot.core.Exceptions;
using mars_robot.core.Models;
using Xunit;

namespace mars_robot.core.tests;

public class RoverTests
{
    private readonly Plateau _basePlateau;

    public RoverTests()
    {
        this._basePlateau = new Plateau(5, 5);
    }

    
    [Theory]
    [InlineData(1, 2, 'N', "LMMMMMMMMMMMMMMMMMMMM", CardinalPoint.CARDINAL_NORTH)]
    public void Test_Moviment_out_Plateau_Min_AxisX(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        Assert.Throws<InvalidMovimentException>("X", () => rover.Run());
    }
    
    
    [Theory]
    [InlineData(1, 2, 'N', "RRMMMMMMMMMMMMMMMMMMMM", CardinalPoint.CARDINAL_NORTH)]
    public void Test_Moviment_out_Plateau_Min_AxisY(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        Assert.Throws<InvalidMovimentException>("Y", () => rover.Run());
    }
    
    
    [Theory]
    [InlineData(1, 2, 'N', "RMMMMMMMMMMMMMMMMMMMM", CardinalPoint.CARDINAL_NORTH)]
    public void Test_Moviment_out_Plateau_Max_AxisX(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        Assert.Throws<InvalidMovimentException>("X", () => rover.Run());
    }
    
    [Theory]
    [InlineData(1, 2, 'N', "LMLMLMLMMMMMMMMMMMMMMMMMMM", CardinalPoint.CARDINAL_NORTH)]
    public void Test_Moviment_out_Plateau_Max_AxisY(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        Assert.Throws<InvalidMovimentException>("Y", () => rover.Run());
    }

    [Theory]
    [InlineData(1, 2, 'N', "", CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'E', "", CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'S', "", CardinalPoint.CARDINAL_SOUTH)]
    [InlineData(1, 2, 'W', "", CardinalPoint.CARDINAL_WEST)]
    public void Test_Without_Movement(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        rover.Run();

        Assert.Equal(cardinalTarget, rover.Cardinal.Key);
        Assert.Equal(x, rover.X);
        Assert.Equal(y, rover.Y);
    }

    [Theory]
    [InlineData(1, 2, 'N', "R", CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'E', "R", CardinalPoint.CARDINAL_SOUTH)]
    [InlineData(1, 2, 'S', "R", CardinalPoint.CARDINAL_WEST)]
    [InlineData(1, 2, 'W', "R", CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'N', "L", CardinalPoint.CARDINAL_WEST)]
    [InlineData(1, 2, 'E', "L", CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'S', "L", CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'W', "L", CardinalPoint.CARDINAL_SOUTH)]
    public void Test_Single_Direction_Movement(int x, int y, char cardinal, string commands, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        rover.Run();

        Assert.Equal(cardinalTarget, rover.Cardinal.Key);
    }

    [Theory]
    [InlineData(1, 2, 'X')]
    [InlineData(1, 2, 'Y')]
    [InlineData(1, 2, 'Z')]
    [InlineData(1, 2, ' ')]
    public void Test_Invalid_Cardinal_Point(int x, int y, char cardinal)
    {
        Assert.Throws<InvalidCardinalException>(() => new Rover(x, y, cardinal, "", _basePlateau));
    }

    [Theory]
    [InlineData(1, 2, 'N', "X")]
    [InlineData(1, 2, 'E', "Y")]
    [InlineData(1, 2, 'S', "Z")]
    public void Test_Invalid_Direction_Movement(int x, int y, char cardinal, string commands)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        Assert.Throws<InvalidDirectionException>("Direction", () => rover.Run());
    }


    [Theory]
    [InlineData(1, 2, 'N', "RM", 2, 2, CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'N', "LLLL", 1, 2, CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'N', "LMLMLMLMM", 1, 3,
        CardinalPoint.CARDINAL_NORTH)]
    [InlineData(3, 3, 'E', "MMRMMRMRRM", 5, 1,
        CardinalPoint.CARDINAL_EAST)]
    public void Test_Multiples_Direction_Movements(int x, int y, char cardinal, string commands, int targetX,
        int targetY,
        char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal, commands, _basePlateau);

        rover.Run();

        Assert.Equal(cardinalTarget, rover.Cardinal.Key);
        Assert.Equal(targetX, rover.X);
        Assert.Equal(targetY, rover.Y);
    }
}