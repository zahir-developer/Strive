using System;
using Strive.Core.Models;
using Strive.Core.Models.Customer;

namespace Strive.Core.Utils
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string txt) => string.IsNullOrWhiteSpace(txt);
        public static bool IsSuccess(this BaseResponsePayment responseBase) => responseBase.StatusCode >= 200 && responseBase.StatusCode <= 299;
    }
}
