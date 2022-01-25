

using BasicWebServer.Server.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using System.Text;
using System.Web;
using BasicWebServer.Demo.Controllers;

public class StartUp
{

    private const string LoginForm = @"<form action='/Login' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='text' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";

    private const string UserName = "user";

    private const string Password = "user123";


    public static async Task Main()
    {
        //await DownloadSitesAsTextFile(StartUp.FileName,
        //    new string[] { "https://judge.softuni.org/", "https://softuni.org/" });

        var server = new HTTPServer(routes => routes
        .MapGet<HomeController>("/",c=>c.Index())
        .MapGet<HomeController>("/redirect",c=>c.Redirect())
        .MapGet<HomeController>("/html",c=>c.Html())
        .MapPost<HomeController>("/html", c=>c.HtmlFormPost())
        .MapGet<HomeController>("/content", c=>c.Content())
        .MapPost<HomeController>("/content", c => c.DownloadContent())
        .MapGet<HomeController>("/cookies", c=>c.Cookies())
        .MapGet<HomeController>("/session", c => c.Session()));

        await server.Start();
     

    }

    //    var server = new HTTPServer(routes => routes
    //  
    //   .MapGet("/Redirect".ToLower(), new RedirectResponse("https://softuni.org"))
    //   .MapGet("/HTML".ToLower(), new HtmlResponse(StartUp.HtmlForm))
    //   .MapPost("/HTML".ToLower(), new TextResponse("", StartUp.AddFormDataAction))
    //   .MapGet("/Content".ToLower(), new HtmlResponse(StartUp.DownloadForm))
    //   .MapPost("/Content".ToLower(), new TextFileResponse(StartUp.FileName))
    //   .MapGet("/Cookies".ToLower(), new HtmlResponse("", StartUp.AddCookiesAction))
    //   .MapGet("/Session".ToLower(), new TextResponse("",StartUp.DisplaySessionInfoAction))
    //   .MapGet("/Login".ToLower(),new HtmlResponse(StartUp.LoginForm))
    //   .MapPost("/Login".ToLower(),new HtmlResponse("",StartUp.LoginAction))
    //   .MapGet("/Logout".ToLower(), new HtmlResponse("", StartUp.LogoutAction))
    //   .MapGet("/UserProfile".ToLower(), new HtmlResponse("", StartUp.GetUserDataAction)));

    //    await server.Start();

    
    private static void LoginAction(Request request,Response response)
    {
        request.Session.Clear();

        var bodyText = "";

        var userNameMatches = request.Form["Username"] == StartUp.UserName;
        var passwordMatches = request.Form["Password"] == StartUp.Password;

        if (userNameMatches&&passwordMatches)
        {
            request.Session[Session.SessionUserKey] = "MyUserId";
            request.Cookies.Add(Session.SessionCookieName, request.Session.Id);

            bodyText = "<h3>Logged successfully!</h3>";
        }
        else
        {
            bodyText = StartUp.LoginForm;
        }

        response.Body = "";
        response.Body +=bodyText;
    }

    private static void LogoutAction(Request request,Response response)
    {
       
        request.Session.Clear();

        response.Body = "";
        response.Body += "<h3>Logged out successfully!</h3>";
    }

    private static void GetUserDataAction
        (Request request, Response response)
    {
        if (request.Session.ContainsKey(Session.SessionUserKey))
        {
            response.Body = "";
            response.Body += $"<h3>Currently logged-in user" +
                $"is with username '{UserName}'</h3>";
        }
        else
        {
            response.Body = "";
            response.Body += "<h3>You should first log in" +
               "- <a href='/Login'>Login</a></h3>";
        }
    }
}

