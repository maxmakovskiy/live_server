using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace liverserver.app {

public class Server {

    private HttpListener listener;
    private string ip;
    private int port;

    private string observableFile;

    public Server(string ip, int port, string filepath)
    {
        this.observableFile = filepath;
        this.port = port;
        this.ip = ip;

        listener = new HttpListener();
        listener.Prefixes.Add(String.Format("http://{0}:{1}/", ip, port));
    }

    public void Process()
    {
        listener.Start();
        Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

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

        Console.WriteLine("Page are sended to client");
        Console.WriteLine("Server stopped...");


    }

}

}