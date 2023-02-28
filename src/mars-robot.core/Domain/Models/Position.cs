using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class Position
{
    public const string AXIS_X_KEY = "X";
    public const string AXIS_Y_KEY = "Y";

    #region Fields

    private readonly int _axisXMin = -1;
    private readonly int _axisXMax = -1;

    private readonly int _axisYMin = -1;
    private readonly int _axisYMax = -1;

    #endregion

    #region Properties

    private int _x;

    public int X
    {
        get => this._x;
        private set
        {
            if (_axisXMax > -1 && value > _axisXMax)
                throw new InvalidMovementException(Position.AXIS_X_KEY, value);

            if (_axisXMin > -1 && value < _axisXMin)
                throw new InvalidMovementException(Position.AXIS_X_KEY, value);

            this._x = value;
        }
    }

    private int _y;

    public int Y
    {
        get => _y;
        private set
        {
            if (_axisYMax > -1 && value > _axisYMax)
                throw new InvalidMovementException(Position.AXIS_Y_KEY, value);

            if (_axisYMin > -1 && value < _axisYMin)
                throw new InvalidMovementException(Position.AXIS_Y_KEY, value);

            this._y = value;
        }
    }

    public CardinalPoint Cardinal { private set; get; }

    #endregion

    #region Constructor

    public Position(int x, int y, char cardinalPoint, Plateau plateau) : this(x, y, cardinalPoint)
    {
        _axisXMin = plateau.AxisXMin;
        _axisXMax = plateau.AxisXMax;
        _axisYMin = plateau.AxisYMin;
        _axisYMax = plateau.AxisYMax;
    }

    public Position(int x, int y, char cardinalPoint)
    {
        X = x;
        Y = y;
        Cardinal = CardinalPoint.Parse(cardinalPoint);
    }

    #endregion

    public void MoveToLeft()
    {
        Cardinal = Cardinal.MoveToLeft();
    }

    public void MoveToRight()
    {
        Cardinal = Cardinal.MoveToRight();
    }

    public void MoveAhead()
    {
        switch (Cardinal.Key)
        {
            case CardinalPoint.NORTH:
                Y += 1;
                break;
            case CardinalPoint.EAST:
                X += 1;
                break;
            case CardinalPoint.SOUTH:
                Y -= 1;
                break;
            case CardinalPoint.WEST:
                X -= 1;
                break;
        }
    }

    public override string ToString()
    {
        return $"{this.X} {this.Y} {this.Cardinal.Key}";
    }
}