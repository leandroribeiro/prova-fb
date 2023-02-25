using mars_robot.core.Domain.Models;

namespace mars_robot.core.Application.UseCases;

public interface IParseInstructions
{
    public Plateau Plateau { get; }
    public Rover CurrentRover { get; }
    Plateau Execute(string source);
}