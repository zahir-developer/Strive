using System;
namespace Strive.Core.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
        }
        public string status { get; set; }
        public int statusCode { get; set; }
        public string exception { get; set; }
        public string resultData { get; set; }
    }
}
