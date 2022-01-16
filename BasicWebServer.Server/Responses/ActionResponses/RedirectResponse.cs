using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Server.Responses.ActionResponses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string locaion) 
            : base(StatusCode.Found)
        {
            this.Headers.Add(Header.Location, locaion);
        }
    }
}
