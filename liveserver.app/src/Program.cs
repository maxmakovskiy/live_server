using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace liverserver.app {

public class Program {

    public static void Main(string[] args) 
    {
        HttpServer server = new HttpServer("127.0.0.1", 80);
        server.Start();

        /*
        string target = "/home/xemerius/devs/live_server/liveserver.test/rcs";

        var wb = new WebSocketServer("ws://localhost:80");
        wb.AddWebSocketService<ReloaderService>("/",
            () => new ReloaderService() {
                targetDir = target
            });

        wb.Start();
        
        Console.ReadKey(true); // block
        
        wb.Stop();
        */

    }

    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Changed: {e.FullPath}");
    }

}

}