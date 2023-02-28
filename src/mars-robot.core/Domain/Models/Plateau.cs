namespace mars_robot.core.Domain.Models;

public class Plateau
{
    public int AxisXMin;

    public int AxisYMin;

    public Plateau(int axisXMax, int axisYMax)
    {
        AxisXMin = 0;
        AxisXMax = axisXMax;
        AxisYMin = 0;
        AxisYMax = axisYMax;
        Rovers = new List<Rover>();
    }

    public int AxisXMax { get; }
    public int AxisYMax { get; }
    public List<Rover> Rovers { get; }

    public void AddRover(Rover rover)
    {
        Rovers.Add(rover);
    }
}