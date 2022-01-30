using WebSocketSharp;
using WebSocketSharp.Server;

namespace liverserver.app {

public class ReloaderService : WebSocketBehavior {
    public string targetDir = "";

    private DateTime lastOperationTime = DateTime.MinValue;

    protected override void OnOpen()
    {
        using var watcher = new FileSystemWatcher(targetDir);

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