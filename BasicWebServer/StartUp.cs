using BasicWebServer.Server.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Demo.Controllers;


public class StartUp
{
    public static async Task Main()
    => await new HTTPServer(routes => routes
         .MapGet<HomeController>("/", c => c.Index())
         .MapGet<HomeController>("/redirect", c => c.Redirect())
         .MapGet<HomeController>("/html", c => c.Html())
         .MapPost<HomeController>("/html", c => c.HtmlFormPost())
         .MapGet<HomeController>("/content", c => c.Content())
         .MapPost<HomeController>("/content", c => c.DownloadContent())
         .MapGet<HomeController>("/cookies", c => c.Cookies())
         .MapGet<HomeController>("/session", c => c.Session())
         .MapGet<UsersController>("/login", c => c.Login())
         .MapPost<UsersController>("/login", c => c.LogInUser())
         .MapGet<UsersController>("/logout", c => c.Logout())
         .MapGet<UsersController>("/userprofile", c => c.GetUserData()))
        .Start();
}

