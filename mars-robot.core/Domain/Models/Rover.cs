using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class Rover
{
    public Position StartPosition { private set; get; }
    public Position CurrentPosition { private set; get; }

    public Rover(int x, int y, char cardinalPoint, string commands, Plateau plateau)
    {
        Plateau = plateau;
        StartPosition = new Position(x, y, cardinalPoint);
        CurrentPosition = new Position(x, y, cardinalPoint, plateau);
        Commands = commands.ToUpper();

        Plateau.AddRover(this);
    }

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
                CurrentPosition.MoveToLeft();
                break;
            }
            case Direction.RIGHT:
            {
                CurrentPosition.MoveToRight();
                break;
            }
            case Direction.MOVE:
            {
                CurrentPosition.MoveAhead();
                break;
            }
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
        return $"{this.CurrentPosition.ToString()}";
    }
}