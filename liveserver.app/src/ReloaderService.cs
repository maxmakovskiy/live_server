using WebSocketSharp;
using WebSocketSharp.Server;

namespace liverserver.app {

public class ReloaderService : WebSocketBehavior {

    protected override void OnOpen()
    {
        Console.WriteLine("Server: connection was established");

        Send("hello from server");
    }

    protected override void OnMessage(MessageEventArgs eventArgs) 
    {
        Console.WriteLine(eventArgs.Data);
    }



}

}