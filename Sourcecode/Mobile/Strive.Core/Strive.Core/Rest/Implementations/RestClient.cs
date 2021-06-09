using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Logging;
using Strive.Core.Models;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly IMvxLog _mvxLog;
        private static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public RestClient(IMvxJsonConverter jsonConverter, IMvxLog mvxLog)
        {
            _jsonConverter = jsonConverter;
            _mvxLog = mvxLog;
        }

        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            string stringSerialized;
            BaseResponse baseResponse = new BaseResponse();
            url = ApiUtils.AZURE_URL + url;
            //url = url.Replace("http://", "https://");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiUtils.Token);
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {
                    if (method != HttpMethod.Get)
                    {
                        var json = _jsonConverter.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        Console.WriteLine(json);
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        Console.WriteLine(request);
                        response = await httpClient.SendAsync(request).ConfigureAwait(true);
                        Console.WriteLine(response); 
                    }
                    catch (Exception ex)
                    {
                        _userDialog.HideLoading();
                        _mvxLog.ErrorException("MakeApiCall failed", ex);
                        if (!url.Contains("Login"))
                        {
                            await _userDialog.AlertAsync("The operation cannot be completed at this time.", "Unexpected Error");
                        }
                        baseResponse.resultData = "null";
                        return _jsonConverter.DeserializeObject<TResult>(baseResponse.resultData);
                    }
                    try
                    {
                        stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                        Console.WriteLine(stringSerialized);
                        baseResponse = _jsonConverter.DeserializeObject<BaseResponse>(stringSerialized);
                    }
                    catch(Exception ex)
                    {
                        _userDialog.HideLoading();
                        if (!url.Contains("Login"))
                        {
                            await _userDialog.AlertAsync("The operation cannot be completed at this time.", "Unexpected Error");
                        }
                        baseResponse.resultData = "null";
                        return _jsonConverter.DeserializeObject<TResult>(baseResponse.resultData);
                    }

                    if (WeirdResponse(baseResponse))
                    {
                        _userDialog.HideLoading();
                        baseResponse.resultData = "null";
                        return _jsonConverter.DeserializeObject<TResult>(baseResponse.resultData);
                    }

                    if (!ValidateResponse(baseResponse))
                    {
                        _userDialog.HideLoading();
                        if(!url.Contains("Login"))
                        {
                            await _userDialog.AlertAsync("The operation cannot be completed at this time.", "Unexpected Error");
                        }
                        baseResponse.resultData = "null";
                        return _jsonConverter.DeserializeObject<TResult>(baseResponse.resultData);
                    }
                    _userDialog.HideLoading();
                    return _jsonConverter.DeserializeObject<TResult>(baseResponse.resultData);
                }
            }
        }

        bool ValidateResponse (BaseResponse response)
        {
            bool isValid = true;
            if(response.statusCode == 200)
            {
                return isValid;
            }
            return !isValid;
        }

        bool WeirdResponse(BaseResponse response)
        {
            bool isValid = true;
            if (response.statusCode == 200 && response.resultData == null)
            {
                return isValid;
            }
            else if(response.statusCode == 403 && response.resultData == null)
            {
                _userDialog.AlertAsync(Strings.UsernamePasswordIncorrect);
            }
            return !isValid;
        }
    }
}
