using mars_robot.core.Domain.Models;

namespace mars_robot.core.Application.UseCases;

public class ParseFileInstructions : IParseInstructions
{
    private readonly ParseStringInstructions _parse;

    public ParseFileInstructions()
    {
        _parse = new ParseStringInstructions();
    }
    
    public Plateau Execute(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();
        
        var content = File.ReadAllText(filePath);
        
        return _parse.Execute(content);
    }
}