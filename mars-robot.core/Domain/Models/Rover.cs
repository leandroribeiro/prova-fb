using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class Rover
{
    const string AXIS_X = "X";
    const string AXIS_Y = "Y";
    
    public Rover(int x, int y, char cardinalPoint, string commands, Plateau plateau)
    {
        Plateau = plateau;
        X = x;
        Y = y;
        Cardinal = CardinalPoint.Parse(cardinalPoint);
        Commands = commands.ToUpper();
        
        Plateau.AddRover(this);
    }

    private int _x;

    public int X
    {
        private set
        {
            if (value > Plateau.AxisXMax)
                throw new InvalidMovementException(AXIS_X, value);
            else if (value < Plateau.AxisXMin)
                throw new InvalidMovementException(AXIS_X, value);

            _x = value;
        }
        get => _x;
    }

    private int _y;

    public int Y
    {
        private set
        {
            if (value > Plateau.AxisYMax)
                throw new InvalidMovementException(AXIS_Y, value);
            else if (value < Plateau.AxisYMin)
                throw new InvalidMovementException(AXIS_Y, value);

            _y = value;
        }
        get => _y;
    }

    public CardinalPoint Cardinal { private set; get; }

    public string Commands { private set; get; }

    public Plateau Plateau { private set; get; }

    private void IncreaseY()
    {
        Y += 1;
    }

    private void DecreaseY()
    {
        Y -= 1;
    }

    private void IncreaseX()
    {
        X += 1;
    }

    private void DecreaseX()
    {
        X -= 1;
    }

    private void SetDirection(char direction)
    {
        if (!Direction.IsValid(direction))
            throw new InvalidDirectionException(direction);

        switch (direction)
        {
            case Direction.LEFT:
            {
                Cardinal = Cardinal.MoveToLeft();
                break;
            }
            case Direction.RIGHT:
            {
                Cardinal = Cardinal.MoveToRight();
                break;
            }
            case Direction.MOVE:
            {
                MoveAhead();
                break;
            }
        }
    }

    private void MoveAhead()
    {
        switch (Cardinal.Key)
        {
            case CardinalPoint.NORTH:
                IncreaseY();
                break;
            case CardinalPoint.SOUTH:
                DecreaseY();
                break;
            case CardinalPoint.EAST:
                IncreaseX();
                break;
            case CardinalPoint.WEST:
                DecreaseX();
                break;
        }
    }

    public void Run()
    {
        var commands = Commands.ToCharArray();

        foreach (var command in commands)
            SetDirection(command);
    }

    public override string ToString()
    {
        return $"{AXIS_X}: {this.X} {Environment.NewLine}" +
               $"{AXIS_Y}: {this.Y} {Environment.NewLine}" +
               $"Cardinal: {this.Cardinal.Key}";
    }
}