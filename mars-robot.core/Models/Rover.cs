namespace mars_robot.core.Models;

public class Rover
{
    private readonly string _commands;
    const char DIRECTION_LEFT_KEY = 'L';
    const char DIRECTION_RIGHT_KEY = 'R';
    const char DIRECTION_MOVE_KEY = 'M';

    public int X { private set; get; }
    public int Y { private set; get; }
    public CardinalPoint Cardinal { private set; get; }

    public Rover(int x, int y, char cardinalPoint, string commands)
    {
        X = x;
        Y = y;
        Cardinal = CardinalPoint.Parse(cardinalPoint);
        _commands = commands;
    }

    private void increaseY()
    {
        this.Y += 1;
    }
    private void decreaseY()
    {
        this.Y -= 1;
    }
    
    private void increaseX()
    {
        this.X += 1;
    }
    
    private void decreaseX()
    {
        this.X -= 1;
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
                // TODO is?
                switch (Cardinal.Key)
                {
                    case CardinalPoint.CARDINAL_NORTH:
                        increaseY();
                        break;
                    case CardinalPoint.CARDINAL_SOUTH:
                        decreaseY();
                        break;
                    case CardinalPoint.CARDINAL_EAST:
                        increaseX();
                        break;
                    case CardinalPoint.CARDINAL_WEST:
                        decreaseX();
                        break;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(direction.ToString(), "A direção informada é inválida.");
        }
    }

    public void Run()
    {
        var commands = this._commands.ToCharArray();

        foreach (var command in commands)
        {
            SetDirection(command);
        }
    }
}