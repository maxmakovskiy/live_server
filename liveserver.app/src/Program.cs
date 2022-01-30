using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace liverserver.app {

public class Program {

    public static void Main(string[] args) 
    {
        Console.WriteLine("Observe next file {0}", args[0]);
        Server server = new Server("127.0.0.1", 80, args[0], args[1]);
        server.Process();

    }

}

}