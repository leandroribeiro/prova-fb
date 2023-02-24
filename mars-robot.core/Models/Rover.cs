namespace mars_robot.core.Models;

public class Rover
{
    const char DIRECTION_LEFT_KEY = 'L';
    const char DIRECTION_RIGHT_KEY = 'R';
    const char DIRECTION_MOVE_KEY = 'M';

    public int X { private set; get; }
    public int Y { private set; get; }
    public Cardinal.CardinalPoints _cardinal { private set; get; }

    public Rover(int x, int y, char direction)
    {
        X = x;
        Y = y;
        _cardinal = Cardinal.ParseDirection(direction);
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
    
    public void SetDirection(char direction)
    {
        switch (direction)
        {
            case DIRECTION_LEFT_KEY:
            {
                var cardinalNumber = (int)_cardinal;
                cardinalNumber = (cardinalNumber > (int)Cardinal.CardinalPoints.North
                    ? cardinalNumber - 1
                    : (int)Cardinal.CardinalPoints.West);
                _cardinal = (Cardinal.CardinalPoints)cardinalNumber;
                break;
            }
            case DIRECTION_RIGHT_KEY:
            {
                var cardinalNumber = (int)_cardinal;
                cardinalNumber = (cardinalNumber < (int)Cardinal.CardinalPoints.West
                    ? cardinalNumber + 1
                    : (int)Cardinal.CardinalPoints.North);
                _cardinal = (Cardinal.CardinalPoints)cardinalNumber;
                break;
            }
            case DIRECTION_MOVE_KEY:
            {
                switch (_cardinal)
                {
                    case Cardinal.CardinalPoints.North:
                        increaseY();
                        break;
                    case Cardinal.CardinalPoints.South:
                        decreaseY();
                        break;
                    case Cardinal.CardinalPoints.East:
                        increaseX();
                        break;
                    case Cardinal.CardinalPoints.West:
                        decreaseX();
                        break;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(direction.ToString(), "A direção informada é inválida.");
        }
    }
}