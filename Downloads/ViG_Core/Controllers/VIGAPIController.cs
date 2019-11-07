using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ISG_VIG_Brands.Library;
using ISG_VIG_Brands.Models;
using ISG_VIG_Brands.Models.POS.Rkeeper;
using ISG_VIG_Brands.Models.TransactionFailedResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VIG_CORE.Library;

namespace VIG_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VIGAPIController : ControllerBase
    {
        public static IConfiguration _configuration;
        public VIGAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Transaction")]
        public async Task<String> TransactionPost()
        {
            string response = string.Empty;
            XmlDocument doc = new XmlDocument();
            List<string> configs = new List<string>();
            string customerGetAPI = _configuration.GetSection("configvalues").GetSection("CustomerGetApi").Value;
            string TranscationPost = _configuration.GetSection("configvalues").GetSection("TranscationPost").Value;
            string VoucherIsreedemable = _configuration.GetSection("configvalues").GetSection("VoucherIsreedemable").Value;

            string VoucherRedeem = _configuration.GetSection("configvalues").GetSection("VoucherRedeem").Value;

            string VoucherGET = _configuration.GetSection("configvalues").GetSection("VoucherGET").Value;

            string username = _configuration.GetSection("configvalues").GetSection("RKeeperUserName").Value;


            string password = _configuration.GetSection("configvalues").GetSection("RKeeperUserPasswrod").Value;
            configs.Add(customerGetAPI);
            configs.Add(TranscationPost);
            configs.Add(VoucherIsreedemable);
            configs.Add(VoucherRedeem);
            configs.Add(VoucherGET);
            configs.Add(username);
            configs.Add(password);

            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.OK);

            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var result = sr.ReadToEndAsync();
                string xml = result.Result;
                if (xml == null || xml.Length < 4)
                {
                    throw new System.Exception("Invalid XML");
                }
                RkeeperTransactionclientrequest transactionrequest = xml.DeserializerXml<RkeeperTransactionclientrequest>();
                doc = await Task.Run(() => Rkeeper.PosttransctionAsync(transactionrequest, configs));
            }
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.LoadXml("<Root><message>some error occurred while processing the request</message></Root>");
            }
            else
            {
                XmlNode node = doc.SelectSingleNode("/Root");
                node.Attributes.RemoveAll();
            }
            return doc.InnerXml.Replace("utf-16", "utf-8");
            //  return response = await TransactionPostAsync();
        }
    }
}
