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
        var wb = new WebSocketServer("ws://localhost:80");
        wb.AddWebSocketService<ReloaderService>("/");

        wb.Start();
        Console.ReadKey(true);
        wb.Stop();

    }

}

}