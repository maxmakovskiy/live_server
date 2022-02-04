using System.Net;
using System.Text;
using Microsoft.VisualBasic;
using WebSocketSharp.NetCore.Server;

namespace liveserver.app {

public class Program {

    public static void Main(string[] args) 
    {
        if (!CmdOptions.IsValid(args))
        {
            Console.WriteLine("Invalid options format: {0}", String.Join(' ', args));
            Console.WriteLine("Please try: -target [/path/to/target]");
            return;
        }
        
        Console.WriteLine("Observable file: {0}", args[1]);

        var server = new Server("127.0.0.1", 80, args[1]);
        server.Process();
    }

}

}