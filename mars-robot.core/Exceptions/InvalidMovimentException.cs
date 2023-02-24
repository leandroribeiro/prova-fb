namespace mars_robot.core.Exceptions;

public class InvalidMovimentException : ArgumentOutOfRangeException
{
    private const string message = "Movimento não permitido pois está fora dos limites do Plateau.";

    public InvalidMovimentException(string paramName, int actualValue) : base(paramName, actualValue, message)
    {
    }
}