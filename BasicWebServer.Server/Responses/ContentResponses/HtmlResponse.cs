

using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Responses.ContentResponses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text ) 
            : base(text, ContentType.Html)
        {
        }
    }
}
