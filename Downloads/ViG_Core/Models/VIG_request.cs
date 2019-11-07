using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Library
{
    public class VIG_request
    {
        public string ApiRequest { get; set; }
        public string ApiResponse { get; set; }
        public bool Success { get; set; }
        public int Attempt { get; set; }
        public int Id { get; set; }
        public string requestXml { get; set; }
        public string APIURL { get; set; }
        public string ErrorMessge { get; set; }
        public string ErrorType { get; set; }
        public string StoreCode { get; set; }
        //public StoreDetail Store { get; set; }
        public string Request_Id { get; set; }
    }
}