namespace mars_robot.core.Models;

public struct Cardinal
{
    private const char CARDINAL_NORTH = 'N';
    private const char CARDINAL_SOUTH = 'S';
    private const char CARDINAL_EAST = 'E';
    private const char CARDINAL_WEST = 'W';

    public enum CardinalPoints
    {
        North = 1,
        East = 2, // leste
        South = 3,
        West = 4, // oeste
    }

    public static CardinalPoints ParseDirection(char cardinal)
    {
        return cardinal switch
        {
            CARDINAL_NORTH => CardinalPoints.North,
            CARDINAL_SOUTH => CardinalPoints.South,
            CARDINAL_EAST => CardinalPoints.East,
            CARDINAL_WEST => CardinalPoints.West,
            _ => throw new ArgumentOutOfRangeException(cardinal.ToString(), "O cardinal informado é inválido.")
        };
    }
}