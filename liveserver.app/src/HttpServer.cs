using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace liverserver.app {

public class HttpServer {

    private TcpListener listener;
    private string ip;
    private int port;

    public HttpServer(string ip, int port)
    {
        this.port = port;
        this.ip = ip;
        listener = new TcpListener(IPAddress.Parse(ip), port);
    }

    public void Start()
    {
        listener.Start();
        Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("A client connected.");

        NetworkStream stream = client.GetStream();

        while(true) {
            while(!stream.DataAvailable);
            while(client.Available < 3); // match against "get"

            byte[] bytes = new byte[client.Available];
            stream.Read(bytes, 0, client.Available);
            string s = Encoding.UTF8.GetString(bytes);

            if(Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase)) {
                Console.WriteLine("=====Handshaking from client=====\n{0}", s);

                string swk = Handshaking(s);

               // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                byte[] response = Encoding.UTF8.GetBytes(
                    "HTTP/1.1 101 Switching Protocols\r\n" +
                    "Connection: Upgrade\r\n" +
                    "Upgrade: websocket\r\n" +
                    "Sec-WebSocket-Accept: " + swk + "\r\n\r\n");

                stream.Write(response, 0, response.Length);

            } else {
                bool fin = (bytes[0] & 0b10000000) != 0;
                bool mask = (bytes[1] & 0b10000000) != 0; 

                int opcode = bytes[0] & 0b00001111;
                int msglen = bytes[1] - 128;

                if(mask) {
                    byte[] masked = new ArraySegment<byte>(bytes, 2, 4).ToArray();
                    byte[] encoded = new ArraySegment<byte>(bytes, 6,
                        bytes.Length - masked.Length - 2).ToArray();

                    var decodedData = Decode(encoded, masked, encoded.Length);

                    string text = Encoding.UTF8.GetString(decodedData);

                    Console.WriteLine("{0}", text);
                } else {
                    Console.WriteLine("mask bit not set");
                }

                Console.WriteLine();
            }
        }

    }

    private string Handshaking(string data)
    {
        return Convert.ToBase64String(
            System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(
                Regex.Match(data, "Sec-WebSocket-Key: (.*)")
                    .Groups[1]
                    .Value
                    .Trim()
                + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
            ))
        );
    }

    private byte[] Decode(byte[] encodedData, byte[] masks, int msgLength)
    {
        byte[] decoded = new byte[msgLength];

        for (int i = 0; i < msgLength; ++i) {
            decoded[i] = (byte)(encodedData[i] ^ masks[i % 4]);
        }

        return decoded;
    }

}

}