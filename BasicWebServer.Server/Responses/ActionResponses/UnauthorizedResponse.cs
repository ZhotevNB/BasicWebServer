using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Server.Responses.ActionResponses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse() 
            : base(StatusCode.Unauthorized)
        {
        }
    }
}
