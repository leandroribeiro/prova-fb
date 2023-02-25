using mars_robot.core.Domain.Exceptions;

namespace mars_robot.core.Domain.Models;

public class Position
{
    public const string AXIS_X_KEY = "X";
    public const string AXIS_Y_KEY = "Y";

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    private int _axisXMin = -1;
    private int _axisXMax = -1;

    private int _axisYMin = -1;
    private int _axisYMax = -1;

    public int X { get; private set; }
    public int Y { get; private set; }

    public void SetAxisXLimits(int min, int max)
    {
        _axisXMin = min;
        _axisXMax = max;
    }

    public void SetAxisYLimits(int min, int max)
    {
        _axisYMin = min;
        _axisYMax = max;
    }

    public void IncreaseY()
    {
        SetAxisY(Y + 1);
    }

    public void DecreaseY()
    {
        SetAxisY(Y - 1);
    }

    public void IncreaseX()
    {
        SetAxisX(X + 1);
    }

    public void DecreaseX()
    {
        SetAxisX(X - 1);
    }

    private void SetAxisX(int value)
    {
        if (_axisXMax > -1 && value > _axisXMax)
            throw new InvalidMovementException(Position.AXIS_X_KEY, value);

        if (_axisXMin > -1 && value < _axisXMin)
            throw new InvalidMovementException(Position.AXIS_X_KEY, value);

        this.X = value;
    }


    private void SetAxisY(int value)
    {
        if (_axisYMax > -1 && value > _axisYMax)
            throw new InvalidMovementException(Position.AXIS_Y_KEY, value);

        if (_axisYMin > -1 && value < _axisYMin)
            throw new InvalidMovementException(Position.AXIS_Y_KEY, value);

        this.Y = value;
    }

    public override string ToString()
    {
        return $"{this.X} {this.Y}";
    }
}