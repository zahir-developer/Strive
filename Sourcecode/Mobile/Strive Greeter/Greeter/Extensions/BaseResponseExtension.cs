﻿using Greeter.DTOs;

namespace Greeter.Extensions
{
    public static class BaseResponseExtension
    {
        public static bool IsSuccess(this BaseResponse responseBase) => responseBase.StatusCode is >= 200 and <= 299;

        public static bool IsNoInternet(this BaseResponse responseBase) => responseBase.StatusCode is -1;
    }
}