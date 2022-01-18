using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Routing;
using BasicWebServer.Server.Routing.Contracts;
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

        private readonly RoutingTable routingTable;

        public HTTPServer(string ipAddress, int port,Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.serverListener = new TcpListener(this.ipAddress, port);

            routingTableConfiguration(this.routingTable=new RoutingTable());
        }

        public HTTPServer(int port, Action<IRoutingTable> routingTable)
            :this("127.0.0.1",port,routingTable)
        {

        }
        public HTTPServer(Action<IRoutingTable> routingTable)
            :this(8080,routingTable)
        {

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

                var request=Request.Parse(requestText);

                var response = this.routingTable.MatchRequest(request);

                if (response.PreRenderAction != null)
                {
                    response.PreRenderAction(request, response);
                }

                WriteResponse(networkStream, response);

                connection.Close();

            }

        }

        private void WriteResponse(NetworkStream networkStream, Response respons)
        {
            

            var resopnseBytes = Encoding.UTF8.GetBytes(respons.ToString());

            networkStream.Write(resopnseBytes);
        }

        private static string ReadRequest(NetworkStream networkStream)
        {
            var bufferLenght = 1024;
            var buffer = new byte[bufferLenght];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = networkStream.Read(buffer, 0, bufferLenght);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
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
