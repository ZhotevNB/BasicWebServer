using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server
{
    public class HTTPServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;


        public HTTPServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.serverListener = new TcpListener(this.ipAddress, port);
        }

        public void Start()
        {
            this.serverListener.Start();

            Console.WriteLine($"Server is started on port {port}");
            Console.WriteLine("Listening for requests...");

            while (true)
            {

                var connection = serverListener.AcceptTcpClient();

                var networkStream = connection.GetStream();

                var requestText = ReadRequest(networkStream);

                Console.WriteLine(requestText);

                WriteResponse(networkStream, "Hello from the server");

                connection.Close();

            }

        }

        private static void WriteResponse(NetworkStream networkStream, string respons)
        {
            var content = respons;
            var conthentLength = Encoding.UTF8.GetByteCount(content);

            var resopnse = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {conthentLength}


{content}";

            var resopnseBytes = Encoding.UTF8.GetBytes(resopnse);

            networkStream.Write(resopnseBytes);
        }

        private static string ReadRequest(NetworkStream networkStream)
        {
            var bufferLenght = 1024;
            var buffer = new byte[bufferLenght];

            var totalBytes = 0;

            var requestBuilder=new StringBuilder();

            do
            {
                var bytesRead = networkStream.Read(buffer, 0, bufferLenght);

                totalBytes += bytesRead;

                if (totalBytes>10*1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }
    }
}
