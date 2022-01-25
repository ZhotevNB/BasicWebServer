using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Responses.ContentResponses
{
    public class ViewResponse : ContentResponse
    {
        private const char PathSeparator = '/';
        public ViewResponse(string viweName, string controllerName, object model = null)
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

            if (model!= null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.Body = viewContent;
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var entry in data)
            {
                const string openingBrackets = "{{";
                const string closingBrackets = "}}";

                viewContent = viewContent.Replace(
                    $"{openingBrackets}{entry.Name}{closingBrackets}", entry.Value.ToString());
            }
            return viewContent;
        }
    }
}
