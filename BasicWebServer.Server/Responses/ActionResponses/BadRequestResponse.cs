using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Responses.ActionResponses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse()
            : base(StatusCode.BadRequest)
        {
        }
    }
}
