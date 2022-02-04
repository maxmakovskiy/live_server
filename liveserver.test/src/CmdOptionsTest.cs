using liveserver.app;
using Xunit;

namespace liveserver.test;

public class CmdOptionsTest
{
    [Fact] 
    public void IsValidTest()
    {
        Assert.True(CmdOptions.IsValid(new []{"-target", FileSpectatorTest.GetRoot() + "/rcs/index2.html"}));
        Assert.False(CmdOptions.IsValid(new []{"", FileSpectatorTest.GetRoot() + "/rcs/index2.html"}));
    }

}