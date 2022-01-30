using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace liverserver.app {

public class Server {
    private WebSocketServer wb;
    private HttpListener listener;
    private string ip;
    private int port;

    private string observableFile;
    private string target;

    public Server(string ip, int port, string filepath, string targetDirectory)
    {
        this.observableFile = filepath;
        this.port = port;
        this.ip = ip;
        this.target = targetDirectory;

        listener = new HttpListener();
        listener.Prefixes.Add(String.Format("http://{0}:{1}/", ip, port));

        wb = new WebSocketServer("ws://localhost:80");
        wb.AddWebSocketService<ReloaderService>("/",
            () => new ReloaderService() {
                targetDir = target
            });
    }

    public void Process()
    {
        listener.Start();
        Console.WriteLine("HttpServer has started on {0}:{1}, Waiting for a connection...", ip, port);

        HttpListenerContext context = listener.GetContext();

        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string responseStr = FileSpectator.LoadAndInject(observableFile);
        byte[] buffer = Encoding.UTF8.GetBytes(responseStr);

        response.ContentLength64 = buffer.Length;

        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);

        output.Close();
        listener.Stop();
        Console.WriteLine("Http Server stopped");

        wb.Start();
        Console.WriteLine("WebSocketServer started");
        Console.ReadKey(true);
        wb.Stop(); 
        Console.WriteLine("WebSocketServer stopped");

    }

}

}