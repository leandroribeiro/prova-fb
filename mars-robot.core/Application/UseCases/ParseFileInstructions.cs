using mars_robot.core.Domain.Models;

namespace mars_robot.core.Application.UseCases;

public class ParseFileInstructions : ParseStringInstructions
{
    public ParseFileInstructions()
    {
    }

    public Plateau Execute(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        var content = File.ReadAllText(filePath);

        base.Execute(content);

        return this.Plateau;
    }
}