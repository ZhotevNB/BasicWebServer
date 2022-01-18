using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses.ActionResponses;
using BasicWebServer.Server.Responses.ContentResponses;

public class StartUp
{


    private const string HtmlForm = @"<form action='/HTML' method='Post'>
    Name: <input type='text' name='Name'/>
    Age: <input type='number' name='Age'/>
    <input type='submit' value='Save'/>
</form>";
    public static void Main()
    {
        new HTTPServer(routes => routes
       .MapGet("/", new TextResponse("Hello from the server!"))
        .MapGet("/Redirect".ToLower(), new RedirectResponse("https://softuni.org"))
        .MapGet("/HTML".ToLower(), new HtmlResponse(StartUp.HtmlForm))
        .MapPost("/HTML".ToLower(), new TextResponse("",StartUp.AddFormDataAction)))
        .Start();
    }
    private static void AddFormDataAction(
        Request request,Response response)
    {
        response.Body = "";

        foreach (var (key,value) in request.Form)
        {
            response.Body += $"{key} - {value}";
            response.Body += Environment.NewLine;
        }
    }
}

