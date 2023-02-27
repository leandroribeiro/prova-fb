using System.Drawing;

namespace mars_robot.core.Infrastructure;

public static class Text
{
    public const int RED_CODE = 31;
    public const int GREEN_CODE = 32;
    public const int YELLOW_CODE = 33;
    
    public static string Red(string message)
    {
        return $"\u001b[{RED_CODE}m{message}\u001b[0m";
    }

    public static string Green(string message)
    {
        return $"\u001b[{GREEN_CODE}m{message}\u001b[0m";
    }
    
    public static string Yellow(string message)
    {
        return $"\u001b[{YELLOW_CODE}m{message}\u001b[0m";
    }
    
}