using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Library
{
    public class ApiLayer
    {
        VIG_request VIG_request = new VIG_request();
        public string GetCustomer(string MemberId, string username, string password)
        {
            try
            {
                // string customerGetAPI = ConfigurationManager.AppSettings["CustomerGetApi"].ToString();
                string customerGetAPI = "";
                string response = string.Empty;

                var client = new RestAPIRestClient(customerGetAPI, HttpVerb.GET);

                VIG_request.ApiRequest = "&MemberId" + MemberId;
                //response = client.MakeRequest(string.Empty, Puma_request, username, password);
                //response = client.MakeRequest("&mobile=" + mobile, null, username, password);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}