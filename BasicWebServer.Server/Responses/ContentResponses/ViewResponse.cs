using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Responses.ContentResponses
{
    public class ViewResponse : ContentResponse
    {
        private const char PathSeparator = '/';
        public ViewResponse(string viweName, string controllerName)
            : base("", ContentType.Html)
        {
            if (!viweName.Contains(PathSeparator))
            {
                viweName = controllerName + PathSeparator + viweName;
            }
            var viewPath = Path.GetFullPath(
                $"./Views/" +
                viweName.TrimStart(PathSeparator)
                + ".cshtml");

            var viewContent = File.ReadAllText(viewPath);

            this.Body = viewContent;
        }
    }
}
