using BasicWebServer.Server.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Demo.Controllers;


public class StartUp
{
    public static async Task Main()
    => await new HTTPServer(routes => routes
         .MapControllers())
        .Start();
}

