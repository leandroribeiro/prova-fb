namespace mars_robot.core.Models;

public class Plateau
{
    // x axis
    public int AxisXMin;
    public int AxisXMax { private set; get; }
    
    // y axis
    public int AxisYMin;
    public int AxisYMax { private set; get; }
    public List<Rover> Rovers { private set; get; }

    public Plateau(int axisXMax, int axisYMax)
    {
        AxisXMin = 0;
        AxisXMax = axisXMax;
        AxisYMin = 0;
        AxisYMax = axisYMax;
        this.Rovers = new List<Rover>();
    }

    public void AddRover(ref Rover rover)
    {
        rover.SetPlateau(this);
        this.Rovers.Add(rover);
    }
}