﻿using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses.ActionResponses;
using BasicWebServer.Server.Responses.ContentResponses;
using System.Runtime.CompilerServices;


namespace BasicWebServer.Server.Controllers
{
    public class Controller
    {

        protected Request Request { get; set; }
        protected Controller(Request request)
        {
            this.Request = request;
        }


        protected Response Text(string text) => new TextResponse(text);
        protected Response Html(string text) => new HtmlResponse(text);

        protected Response Html(string html, CookieCollection cookies)
        {
            var response = new HtmlResponse(html);

            if (cookies!=null)
            {
                foreach (var cookie in cookies)
                {
                    response.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return response;
        }

        protected Response BadRequest() => new BadRequestResponse();

        protected Response Unauthorized() => new UnauthorizedResponse();

        protected Response Notfound() => new NotFoundResponse();

        protected Response Redirect(string location) => new RedirectResponse(location);

        protected Response File(string fileName) => new TextFileResponse(fileName);

        protected Response View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, this.GetControllerName());

        protected Response View(object model,[CallerMemberName] string viewName = "")
           => new ViewResponse(viewName, this.GetControllerName(),model);

        private string GetControllerName()
            => this.GetType().Name
            .Replace(nameof(Controller), string.Empty);

    }
}
