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
        private const int Port = 80;
        private const string NewLine = "\r\n";

        public async static Task Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var tcpListener = new TcpListener(IPAddress.Loopback, Port);

            tcpListener.Start();

            while (true)
            {
                var client = await tcpListener.AcceptTcpClientAsync();

               ProcessClientAsync(client);
            }
        }

        private async static Task ProcessClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();

            var data = await GetAllDataAsync(stream);

            var requestString = Encoding.UTF8.GetString(data, 0, data.Length);

            Console.WriteLine(requestString);

            var html = $"<h1>Hello from CustomServer {DateTime.Now}</h1>";

            var content = Encoding.UTF8.GetBytes(html);

            var response = "HTTP/1.1 200 OK" + NewLine +
                "Server: CustomServer 2020" + NewLine +
                // "Location: https://google.com" + NewLine +
                "Content-Type: text/html; charset=utf-8" + NewLine +
                "Set-Cookie: language=bg" + NewLine +
                "Set-Cookie: sid=12345ggj; Secure; HttpOnly" + NewLine +
                //"Set-Cookie: test=value; Max-Age=" + 20 + NewLine +
                //"Set-Cookie: test=pathCookie; Path=/test" + NewLine +
                $"Content-Length: {content.Length}" + NewLine +
                NewLine +
                html +
                NewLine;

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await stream.WriteAsync(responseBytes);

            Console.WriteLine(new string('=', 80));
        }

        private async static Task<byte[]> GetAllDataAsync(NetworkStream stream)
        {
            var data = new List<byte>();

            var buffer = new byte[4096];

            while (true)
            {
                var readedBytes = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (readedBytes == 0)
                {
                    break;
                }

                data.AddRange(buffer.Where(x => x != 0));

                if (buffer.Where(x => x != 0).Count() < buffer.Length)
                {
                    break;
                }
            }

            return data.ToArray();
        }
    }
}
