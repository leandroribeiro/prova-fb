using System.Text.RegularExpressions;

namespace mars_robot.core.Domain.Models;

public sealed class Direction
{
    public const string REGEX_PATTERN = "[LRMlrm]";
    
    public const char LEFT = 'L';
    public const char RIGHT = 'R';
    public const char MOVE = 'M';

    public static bool IsValid(char direction)
    {
        var match = Regex.Match(direction.ToString(), REGEX_PATTERN, RegexOptions.IgnoreCase);

        return match.Success;
    }
}