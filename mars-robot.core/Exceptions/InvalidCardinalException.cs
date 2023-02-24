namespace mars_robot.core.Exceptions;

public class InvalidCardinalException : ArgumentOutOfRangeException
{
    private const string paramName = "Cardinal";
    private const string message = "O cardinal informado é inválido.";
    
    public InvalidCardinalException(char actualValue): base(paramName, actualValue, message)
    {
        
    }
}