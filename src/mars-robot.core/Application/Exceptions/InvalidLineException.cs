namespace mars_robot.core.Application.Exceptions;

public class InvalidLineException : ArgumentOutOfRangeException
{
    private const string paramName = "Line";
    private const string message = "The line data is invalid.";
    
    public InvalidLineException(string actualValue) : base(paramName, actualValue, message)
    {
        
    }
    
}