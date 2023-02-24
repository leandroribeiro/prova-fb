namespace mars_robot.core.Models;

public class CardinalPoint
{
    public const char CARDINAL_NORTH = 'N';
    public const char CARDINAL_SOUTH = 'S';
    public const char CARDINAL_EAST = 'E';
    public const char CARDINAL_WEST = 'W';

    public int Index { private set; get; }
    public char Key { private set; get; }

    public static readonly CardinalPoint North = new(1, CARDINAL_NORTH);
    public static readonly CardinalPoint East = new(2, CARDINAL_EAST);
    public static readonly CardinalPoint South = new(3, CARDINAL_SOUTH);
    public static readonly CardinalPoint West = new(4, CARDINAL_WEST);

    public static readonly IEnumerable<CardinalPoint> _all = new[] { North, South, East, West };

    public CardinalPoint(int index, char key)
    {
        Index = index;
        Key = key;
    }

    public static CardinalPoint Parse(char cardinal)
    {
        return cardinal switch
        {
            CARDINAL_NORTH => North,
            CARDINAL_SOUTH => South,
            CARDINAL_EAST => East,
            CARDINAL_WEST => West,
            _ => throw new ArgumentOutOfRangeException(cardinal.ToString(), "O cardinal informado é inválido.")
        };
    }

    public static CardinalPoint Get(int index)
    {
        return _all.First(x => x.Index == index);
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