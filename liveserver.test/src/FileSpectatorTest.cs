using Xunit;
using System.IO;
using System.Collections;

using liverserver.app;

namespace liveserver.test {

public class FileSpectatorTest {

    [Fact]
    public void TestAbsolutePathToFiles()
    {
        string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string target = root + "/rcs";
        string[] result = FileSpectator.AbsolutePathToFiles(target);
        string[] expected = {
            target + "/file1.txt",
            target + "/file2.txt",
            target + "/file3.txt",
            target + "/dir1/file1_from_dir1.txt",
            target + "/dir1/file2_from_dir1.txt",
            target + "/dir2/file1_from_dir1.txt",
        };

        Assert.Equal(expected, result);
    }



}

}

