using mars_robot.core.Exceptions;

namespace mars_robot.core.Models;

public class Rover
{
    const char DIRECTION_LEFT_KEY = 'L';
    const char DIRECTION_RIGHT_KEY = 'R';
    const char DIRECTION_MOVE_KEY = 'M';

    public int X { private set; get; }
    public int Y { private set; get; }
    public CardinalPoint Cardinal { private set; get; }
    public string Commands { private set; get; }
    public Plateau Plateau { private set; get; }

    public Rover(int x, int y, char cardinalPoint, string commands, Plateau plateau)
    {
        X = x;
        Y = y;
        Cardinal = CardinalPoint.Parse(cardinalPoint);
        Commands = commands;
        Plateau = plateau;
    }

    private void IncreaseY()
    {
        var newValue = this.Y + 1;

        if (newValue > this.Plateau.AxisYMax)
            throw new InvalidMovimentException("Y", newValue);

        this.Y = newValue;
    }

    private void DecreaseY()
    {
        var newValue = this.Y - 1;

        if (newValue < this.Plateau.AxisYMin)
            throw new InvalidMovimentException("Y", newValue);

        this.Y = newValue;
    }

    private void IncreaseX()
    {
        var newValue = this.X + 1;

        if (newValue > this.Plateau.AxisXMax)
            throw new InvalidMovimentException("X", newValue);

        this.X = newValue;
    }

    private void DecreaseX()
    {
        var newValue = this.X - 1;

        if (newValue < this.Plateau.AxisXMin)
            throw new InvalidMovimentException("X", newValue);

        this.X = newValue;
    }

    private void SetDirection(char direction)
    {
        switch (direction)
        {
            case DIRECTION_LEFT_KEY:
            {
                Cardinal = Cardinal.MoveToLeft();
                break;
            }
            case DIRECTION_RIGHT_KEY:
            {
                Cardinal = Cardinal.MoveToRight();
                break;
            }
            case DIRECTION_MOVE_KEY:
            {
                MoveAhead();
                break;
            }
            default:
                throw new InvalidDirectionException(direction);
        }
    }

    private void MoveAhead()
    {
        // TODO is?
        switch (Cardinal.Key)
        {
            case CardinalPoint.CARDINAL_NORTH:
                IncreaseY();
                break;
            case CardinalPoint.CARDINAL_SOUTH:
                DecreaseY();
                break;
            case CardinalPoint.CARDINAL_EAST:
                IncreaseX();
                break;
            case CardinalPoint.CARDINAL_WEST:
                DecreaseX();
                break;
        }
    }

    public void Run()
    {
        var commands = this.Commands.ToCharArray();

        foreach (var command in commands)
            SetDirection(command);
    }

    public void SetPlateau(Plateau plateau)
    {
        this.Plateau = plateau;
    }
}