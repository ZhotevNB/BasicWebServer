using BasicWebServer.Server;
using BasicWebServer.Server.Responses.ActionResponses;
using BasicWebServer.Server.Responses.ContentResponses;
using System.Net;
using System.Net.Sockets;
using System.Text;

     new HTTPServer(routes => routes
    .MapGet("/", new TextResponse("Hello from the server!"))
    .MapGet("/HTML".ToLower(), new HtmlResponse("<h1>HTML response</h1"))
    .MapGet("/Redirect".ToLower(), new RedirectResponse("https://softuni.org")))
    .Start();


