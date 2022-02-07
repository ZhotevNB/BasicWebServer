using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributes
{
    public class HttpPostAttribute : HttpMethotAttribute
    {
        public HttpPostAttribute() 
            : base(Method.POST)
        {

        }
    }
}
