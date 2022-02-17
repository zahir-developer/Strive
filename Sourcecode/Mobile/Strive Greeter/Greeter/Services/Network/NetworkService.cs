using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Foundation;
using Newtonsoft.Json;
using System.Linq;
using System;
using Xamarin.Essentials;
using System.Diagnostics;
using Greeter.Services.Authentication;
using Greeter.Extensions;
using System.Net;

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
                if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    var dataTaskRequest = await task;
                    var urlResponse = dataTaskRequest.Response as NSHttpUrlResponse;

                    // Log
                    Debug.WriteLine("Url : " + urlRequest.Url.ToString());
                    Debug.WriteLine("Method : " + urlRequest.HttpMethod.ToString());
                    Debug.WriteLine("Bearer Token : " + AppSettings.BearereToken);

                    if (request.Body is not null)
                        Debug.WriteLine("Request Body : " + JsonConvert.SerializeObject(request.Body));

                    Debug.WriteLine("Status Code : " + urlResponse.StatusCode);

                    if (urlResponse?.StatusCode >= 200 && urlResponse?.StatusCode <= 299)
                    {
                        using var responseString = NSString.FromData(dataTaskRequest.Data, NSStringEncoding.UTF8);
                        Debug.WriteLine("Response String : " + responseString);

                        //Debug.WriteLine("Response JSON : " + JsonConvert.SerializeObject(responseString));

                        TResult result = Activator.CreateInstance<TResult>();
                        if(!string.IsNullOrEmpty(responseString))
                            result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseString));
                        result.StatusCode = (int)urlResponse?.StatusCode;
                        return result;
                    } // Un-Authorized
                    else if (urlResponse.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        var refreshApiResponse = await new AuthenticationService().ResfreshApiCall(AppSettings.Token, AppSettings.RefreshToken);
                        if (refreshApiResponse?.IsSuccess() ?? false)
                        {
                            AppSettings.Token = refreshApiResponse.Token;
                            AppSettings.RefreshToken = refreshApiResponse?.RefreshToken;
                            return await this.ExecuteAsync<TResult>(request);
                        }
                        else
                        {
                            var errorResult = Activator.CreateInstance<TResult>();
                            errorResult.StatusCode = (int)urlResponse.StatusCode;
                            errorResult.Message = urlResponse.StatusCode.ToString();
                            return errorResult;
                        }
                    }
                    else
                    {
                        var errorResult = Activator.CreateInstance<TResult>();
                        errorResult.StatusCode = (int)urlResponse.StatusCode;
                        errorResult.Message = urlResponse.StatusCode.ToString();
                        return errorResult;
                    }
                }
                else
                {
                    var response = Activator.CreateInstance<TResult>();
                    // Status Code for no network connectivity 
                    response.StatusCode = -1;
                    response.Message = Common.Messages.NO_INTERNET_MSG;
                    return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                var errorResult = Activator.CreateInstance<TResult>();
                errorResult.StatusCode = 707;
                errorResult.Message = ex.Message;
                return errorResult;
            }
        }

        //public async Task<TResult> ExecuteAsync<TResult, T>(IRestRequest request, string baseUrl = Urls.BASE_URL) where TResult : BaseResponse where T : CommonResponse
        //{
        //    using var urlSession = NSUrlSession.SharedSession;
        //    using var urlRequest = CreateRequest(request, baseUrl);

        //    using var task = urlSession.CreateDataTaskAsync(urlRequest, out NSUrlSessionDataTask sessionDataTask);
        //    sessionDataTask.Resume();

        //    try
        //    {
        //        var dataTaskRequest = await task;
        //        var urlResponse = dataTaskRequest.Response as NSHttpUrlResponse;

        //        if (urlResponse?.StatusCode >= 200 || urlResponse?.StatusCode <= 299)
        //        {
        //            using var responseString = NSString.FromData(dataTaskRequest.Data, NSStringEncoding.UTF8);
        //            var result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseString));

        //            return result;
        //        }
        //        else
        //        {
        //            var errorResult = Activator.CreateInstance<TResult>();
        //            errorResult.StatusCode = (int)urlResponse.StatusCode;
        //            errorResult.Message = urlResponse.StatusCode.ToString();
        //            return errorResult;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var errorResult = Activator.CreateInstance<TResult>();
        //        errorResult.StatusCode = 707;
        //        errorResult.Message = e.Message;
        //        return errorResult;
        //    }
        //}

        NSUrlRequest CreateRequest(IRestRequest request, string baseUrl)
        {
            NSMutableUrlRequest urlRequest = null;

            try
            {
                var urlString = baseUrl + request.Path;
                if (request.Parameter is not null && request.Parameter.Count > 0)
                {
                    urlString += "?";

                    foreach (var keyValuePair in request.Parameter)
                    {
                        if (!urlString[^1].Equals('?'))
                        {
                            urlString += "&";
                        }

                        var value = new NSString(keyValuePair.Value).CreateStringByAddingPercentEncoding(NSUrlUtilities_NSCharacterSet.UrlQueryAllowedCharacterSet);

                        urlString += $"{keyValuePair.Key}={value}";
                    }
                }

                var url = NSUrl.FromString(urlString);

                urlRequest = new NSMutableUrlRequest(url)
                {
                    HttpMethod = GetHttpMethod(request.Method)
                    //HttpMethod = request.Method.ToString()
                    //HttpMethod = nameof(request.Method)
                };

                request.Header.Add("Content-Type", "application/json");
                //request.Header.Add("Accept", "application/json");

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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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