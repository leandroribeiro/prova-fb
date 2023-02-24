using System;
using mars_robot.core.Models;
using Xunit;

namespace mars_robot.core.tests;

public class RoverTests
{
    [Theory]
    [InlineData(1, 2, 'N', 'R', CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'E', 'R', CardinalPoint.CARDINAL_SOUTH)]
    [InlineData(1, 2, 'S', 'R', CardinalPoint.CARDINAL_WEST)]
    [InlineData(1, 2, 'W', 'R', CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'N', 'L', CardinalPoint.CARDINAL_WEST)]
    [InlineData(1, 2, 'E', 'L', CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'S', 'L', CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'W', 'L', CardinalPoint.CARDINAL_SOUTH)]
    public void Test_Single_Direction_Movement(int x, int y, char cardinal, char direction, char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal);
        rover.SetDirection(direction);

        Assert.Equal(cardinalTarget, rover.Cardinal.Key);
    }

    [Theory]
    [InlineData(1, 2, 'X')]
    [InlineData(1, 2, 'Y')]
    [InlineData(1, 2, 'Z')]
    public void Test_Invalid_Cardinal_Point(int x, int y, char cardinal)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Rover(x, y, cardinal));
    }

    [Theory]
    [InlineData(1, 2, 'N', 'X')]
    [InlineData(1, 2, 'E', 'Y')]
    [InlineData(1, 2, 'S', 'Z')]
    public void Test_Invalid_Direction_Movement(int x, int y, char cardinal, char direction)
    {
        var rover = new Rover(x, y, cardinal);

        Assert.Throws<ArgumentOutOfRangeException>(() => rover.SetDirection(direction));
    }


    [Theory]
    [InlineData(1, 2, 'N', new char[] { 'R', 'M' }, 2, 2, CardinalPoint.CARDINAL_EAST)]
    [InlineData(1, 2, 'N', new char[] { 'L', 'L', 'L', 'L', }, 1, 2, CardinalPoint.CARDINAL_NORTH)]
    [InlineData(1, 2, 'N', new char[] { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' }, 1, 3,
        CardinalPoint.CARDINAL_NORTH)]
    [InlineData(3, 3, 'E', new char[] { 'M', 'M', 'R', 'M', 'M', 'R', 'M', 'R', 'R', 'M' }, 5, 1,
        CardinalPoint.CARDINAL_EAST)]
    public void Test_Multiples_Direction_Movements(int x, int y, char cardinal, char[] direction, int targetX,
        int targetY,
        char cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal);

        foreach (var d in direction)
            rover.SetDirection(d);

        Assert.Equal(cardinalTarget, rover.Cardinal.Key);
        Assert.Equal(targetX, rover.X);
        Assert.Equal(targetY, rover.Y);
    }
}