using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class Rover
{
    public Position StartPosition { private set; get; }
    public Position CurrentPosition { private set; get; }

    public Rover(int x, int y, char cardinalPoint, string commands, Plateau plateau)
    {
        Plateau = plateau;
        StartPosition = new Position(x, y);
        // StartPosition.SetAxisXLimits(plateau.AxisXMin, plateau.AxisXMax);
        // StartPosition.SetAxisYLimits(plateau.AxisYMin, plateau.AxisYMax);
        CurrentPosition = new Position(x, y);
        CurrentPosition.SetAxisXLimits(plateau.AxisXMin, plateau.AxisXMax);
        CurrentPosition.SetAxisYLimits(plateau.AxisYMin, plateau.AxisYMax);
        Cardinal = CardinalPoint.Parse(cardinalPoint);
        Commands = commands.ToUpper();

        Plateau.AddRover(this);
    }

    public CardinalPoint Cardinal { private set; get; }

    public string Commands { private set; get; }

    public Plateau Plateau { private set; get; }

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
                CurrentPosition.IncreaseY();
                break;
            case CardinalPoint.SOUTH:
                CurrentPosition.DecreaseY();
                break;
            case CardinalPoint.EAST:
                CurrentPosition.IncreaseX();
                break;
            case CardinalPoint.WEST:
                CurrentPosition.DecreaseX();
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
        return $"{this.CurrentPosition.ToString()} {this.Cardinal.Key}";
    }
}