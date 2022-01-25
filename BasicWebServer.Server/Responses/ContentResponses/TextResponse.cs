using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Server.Responses.ContentResponses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text) 
            : base(text,ContentType.PlainText)
        {
        }
    }
}
