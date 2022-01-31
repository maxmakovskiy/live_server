using System.Net;
using System.Text;
using WebSocketSharp.NetCore.Server;

namespace liveserver.app {

public class Program {

    public static void Main(string[] args) 
    {
        Console.WriteLine("Observable file: {0}", args[0]);

        var server = new Server("127.0.0.1", 80, args[0]);
        server.Process();

    }

}

}