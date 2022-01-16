using System;
using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Responses.ActionResponses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse()
            : base(StatusCode.NotFound)
        {
        }
    }
}
