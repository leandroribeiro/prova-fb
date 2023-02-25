namespace mars_robot.core.Infrastructure;

public static class Text
{
    public static string Red(string message)
    {
        return $"\u001b[31m{message}\u001b[0m";
    }

    public static string Green(string message)
    {
        return $"\u001b[32m{message}\u001b[0m";
    }
    
    public static string Yellow(string message)
    {
        return $"\u001b[33m{message}\u001b[0m";
    }
    
}