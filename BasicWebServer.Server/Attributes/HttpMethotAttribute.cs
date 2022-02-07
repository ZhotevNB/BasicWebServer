using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethotAttribute : Attribute
    {
        public Method HttpMethod { get; }

        public HttpMethotAttribute(Method httpMethod)
        {
            HttpMethod = httpMethod;
        }
    }
}
