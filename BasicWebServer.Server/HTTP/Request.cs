using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicWebServer.Server.HTTP
{
    public class Request
    {
        public Method Method { get; private set; }

        public string Url { get; private set; }

        public HeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public static Request Parse(string request)
        {
            var lines = request.Split("\r\n");

            var startLine = lines.First().Split(" ");

            Method method = (Method)Enum.Parse(typeof(Method), startLine[0], true);

            var url = startLine[1];

            var headers = ParseHeaders(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join("\r\n", bodyLines);

            var form = ParseForm(headers, body);

            return new Request
            {
                Method = method,
                Url = url.ToLower(),
                Headers = headers,
                Body = body,
                Form=form
            };
        }

        private static Dictionary<string, string> ParseForm(
            HeaderCollection headers, string body)
        {
            var formCollection = new Dictionary<string, string>();

            if (headers.Contains(Header.ContentType)
                && headers[Header.ContentType]==ContentType.FromUrlEncoded)
            {
                var parsedResult=ParseFormData(body);

                foreach (var (name,value) in parsedResult)
                {
                    formCollection.Add(name, value);
                }
            }

            return formCollection;
        }
        private static Dictionary<string,string>ParseFormData(string bodyLines)
            =>HttpUtility.UrlDecode(bodyLines)
            .Split('&')
            .Select(part=>part.Split('='))
            .Where(part => part.Length == 2)
            .ToDictionary(
                part => part[0],
                part => part[1],
                StringComparer.InvariantCultureIgnoreCase);
             

        private static HeaderCollection ParseHeaders(IEnumerable<string> headerLines)
        {
            var headerCollection = new HeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (String.IsNullOrWhiteSpace(headerLine))
                {
                    break;
                }

                var headerParts = headerLine.Split(": ", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1];

                headerCollection.Add(headerName, headerValue);
            }

            return headerCollection;
        }
    }
}
