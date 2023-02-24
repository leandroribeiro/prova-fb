namespace mars_robot.core.Exceptions;

public class InvalidDirectionException : ArgumentOutOfRangeException
{
    private const string paramName = "Direction";
    private const string message = "A direção informada é inválida.";

    public InvalidDirectionException(char actualValue) : base(paramName, actualValue, message)
    {
    }
}