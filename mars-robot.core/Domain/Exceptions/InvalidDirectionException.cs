namespace mars_robot.core.Domain.Exceptions;

public class InvalidDirectionException : ArgumentOutOfRangeException
{
    private const string paramName = "Direction";
    private const string message = "The informed direction is invalid.";

    public InvalidDirectionException(char actualValue) : base(paramName, actualValue, message)
    {
    }
}