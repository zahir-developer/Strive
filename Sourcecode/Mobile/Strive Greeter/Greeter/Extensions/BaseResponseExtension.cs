using System.Net;
using Greeter.DTOs;

namespace Greeter.Extensions
{
    public static class BaseResponseExtension
    {
        public static bool IsSuccess(this BaseResponse responseBase) => responseBase.StatusCode is >= 200 and <= 299;

        public static bool IsNoInternet(this BaseResponse responseBase) => responseBase.StatusCode is -1;

        public static bool IsBadRequest(this BaseResponse responseBase) => responseBase.StatusCode is (int)HttpStatusCode.BadRequest;

        public static bool IsInternalServerError(this BaseResponse responseBase) => responseBase.StatusCode is (int)HttpStatusCode.InternalServerError;
    }
}