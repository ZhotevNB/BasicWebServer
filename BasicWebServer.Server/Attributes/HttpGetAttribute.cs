using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributes
{
    public class HttpGetAttribute : HttpMethotAttribute
    {
        public HttpGetAttribute() 
            : base(Method.GET)
        {
        }
    }
}
