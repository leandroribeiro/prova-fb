using mars_robot.core.Infrastructure;
using Xunit;

namespace mars_robot.core.tests.Infrastructure;

public class TextTests
{

    [Fact]
    public void Red_Test()
    {
        // ARRANGE
        var msg = "Test Text Red";
        
        // ACT
        var output = Text.Red(msg);
        
        // ASSERT
        Assert.Contains(msg, output);
        Assert.Contains(Text.RED_CODE.ToString(), output);
    }
    
    [Fact]
    public void Green_Test()
    {
        // ARRANGE
        var msg = "Test Text Green";
        
        // ACT
        var output = Text.Green(msg);
        
        // ASSERT
        Assert.Contains(msg, output);
        Assert.Contains(Text.GREEN_CODE.ToString(), output);
    }
    
    [Fact]
    public void Yellow_Test()
    {
        // ARRANGE
        var msg = "Test Text Yellow";
        
        // ACT
        var output = Text.Yellow(msg);
        
        // ASSERT
        Assert.Contains(msg, output);
        Assert.Contains(Text.YELLOW_CODE.ToString(), output);
    }
    
}