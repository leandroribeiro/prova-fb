namespace mars_robot.core.Domain.Exceptions;

public class InvalidMovementException : ArgumentOutOfRangeException
{
    private const string message = "Invalid movement, this is out of Plateau limits.";

    public InvalidMovementException(string paramName, int actualValue) : base(paramName, actualValue, message)
    {
    }
}