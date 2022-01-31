using WebSocketSharp.NetCore.Server;

namespace liveserver.app {

public class ReloaderService : WebSocketBehavior {
    public string Target = "";

    private DateTime lastOperationTime = DateTime.MinValue;
    private FileSystemWatcher watcher;

    public ReloaderService()
    {
        watcher = new FileSystemWatcher();
    }

    ~ReloaderService()
    {
        watcher.Dispose();
    }

    protected override void OnOpen()
    {
        Console.WriteLine("WebSocket connection with was successfully established");
        
        var target = FileUtils.SplitToDirAndFile(Target);
        watcher.Path = target.Item1;
        watcher.Filter = target.Item2;

        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.Changed += (sender, eventArgs) => {
            DateTime lastWrite = File.GetLastWriteTime(eventArgs.FullPath);

            if (lastWrite != lastOperationTime)
            {
                string outputInfo = String.Format("File changed: {0}", eventArgs.FullPath);
                Console.WriteLine(outputInfo);
                Send(outputInfo);
                lastOperationTime = lastWrite;
            }
        };

        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true; // begin watching
    }
   
/*
    protected override void OnClose(CloseEventArgs eventArgs) 
    {
        Console.WriteLine(eventArgs.Data);
    }
*/



}

}