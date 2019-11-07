using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Diagnostics;
using System.IO;
using NLog;

namespace ISG_VIG_Brands.Library
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public class RestAPIRestClient
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
     //   static readonly IApplicationLogger logger = Converter.logger;
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestAPIRestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public RestAPIRestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public RestAPIRestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = "";
        }

        public RestAPIRestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }


        //public string MakeRequest()
        //{
        //    return MakeRequest(string.Empty, string.Empty, string.Empty, string.Empty, null);
        //}
        public void MakeRequest(string parameters, VIG_request request_VIG, string username, string password)
        {
            try
            {

                string url = EndPoint + parameters;
                _logger.Info("posting url: " + url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                NetworkCredential credentials = new NetworkCredential(username, password);
                request.Credentials = credentials;
                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = ContentType;
                request.Timeout = 3000000;
                request.KeepAlive = false;
                request.PreAuthenticate = true;
                request.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                System.Net.ServicePointManager.Expect100Continue = false;
                request.MediaType = "application/json;charset=\"utf-8\"";

                if (!string.IsNullOrEmpty(request_VIG.requestXml) && Method == HttpVerb.POST)
                {

                    request.Method = "POST";
                    byte[] postBuffer = Encoding.UTF8.GetBytes(request_VIG.requestXml);
                    request.ContentLength = postBuffer.Length;
                    var postStream = request.GetRequestStream();
                    try
                    {
                        postStream.Write(postBuffer, 0, postBuffer.Length);
                        postStream.Close();
                    }
                    catch (Exception ex)
                    {
                        // logger.Debug(string.Format("Critical failure. Exception details: {0}", ex.ToString()), "Http Web Post Request Failed", EventLogEntryType.Error, false);

                    }
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                while (!reader.EndOfStream)
                                {

                                    request_VIG.ApiResponse += reader.ReadToEnd();
                                }
                                request_VIG.Request_Id = response.Headers["X-Cap-RequestID"] != null ? response.Headers["X-Cap-RequestID"].ToString() : string.Empty;
                                _logger.Info("Capillary Request Id {0}", request_VIG.Request_Id);
                            }

                        //return request_landmark;
                    }
                }
            }
            catch (WebException ex)
            {
                request_VIG.ApiResponse = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

            }
        }


    }
}