using mars_robot.core.Domain.Models;

namespace mars_robot.core.UseCases;

public interface IParseInstructions
{
    void Execute<T>(T source);
}