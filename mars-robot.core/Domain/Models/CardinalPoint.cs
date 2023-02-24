using System.Text.RegularExpressions;
using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class CardinalPoint
{
    public const string REGEX_PATTERN = "[NSEWnsew]";
    
    public const char NORTH = 'N';
    public const char SOUTH = 'S';
    public const char EAST = 'E';
    public const char WEST = 'W';

    public static readonly CardinalPoint North = new(1, NORTH);
    public static readonly CardinalPoint East = new(2, EAST);
    public static readonly CardinalPoint South = new(3, SOUTH);
    public static readonly CardinalPoint West = new(4, WEST);

    public static readonly IEnumerable<CardinalPoint> Points = new[] { North, South, East, West };

    public CardinalPoint(int index, char key)
    {
        Index = index;
        Key = key;
    }

    public int Index { get; }
    public char Key { get; }

    public static bool IsValid(char cardinal)
    {
        var match = Regex.Match(cardinal.ToString(), REGEX_PATTERN, RegexOptions.IgnoreCase);

        return match.Success;
    }
    
    public static CardinalPoint Parse(char cardinal)
    {
        if (!IsValid(cardinal))
            throw new InvalidCardinalException(cardinal);
        
        return cardinal switch
        {
            NORTH => North,
            SOUTH => South,
            EAST => East,
            WEST => West,
            _ => throw new InvalidCardinalException(cardinal)
        };
    }

    public static CardinalPoint Get(int index)
    {
        return Points.First(x => x.Index == index);
    }

    public CardinalPoint MoveToLeft()
    {
        var currentIndex = Index;

        if (currentIndex > North.Index)
            currentIndex -= 1;
        else
            currentIndex = West.Index;

        return Get(currentIndex);
    }

    public CardinalPoint MoveToRight()
    {
        var currentIndex = Index;

        if (currentIndex < West.Index)
            currentIndex += 1;
        else
            currentIndex = North.Index;

        return Get(currentIndex);
    }
}