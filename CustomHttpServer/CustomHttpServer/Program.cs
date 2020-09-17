namespace CustomHttpServer
{
    using System;
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
                var client = await tcpListener.AcceptTcpClientAsync();

                using var stream = client.GetStream();
                var buffer = new byte[1000000];

                var readedBytes = stream.Read(buffer, 0, buffer.Length);

                var data = Encoding.UTF8.GetString(buffer,0,buffer.Length);

                Console.WriteLine(data);
                Console.WriteLine(new string('=', 80));
            }
        }
    }
}
