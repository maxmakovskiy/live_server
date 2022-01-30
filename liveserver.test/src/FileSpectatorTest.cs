using Xunit;
using System.IO;
using System.Collections;

using liveserver.app;

namespace liveserver.test {

public class FileSpectatorTest {

    [Fact]
    public void TestAbsolutePathToFiles()
    {
        string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string target = root + "/rcs/observableDirs";
        string[] result = FileUtils.AbsolutePathToFiles(target);
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

    [Fact]
    public void TestInjection()
    {
        string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string target = root + "/rcs/index2.html";
        string[] result = FileUtils.LoadAndInject(target).Split('\n');
        string[] expected = {
            "<!DOCTYPE html>",
            "<html>",
            "<head>",
            "<meta charset='utf8'>",
            "</head>",
            "<body>",
            "Hello world!",
            "<script>",
            "(function() {var url = 'ws://localhost:80/';function init() {doWebSocket();}function doWebSocket() {websocket = new WebSocket(url);websocket.onopen = function(e) {onOpen(e);};websocket.onmessage = function(e) {onMessage(e);};}function onOpen(event) {writeToScreen('CONNECTED');send('WebSocket rocks');}function onMessage(event) {writeToScreen('RECEIVE: ' + event.data);}function send(message) {websocket.send(message);}function writeToScreen(message) {var pre = document.createElement('p');pre.innerHTML = message;document.body.appendChild(pre);}window.addEventListener('load', init, false);})();",
            "</script>",
            "</body>",
            "</html>"     
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestSplitToDirAndFile()
    {
        string source = "/home/xemerius/devs/live_server/liveserver.test/rcs/index2.html";
        string targetDirExpected = "/home/xemerius/devs/live_server/liveserver.test/rcs";
        string targetFileExpected = "index2.html";

        var target = FileUtils.SplitToDirAndFile(source);
        Assert.Equal(targetDirExpected, target.Item1);
        Assert.Equal(targetFileExpected, target.Item2);
    }

}

}

