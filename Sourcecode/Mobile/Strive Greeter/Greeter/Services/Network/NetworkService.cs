using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Foundation;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Greeter.Services.Network
{
    public class NetworkService : NSObject, INetworkService
    {
        readonly JsonSerializerSettings serializerSettings;

        public NetworkService()
        {
            serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
        }

        public async Task<TResult> ExecuteAsync<TResult>(IRestRequest request, string baseUrl = Urls.BASE_URL) where TResult : BaseResponse
        {
            using var urlSession = NSUrlSession.SharedSession;
            using var urlRequest = CreateRequest(request, baseUrl);

            using var task = urlSession.CreateDataTaskAsync(urlRequest, out NSUrlSessionDataTask sessionDataTask);
            sessionDataTask.Resume();

            try
            {
                var dataTaskRequest = await task;
                var urlResponse = dataTaskRequest.Response as NSHttpUrlResponse;

                if (urlResponse?.StatusCode >= 200 || urlResponse?.StatusCode <= 299)
                {
                    using var responseString = NSString.FromData(dataTaskRequest.Data, NSStringEncoding.UTF8);
                    var result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseString));
                    result.StatusCode = (int)urlResponse?.StatusCode;
                    return result;
                }
                else
                {
                    var errorResult = Activator.CreateInstance<TResult>();
                    errorResult.StatusCode = (int)urlResponse.StatusCode;
                    errorResult.Message = urlResponse.StatusCode.ToString();
                    return errorResult;
                }
            }
            catch (Exception e)
            {
                var errorResult = Activator.CreateInstance<TResult>();
                errorResult.StatusCode = 707;
                errorResult.Message = e.Message;
                return errorResult;
            }
        }

        NSUrlRequest CreateRequest(IRestRequest request, string baseUrl)
        {
            var urlString = baseUrl + request.Path;
            if (request.Parameter is not null && request.Parameter.Count > 0)
            {
                urlString += "?";

                foreach (var keyValuePair in request.Parameter)
                {
                    urlString += $"{keyValuePair.Key}={keyValuePair.Value}";
                }
            }

            var urlRequest = new NSMutableUrlRequest(NSUrl.FromString(urlString))
            {
                HttpMethod = GetHttpMethod(request.Method)
            };

            if (request.Header is not null)
            {
                urlRequest.Headers = NSDictionary.FromObjectsAndKeys(
                    request.Header.Values.ToArray(),
                    request.Header.Keys.ToArray()
                );
            }

            if (request.Body is not null)
            {
                var bodyString = JsonConvert.SerializeObject(request.Body, serializerSettings);
                urlRequest.Body = NSData.FromString(bodyString);
            }

            return urlRequest;
        }

        string GetHttpMethod(HttpMethod httpMethod)
            => httpMethod switch
            {
                HttpMethod.Get => "Get",
                HttpMethod.Post => "Post",
                HttpMethod.Put => "Put",
                HttpMethod.Delete => "Delete",
                _ => throw new ArgumentException($"{httpMethod} Http method not support")
            };
    }
}