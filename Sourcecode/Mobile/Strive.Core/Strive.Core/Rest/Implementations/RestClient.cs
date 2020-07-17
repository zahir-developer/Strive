using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Logging;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly IMvxLog _mvxLog;

        public RestClient(IMvxJsonConverter jsonConverter, IMvxLog mvxLog)
        {
            _jsonConverter = jsonConverter;
            _mvxLog = mvxLog;
        }

        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            url = ApiUtils.BASE_URL + url;
            //url = url.Replace("http://", "https://");

            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {
                    // add content
                    if (method != HttpMethod.Get)
                    {
                        var json = _jsonConverter.SerializeObject(data);
                        request.Content = new StringContent(json);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        response = await httpClient.SendAsync(request).ConfigureAwait(true);
                    }
                    catch (Exception ex)
                    {
                        _mvxLog.ErrorException("MakeApiCall failed", ex);
                    }

                    var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                    // deserialize content
                    return _jsonConverter.DeserializeObject<TResult>(stringSerialized);
                }
            }
        }
    }
}
