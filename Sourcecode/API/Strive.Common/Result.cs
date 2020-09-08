using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.Common
{
    public class Result
    {
        public string Status { get; set; } = "Success";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Exception { get; set; }
        public string ResultData { get; set; }
        public string ErrorMessage { get; set; }
    }
}
