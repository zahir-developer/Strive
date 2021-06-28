using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;

namespace Greeter.Services.Network
{
    public interface INetworkService
    {
        Task<TResult> ExecuteAsync<TResult>(IRestRequest request, string baseUrl = Urls.BASE_URL) where TResult : BaseResponse;

        //Task<TResult> ExecuteAsync<TResult, T>(IRestRequest request, string baseUrl = Urls.BASE_URL) where TResult : BaseResponse where T : CommonResponse;
    }
}