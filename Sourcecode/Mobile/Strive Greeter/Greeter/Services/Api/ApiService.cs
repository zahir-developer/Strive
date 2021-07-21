using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;
using Newtonsoft.Json;

namespace Greeter.Services.Api
{
    public interface IApiService
    {
        //public static INetworkService iNetworkService = new NetworkService();
        Task<T> DoApiCall<T>(string subUrl, HttpMethod method = HttpMethod.Get, Dictionary<string, string> parameters = null, object req = null, bool isBearerToken = true) where T : BaseResponse;
    }

    public class ApiService : IApiService
    {
        readonly INetworkService iNetworkService = new NetworkService();

        //public Api(INetworkService iNetworkService)
        //{
        //    this.iNetworkService = iNetworkService;
        //}

        // Request forming and response parsing job from network 
        public async Task<T> DoApiCall<T>(string subUrl, HttpMethod method = HttpMethod.Get, Dictionary<string, string> parameters = null, object req = null, bool isBearerToken = true) where T : BaseResponse
        {
            IRestRequest request = new RestRequest(subUrl, method);

            if (isBearerToken)
            {
                request.AddHeader("Authorization", AppSettings.BearereToken);
            }

            if ((method == HttpMethod.Post || method == HttpMethod.Put) && req is not null)
            {
                request.AddBody(req);
            }

            if (parameters is not null && parameters.Count > 0)
            {
                foreach (var keyValuePair in parameters)
                {
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
                }
            }

            var commonResponse = await iNetworkService.ExecuteAsync<CommonResponse>(request);

            // Parse json string result to json (class object)
            T response = null;
            try
            {
                if (commonResponse?.ResultData != null)
                {
                    response = commonResponse?.ResultData.ParseJsonString<T>();
                    response.StatusCode = commonResponse.StatusCode;
                    response.Message = commonResponse.Message;

                    Debug.WriteLine("De-serilised Response : " + JsonConvert.SerializeObject(response));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
            }

            return response;
        }
    }
}
