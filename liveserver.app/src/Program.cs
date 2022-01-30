using System.Net;
using System.Text;
using WebSocketSharp.NetCore.Server;

namespace liveserver.app {

public class Program {

    public static void Main(string[] args) 
    {
        Console.WriteLine("Observable file: {0}", args[0]);

        var server = new HttpServer(IPAddress.Parse("127.0.0.1"), 80);
        
        // Set the HTTP GET request event.
        server.OnGet += (sender, e) => {
            var req = e.Request;
            var res = e.Response;

            byte[] contents = Encoding.UTF8.GetBytes(
               FileUtils.LoadAndInject(args[0]));
            
            res.ContentType = "text/html";
            res.ContentEncoding = Encoding.UTF8;

            res.ContentLength64 = contents.LongLength;
            res.Close (contents, true);
        };

        server.AddWebSocketService<ReloaderService> ("/",
            service => { service.Target = args[0]; }
        );

        server.Start ();
        if (server.IsListening) {
            Console.WriteLine ("Listening on port {0}", server.Port);
        }

        Console.WriteLine ("\nPress Enter key to stop the server...");
        Console.ReadLine ();

        server.Stop ();

    }

}

}