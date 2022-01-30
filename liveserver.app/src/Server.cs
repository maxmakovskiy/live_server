using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

/*
using WebSocketSharp;
using WebSocketSharp.Server;
*/
namespace liveserver.app {
/*
public class Server {
    private WebSocketServer wb;
    private HttpListener listener;
    private string ip;
    private int port;

    public Server(string ip, int port, string filepath)
    {
        this.port = port;
        this.ip = ip;

        listener = new HttpListener();
        listener.Prefixes.Add(String.Format("http://{0}:{1}/", ip, port));

        wb = new WebSocketServer("ws://localhost:80");
        wb.AddWebSocketService<ReloaderService>("/",
            service => { service.Target = filepath; });

    }

    public void Process()
    {
        listener.Start();
        Console.WriteLine("HttpServer has started on {0}:{1}, Waiting for a connection...", ip, port);

        HttpListenerContext context = listener.GetContext();

        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        byte[] buffer = Encoding.UTF8.GetBytes(
            FileSpectator.LoadAndInject("/home/xemerius/devs/live_server/liveserver.test/rcs/index2.html"));

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
*/
}