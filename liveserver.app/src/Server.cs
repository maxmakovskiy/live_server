using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

using WebSocketSharp;
using WebSocketSharp.NetCore.Server;

namespace liveserver.app {

public class Server {
    private string ip;
    private int port;
    private HttpServer server;

    public Server(string ip, int port, string filepath)
    {
        this.port = port;
        this.ip = ip;
        
        server = new HttpServer(IPAddress.Parse(ip), port);
        
        // Set the HTTP GET request event.
        server.OnGet += (sender, e) => {
            Console.WriteLine("Incoming http GET-request");
            
            var res = e.Response;

            byte[] contents = Encoding.UTF8.GetBytes(
               FileUtils.LoadAndInject(filepath));
            
            res.ContentType = "text/html";
            res.ContentEncoding = Encoding.UTF8;

            res.ContentLength64 = contents.LongLength;
            res.Close (contents, true);
        };

        server.AddWebSocketService<ReloaderService> ("/",
            service => { service.Target = filepath; }
        );
    }

    public void Process()
    {
        server.Start();
        
        if(server.IsListening) {
            Console.WriteLine("HttpServer has started on {0}:{1}, Waiting for a connection...", ip, port);
        } else {
            Console.WriteLine("HttpServer does not started");
        }

        Console.ReadLine();
        server.Stop();
    }

}

}