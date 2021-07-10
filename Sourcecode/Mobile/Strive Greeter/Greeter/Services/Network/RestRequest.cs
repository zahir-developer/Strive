using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Greeter.Services.Network
{
#nullable enable
    public class RestRequest : IRestRequest
    {
        [NotNull]
        public string Path { get; private set; }
        public Dictionary<string, string>? Parameter { get; private set; }

        public Dictionary<string, string>? Header { get; private set; }

        public object? Body { get; private set; }
        public HttpMethod Method { get; private set; } = HttpMethod.Get;

        public RestRequest(string path, HttpMethod method = HttpMethod.Get)
        {
            Method = method;
            Parameter = new Dictionary<string, string>();
            Header = new Dictionary<string, string>();
            Path = path;
        }

        public void AddParameter(string key, string value) => Parameter?.Add(key, value);

        public void AddBody(object value) => Body = value;

        public void AddHeader(string key, string value) => Header?.Add(key, value);
    }
#nullable restore
}