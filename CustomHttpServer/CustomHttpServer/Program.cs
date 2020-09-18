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
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            const int Port = 80;

            var tcpListener = new TcpListener(IPAddress.Loopback, Port);

            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
              
                var readedBytes = GetAllData(client).ToArray();

                var dataAsString = Encoding.UTF8.GetString(readedBytes, 0, readedBytes.Length);

                Console.WriteLine(dataAsString);
                Console.WriteLine(new string('=', 80));
            }
        }

        private static List<byte> GetAllData(TcpClient client)
        {
            using var stream = client.GetStream();

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
