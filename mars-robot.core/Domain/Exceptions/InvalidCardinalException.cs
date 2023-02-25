namespace mars_robot.core.Domain.Exceptions;

public class InvalidCardinalException : ArgumentOutOfRangeException
{
    private const string paramName = "Cardinal";
    private const string message = "The informed cardinal point is invalid.";

    public InvalidCardinalException(char actualValue) : base(paramName, actualValue, message)
    {
    }
}