using WebSocketSharp;
using WebSocketSharp.Server;

namespace liverserver.app {

public class ReloaderService : WebSocketBehavior {
    DateTime lastOperationTime = DateTime.MinValue;

    protected override void OnOpen()
    {
        Console.WriteLine("Server: connection was established");

        string target = "/home/xemerius/devs/live_server/liveserver.test/rcs";

        using var watcher = new FileSystemWatcher(target);

        watcher.NotifyFilter = NotifyFilters.LastWrite;

        watcher.Changed += (senser, eventArgs) => {
            DateTime lastWrite = File.GetLastWriteTime(eventArgs.FullPath);

            if (lastWrite != lastOperationTime) {
                Send("File changed: " + eventArgs.FullPath);
                lastOperationTime = lastWrite;
            }

        };

        watcher.Filter = "*.txt";

        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true; // begin watching

        Console.ReadKey(true); // block
    }

    protected override void OnMessage(MessageEventArgs eventArgs) 
    {
        Console.WriteLine(eventArgs.Data);
    }




}

}