using System.Collections.Generic;

namespace Greeter.Services.Network
{
    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete
    }

#nullable enable
    public interface IRestRequest
    {
        string Path { get; }
        Dictionary<string, string>? Parameter { get; }
        Dictionary<string, string>? Header { get; }
        object? Body { get; }
        HttpMethod Method { get; }

        void AddBody(object value);
        void AddParameter(string key, string value);
        void AddHeader(string key, string value);
    }
#nullable restore
}