﻿using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Demo.Controllers
{
    public class UsersController : Controller
    {
        private const string LoginForm = @"<form action='/Login' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='password' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";

        private const string UserName = "user";

        private const string Password = "user123";

        public UsersController(Request request)
            : base(request)
        {
        }

        public Response Login() => Html(UsersController.LoginForm);

        public Response LogInUser()
        {
            this.Request.Session.Clear();

            var userNameMatches =
                this.Request.Form["Username"] == UsersController.UserName;
            var passwordMatches = 
                this.Request.Form["Password"] == UsersController.Password;

            if (userNameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    this.Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies=new CookieCollection();
                    cookies.Add(Session.SessionCookieName, this.Request.Session.Id);
                
                    return Html("<h3>Logged successfully!</h3>",cookies);
                }

                return Html("<h3>Logged successfully!</h3>");


            }

            return Redirect("/Login");
        }

        public Response Logout()
        {
            this.Request.Session.Clear();

            return Html("<h3>Logged out successfully!</h3>");
        }

        public Response GetUserData()
        {
            if (this.Request.Session.ContainsKey(Session.SessionUserKey))
            {
               
               return Html($"<h3>Currently logged-in user" +
                    $"is with username '{UsersController.UserName}'</h3>");
            }

            return Redirect("/Login");
        }
    }
}
