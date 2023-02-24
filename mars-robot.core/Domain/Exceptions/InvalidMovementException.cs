namespace mars_robot.core.Domain.Exceptions;

public class InvalidMovementException : ArgumentOutOfRangeException
{
    private const string message = "Movimento não permitido pois está fora dos limites do Plateau.";

    public InvalidMovementException(string paramName, int actualValue) : base(paramName, actualValue, message)
    {
    }
}