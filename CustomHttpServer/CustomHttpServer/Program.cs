namespace CustomHttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            const int Port = 80;
            const string NewLine = "\r\n";

            var tcpListener = new TcpListener(IPAddress.Loopback, Port);

            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                using var stream = client.GetStream();

                var data = GetAllData(stream).ToArray();

                var requestString = Encoding.UTF8.GetString(data, 0, data.Length);

                Console.WriteLine(requestString);

                var html = $"<h1>Hello from CustomServer {DateTime.Now}</h1>";

                var responseBytes = Encoding.UTF8.GetBytes(html);

                var response = "HTTP/1.1 200 0K" + NewLine +
                    "Server: CustomServer 2020" + NewLine +
                    "Content-Type: text/html; charset=utf-8" + NewLine +
                    $"Content-Lenght: {responseBytes.Length}" + NewLine +
                    NewLine;

                stream.Write(responseBytes);

                Console.WriteLine(new string('=', 80));
            }
        }

        private static List<byte> GetAllData(NetworkStream stream)
        {
            var data = new List<byte>();

            var buffer = new byte[4096];

            while (true)
            {
                var readedBytes = stream.Read(buffer, 0, buffer.Length);

                if (readedBytes == 0 && buffer.Any(x => x != 0))
                {
                    break;
                }

                data.AddRange(buffer.Where(x => x != 0));

                if (buffer.Where(x => x != 0).Count() < buffer.Length)
                {
                    break;
                }
            }

            return data;
        }
    }
}
