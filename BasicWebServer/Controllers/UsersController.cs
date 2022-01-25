using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Demo.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(Request request)
            : base(request)
        {
        }
    }
}
