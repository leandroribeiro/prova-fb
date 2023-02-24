using System;
using mars_robot.core.Models;
using Xunit;

namespace mars_robot.core.tests;

public class RoverTests
{
    [Theory]
    [InlineData(1, 2, 'N', 'R', Cardinal.CardinalPoints.East)]
    [InlineData(1, 2, 'E', 'R', Cardinal.CardinalPoints.South)]
    [InlineData(1, 2, 'S', 'R', Cardinal.CardinalPoints.West)]
    [InlineData(1, 2, 'W', 'R', Cardinal.CardinalPoints.North)]
    [InlineData(1, 2, 'N', 'L', Cardinal.CardinalPoints.West)]
    [InlineData(1, 2, 'E', 'L', Cardinal.CardinalPoints.North)]
    [InlineData(1, 2, 'S', 'L', Cardinal.CardinalPoints.East)]
    [InlineData(1, 2, 'W', 'L', Cardinal.CardinalPoints.South)]
    public void Test_Single_Direction_Movement(int x, int y, char cardinal, char direction, Cardinal.CardinalPoints cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal);
        rover.SetDirection(direction);

        Assert.Equal(cardinalTarget, rover._cardinal);
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
    [InlineData(1, 2, 'N', new char[] { 'R', 'M' }, 2, 2, Cardinal.CardinalPoints.East)]
    [InlineData(1, 2, 'N', new char[] { 'L', 'L', 'L', 'L', }, 1, 2, Cardinal.CardinalPoints.North)]
    [InlineData(1, 2, 'N', new char[] { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' }, 1, 3, Cardinal.CardinalPoints.North)]
    [InlineData(3, 3, 'E', new char[] { 'M', 'M', 'R', 'M', 'M', 'R', 'M', 'R', 'R', 'M' }, 5, 1, Cardinal.CardinalPoints.East)]
    public void Test_Multiples_Direction_Movements(int x, int y, char cardinal, char[] direction, int targetX, int targetY,
        Cardinal.CardinalPoints cardinalTarget)
    {
        var rover = new Rover(x, y, cardinal);

        foreach (var d in direction)
            rover.SetDirection(d);

        Assert.Equal(cardinalTarget, rover._cardinal);
        Assert.Equal(targetX, rover.X);
        Assert.Equal(targetY, rover.Y);
    }
}