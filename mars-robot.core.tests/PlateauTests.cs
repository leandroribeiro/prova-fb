using System;
using System.Collections.Generic;
using mars_robot.core.Models;
using Xunit;

namespace mars_robot.core.tests;

public class PlateauTests
{
    [Fact]
    public void Tests()
    {
        var plateau = new Plateau(5, 5);
        var rover = new Rover(1, 2, 'N', "LMLMLMLMM");

        plateau.AddRover(ref rover);

        rover.Run(); // TODO async Task

        Assert.Equal(CardinalPoint.CARDINAL_NORTH, rover.Cardinal.Key);
        Assert.Equal(1, rover.X);
        Assert.Equal(3, rover.Y);
    }
}

public class Plateau
{
    private readonly int _maxX;
    private readonly int _maxY;
    private List<Rover> rovers;

    public Plateau(int maxX, int maxY)
    {
        _maxX = maxX;
        _maxY = maxY;
        this.rovers = new List<Rover>();
    }

    public void AddRover(ref Rover rover)
    {
        this.rovers.Add(rover);
    }
}