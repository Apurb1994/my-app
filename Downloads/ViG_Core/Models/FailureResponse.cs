using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Models
{
    public class Status_Fail
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Response_Fail
    {
        public Status_Fail status { get; set; }
    }

    public class FailureResponse
    {
        public Response_Fail response { get; set; }
    }
}