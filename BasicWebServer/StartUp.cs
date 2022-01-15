using BasicWebServer.Server;
using System.Net;
using System.Net.Sockets;
using System.Text;

var server = new HTTPServer("127.0.0.1", 8080);
server.Start();




