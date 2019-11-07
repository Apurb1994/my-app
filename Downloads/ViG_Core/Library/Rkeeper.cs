using ISG_VIG_Brands.Library;
using ISG_VIG_Brands.Models;
using ISG_VIG_Brands.Models.POS.Rkeeper;
using ISG_VIG_Brands.Models.RedeemedVoucher;
using ISG_VIG_Brands.Models.Rkeeper;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
using ISG_VIG_Brands.Models.TransactionFailedResponse;

namespace VIG_CORE.Library
{
    public class Rkeeper
    {
        static bool IsNotInterested = false;
        static bool isSuccess = true;
        static string mobileNumber = string.Empty;
        public static XmlDocument GetMember(RKeeperRequest search_Request, List<string> configs)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Enter GetMember");
            DateTime CurrentTime = DateTime.Now;
            ApiLayer api_Layer = new ApiLayer();
            string getCustomerResponse = string.Empty;
            VIG_request VIG_request = new VIG_request();
            User user = new User();
            POSTRKeeperCustomerResponse response = new POSTRKeeperCustomerResponse();
            try
            {
                //ApiIdentity identity = (ApiIdentity)System.Web.HttpContext.Current.User.Identity;
                //username = identity.User.UserName;
                //password = identity.User.Password;
                var client = new RestAPIRestClient(configs[0], HttpVerb.GET);
                Match mobileRegex = Regex.Match(search_Request.QRY.Card, @"(\d{7,})");
                string mobileRegexCountryCode = search_Request.QRY.Card.Substring(0, 2);
                //  Match mobileRegexCountryCode = Regex.Match(search_Request.QRY.Card, @"^(84)([0-2]{7})$");
                Match voucherRegex = Regex.Match(search_Request.QRY.Card, @"(\d{6})");
                if (!string.IsNullOrEmpty(search_Request.QRY.Card))
                {

                    if (mobileRegex.Success == true && mobileRegexCountryCode == "84")
                    {

                        client.MakeRequest("&mobile=" + search_Request.QRY.Card, VIG_request, configs[5], configs[6]);
                        if (!string.IsNullOrEmpty(VIG_request.ApiResponse))
                        {
                            _logger.Info("Capillary Response : " + VIG_request.ApiResponse);
                            ISG_VIG_Brands.Models.Customers.GetCustomerResponse customerGetResponse = JsonConvert.DeserializeObject<ISG_VIG_Brands.Models.Customers.GetCustomerResponse>(VIG_request.ApiResponse);

                            if (customerGetResponse.response.status.code == 200)
                            {
                                try
                                {
                                    if (customerGetResponse != null)
                                    {
                                        response.GetCardInfoEx.CardCode = customerGetResponse.response.customers.customer[0].mobile;
                                        response.GetCardInfoEx.Account = "0";
                                        response.GetCardInfoEx.Deleted = "0";
                                        response.GetCardInfoEx.Locked = "0";
                                        response.GetCardInfoEx.Seize = "0";
                                        response.GetCardInfoEx.Discount = "99";
                                        response.GetCardInfoEx.Bonus = "0";
                                        response.GetCardInfoEx.DiscLimit = "999999999999";
                                        response.GetCardInfoEx.Holder = customerGetResponse.response.customers.customer[0].firstname + " " + customerGetResponse.response.customers.customer[0].lastname;
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.Sum2 = "0";
                                        response.GetCardInfoEx.Sum3 = "0";
                                        response.GetCardInfoEx.Sum4 = "0";
                                        response.GetCardInfoEx.Sum5 = "0";
                                        response.GetCardInfoEx.Sum6 = "0";
                                        response.GetCardInfoEx.Sum7 = "0";
                                        response.GetCardInfoEx.Sum8 = "0";
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.WhyLock = "";
                                        response.GetCardInfoEx.ScrMessage = "** Message for SCREEN **";
                                        response.GetCardInfoEx.PrnMessage = "* Message for PRINT *";
                                        response.GetCardInfoEx.Result = "0";
                                        response.GetCardInfoEx.OutBuf = null;

                                    }
                                }
                                catch (Exception ex)
                                {

                                    throw;
                                }


                            }
                            else
                            {


                                try
                                {
                                    if (customerGetResponse != null)
                                    {
                                        _logger.Info("Customer not found , card number :" + search_Request.QRY.Card);
                                        response.GetCardInfoEx.CardCode = search_Request.QRY.Card;
                                        response.GetCardInfoEx.Account = "0";
                                        response.GetCardInfoEx.Deleted = "0";
                                        response.GetCardInfoEx.Locked = "0";
                                        response.GetCardInfoEx.Seize = "0";
                                        response.GetCardInfoEx.Discount = "99";
                                        response.GetCardInfoEx.Bonus = "0";
                                        response.GetCardInfoEx.DiscLimit = "999999999999";
                                        response.GetCardInfoEx.Holder = customerGetResponse.response.customers.customer[0].firstname + " " + customerGetResponse.response.customers.customer[0].lastname;
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.Sum2 = "0";
                                        response.GetCardInfoEx.Sum3 = "0";
                                        response.GetCardInfoEx.Sum4 = "0";
                                        response.GetCardInfoEx.Sum5 = "0";
                                        response.GetCardInfoEx.Sum6 = "0";
                                        response.GetCardInfoEx.Sum7 = "0";
                                        response.GetCardInfoEx.Sum8 = "0";
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.WhyLock = "";
                                        response.GetCardInfoEx.ScrMessage = "** Message for SCREEN **";
                                        response.GetCardInfoEx.PrnMessage = "* Message for PRINT *";
                                        response.GetCardInfoEx.Result = "1";
                                        response.GetCardInfoEx.OutBuf = null;
                                    }
                                }
                                catch (Exception ex)
                                {

                                    throw;
                                }


                            }
                        }
                    }
                    else

                    {
                        //This is voucher
                        string mobile = string.Empty;
                        client = new RestAPIRestClient(configs[4], HttpVerb.GET);
                        client.MakeRequest("&code=" + search_Request.QRY.Card, VIG_request, configs[5], configs[6]);
                        if (!string.IsNullOrEmpty(VIG_request.ApiResponse))
                        {
                            // Models.Voucher.Response customerGetResponse = JsonConvert.DeserializeObject<Models.Voucher.Response>(VIG_request.ApiResponse);
                            // string responseCode = VIG_request.ApiResponse;
                            XmlDocument docResponse = new XmlDocument();
                            docResponse.LoadXml(VIG_request.ApiResponse);

                            string status = docResponse.SelectSingleNode("//response//status//code").InnerText;
                            if (status == "200")
                            {
                                mobile = docResponse.SelectSingleNode("//response//coupons//coupon//customer//mobile").InnerText;
                            }
                            if (!string.IsNullOrEmpty(status) && status == "200")
                            {
                                VIG_request = new VIG_request();
                                client = new RestAPIRestClient(configs[2], HttpVerb.GET);
                                //client.MakeRequest("&mobile=" + request.mobile + "&code=" + request.coupon_code + "&details=extended"
                                client.MakeRequest("&mobile=" + mobile + "&code=" + search_Request.QRY.Card + "&details=extended", VIG_request, configs[5], configs[6]);
                                XmlDocument docIsReedeem = new XmlDocument();
                                docIsReedeem.LoadXml(VIG_request.ApiResponse);
                                string isRedeemstatus = docIsReedeem.SelectSingleNode("//response//status//code").InnerText;
                                if (isRedeemstatus == "200")
                                {
                                    string isDiscountCoupon = docIsReedeem.SelectSingleNode("//response//coupons//redeemable//series_info//discount_code").InnerText;
                                    decimal sum8 = Convert.ToDecimal(docIsReedeem.SelectSingleNode("//response//coupons//redeemable//series_info//discount_value").InnerText) * 100;

                                    if (isDiscountCoupon == "xyz_123")
                                    {
                                        response.GetCardInfoEx.CardCode = search_Request.QRY.Card;
                                        response.GetCardInfoEx.Account = "0";
                                        response.GetCardInfoEx.Deleted = "0";
                                        response.GetCardInfoEx.Locked = "0";
                                        response.GetCardInfoEx.Seize = "0";
                                        response.GetCardInfoEx.Discount = "0";
                                        response.GetCardInfoEx.Bonus = "0";
                                        response.GetCardInfoEx.Summa = "0";
                                        response.GetCardInfoEx.DiscLimit = "999999999999";
                                        response.GetCardInfoEx.Holder = docIsReedeem.SelectSingleNode("//response//coupons//redeemable//series_info//description").InnerText;
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.Sum2 = "0";
                                        response.GetCardInfoEx.Sum3 = "0";
                                        response.GetCardInfoEx.Sum4 = "0";
                                        response.GetCardInfoEx.Sum5 = "0";
                                        response.GetCardInfoEx.Sum6 = "0";
                                        response.GetCardInfoEx.Sum7 = "0";
                                        response.GetCardInfoEx.Sum8 = Convert.ToString(sum8);
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.WhyLock = "";
                                        response.GetCardInfoEx.ScrMessage = "** Message for SCREEN **";
                                        response.GetCardInfoEx.PrnMessage = "* Message for PRINT *";
                                        response.GetCardInfoEx.Result = "0";
                                        response.GetCardInfoEx.OutBuf = null;
                                    }
                                    else
                                    {
                                        response.GetCardInfoEx.CardCode = search_Request.QRY.Card;
                                        response.GetCardInfoEx.Account = "0";
                                        response.GetCardInfoEx.Deleted = "0";
                                        response.GetCardInfoEx.Locked = "0";
                                        response.GetCardInfoEx.Seize = "0";
                                        response.GetCardInfoEx.Discount = isDiscountCoupon;
                                        response.GetCardInfoEx.Bonus = "0";
                                        response.GetCardInfoEx.DiscLimit = "999999999999";
                                        response.GetCardInfoEx.Holder = docIsReedeem.SelectSingleNode("//response//coupons//redeemable//series_info//description").InnerText; ;
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.Sum2 = "0";
                                        response.GetCardInfoEx.Sum3 = "0";
                                        response.GetCardInfoEx.Sum4 = "0";
                                        response.GetCardInfoEx.Sum5 = "0";
                                        response.GetCardInfoEx.Sum6 = "0";
                                        response.GetCardInfoEx.Sum7 = "0";
                                        response.GetCardInfoEx.Sum8 = "0";
                                        response.GetCardInfoEx.DopInfo = "";
                                        response.GetCardInfoEx.WhyLock = "";
                                        response.GetCardInfoEx.ScrMessage = "** Message for SCREEN **";
                                        response.GetCardInfoEx.PrnMessage = "* Message for PRINT *";
                                        response.GetCardInfoEx.Result = "0";
                                        response.GetCardInfoEx.OutBuf = null;
                                    }
                                }
                                else
                                {
                                    response.GetCardInfoEx.CardCode = search_Request.QRY.Card;
                                    response.GetCardInfoEx.Account = "0";
                                    response.GetCardInfoEx.Deleted = "0";
                                    response.GetCardInfoEx.Locked = "1";
                                    response.GetCardInfoEx.Seize = "0";
                                    response.GetCardInfoEx.Discount = "0";
                                    response.GetCardInfoEx.Bonus = "0";
                                    response.GetCardInfoEx.Summa = "0";
                                    response.GetCardInfoEx.DiscLimit = "999999999999";
                                    response.GetCardInfoEx.Holder = "";
                                    response.GetCardInfoEx.DopInfo = "";
                                    response.GetCardInfoEx.Sum2 = "0";
                                    response.GetCardInfoEx.Sum3 = "0";
                                    response.GetCardInfoEx.Sum4 = "0";
                                    response.GetCardInfoEx.Sum5 = "0";
                                    response.GetCardInfoEx.Sum6 = "0";
                                    response.GetCardInfoEx.Sum7 = "0";
                                    response.GetCardInfoEx.Sum8 = "0";
                                    response.GetCardInfoEx.DopInfo = docIsReedeem.SelectSingleNode("//response//coupons//redeemable//item_status//message").InnerText;
                                    response.GetCardInfoEx.WhyLock = docIsReedeem.SelectSingleNode("//response//coupons//redeemable//item_status//message").InnerText;
                                    response.GetCardInfoEx.ScrMessage = "** Message for SCREEN **";
                                    response.GetCardInfoEx.PrnMessage = "* Message for PRINT *";
                                    response.GetCardInfoEx.Result = "0";
                                    response.GetCardInfoEx.OutBuf = null;
                                }

                            }

                            else
                            {
                                response.GetCardInfoEx.CardCode = search_Request.QRY.Card;
                                response.GetCardInfoEx.Account = "0";
                                response.GetCardInfoEx.Deleted = "0";
                                response.GetCardInfoEx.Locked = "0";
                                response.GetCardInfoEx.Seize = "0";
                                response.GetCardInfoEx.Discount = "0";
                                response.GetCardInfoEx.Bonus = "0";
                                response.GetCardInfoEx.Summa = "0";
                                response.GetCardInfoEx.DiscLimit = "999999999999";
                                response.GetCardInfoEx.Holder = "";
                                response.GetCardInfoEx.DopInfo = "";
                                response.GetCardInfoEx.Sum2 = "0";
                                response.GetCardInfoEx.Sum3 = "0";
                                response.GetCardInfoEx.Sum4 = "0";
                                response.GetCardInfoEx.Sum5 = "0";
                                response.GetCardInfoEx.Sum6 = "0";
                                response.GetCardInfoEx.Sum7 = "0";
                                response.GetCardInfoEx.Sum8 = "0";
                                response.GetCardInfoEx.DopInfo = "Coupon Not Found";
                                response.GetCardInfoEx.WhyLock = "";
                                response.GetCardInfoEx.ScrMessage = "** Coupon Not Found **";
                                response.GetCardInfoEx.PrnMessage = "* Coupon Not Found *";
                                response.GetCardInfoEx.Result = "1";
                                response.GetCardInfoEx.OutBuf = null;
                            }
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured : {0}", ex.Message);

            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.SerializerXml());
            return doc;
            //return response;
        }

        public static async Task<XmlDocument> GetMemberAsync(RKeeperRequest search_Request, List<string> configs)
        {
            return await Task.Run(() => GetMember(search_Request, configs));
        }

            public static XmlDocument Posttransction(RkeeperTransactionclientrequest transactionrequest, List<string> configs)
        {
            bool IsTransaction = false;
            Logger _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Enter Posttransction");
            DateTime CurrentTime = DateTime.Now;
            ApiLayer api_Layer = new ApiLayer();
            //string username = string.Empty;
            //string password = string.Empty;
            bool IsModify = false;
            string getCustomerResponse = string.Empty;
            VIG_request VIG_request = new VIG_request();
            User user = new User();
            ISG_VIG_Brands.Models.TransactionsRequest.Capillary_Transaction_Request root = new ISG_VIG_Brands.Models.TransactionsRequest.Capillary_Transaction_Request();

            POSTRkeeperTranscationresponce response = new POSTRkeeperTranscationresponce();
            TransactionResponseRoot responseTrans = new TransactionResponseRoot();
            TransactionFailedResponse failedResponse = new TransactionFailedResponse();
            var mobileIdentifiers = transactionrequest.INPBUF.CHECK.EXTINFO.INTERFACES.INTERFACE.HOLDERS.ITEM;
            foreach (var identifier in mobileIdentifiers)
            {
                VoucherRedeem redeem = new VoucherRedeem();
                Match mobileRegex = Regex.Match(identifier.cardcode, @"(\d{7,})");
                string mobileRegexCountryCode = identifier.cardcode.Substring(0, 2);
                if (mobileRegex.Success == true && mobileRegexCountryCode == "84")
                {
                    mobileNumber = identifier.cardcode;
                    break;
                }
                else
                {
                    var clientVoucher = new RestAPIRestClient(configs[4], HttpVerb.GET);
                    clientVoucher.MakeRequest("&code=" + identifier.cardcode, VIG_request, configs[5], configs[6]);
                    _logger.Info("Identifier is voucher");
                    if (!string.IsNullOrEmpty(VIG_request.ApiResponse))
                    {
                        // Models.Voucher.Response customerGetResponse = JsonConvert.DeserializeObject<Models.Voucher.Response>(VIG_request.ApiResponse);
                        // string responseCode = VIG_request.ApiResponse;
                        XmlDocument docResponse = new XmlDocument();
                        docResponse.LoadXml(VIG_request.ApiResponse);

                        string status = docResponse.SelectSingleNode("//response//status//code").InnerText;
                        if (status == "200")
                        {
                            mobileNumber = docResponse.SelectSingleNode("//response//coupons//coupon//customer//mobile").InnerText;
                            if (string.IsNullOrEmpty(mobileNumber))
                            {
                                mobileNumber = "84986084229";
                                IsNotInterested = true;
                            }
                            break;
                        }
                    }

                }
            }
            if (transactionrequest.INPBUF.CHECK.chmode == "1" || transactionrequest.INPBUF.CHECK.chmode == "3")
            {

                // string mobile = string.Empty;
                _logger.Info("Ch Mode is 1 or 3");
                var client = new RestAPIRestClient(configs[0], HttpVerb.GET);
                var identifiers = transactionrequest.INPBUF.CHECK.EXTINFO.INTERFACES.INTERFACE.HOLDERS.ITEM;
                foreach (var identifier in identifiers)
                {

                    Match mobileRegex = Regex.Match(identifier.cardcode, @"(\d{7,})");
                    string mobileRegexCountryCode = identifier.cardcode.Substring(0, 2);
                    if (mobileRegex.Success == true && mobileRegexCountryCode == "84")
                    {
                        var clientMobile = new RestAPIRestClient(configs[0], HttpVerb.GET);
                        mobileNumber = identifier.cardcode;
                        VIG_request VIG_requestmob = new VIG_request();
                        clientMobile.MakeRequest("&mobile=" + identifier.cardcode.ToString(), VIG_requestmob, configs[5], configs[6]);
                        ISG_VIG_Brands.Models.Customers.GetCustomerResponse customerGetResponse = new ISG_VIG_Brands.Models.Customers.GetCustomerResponse();
                        _logger.Info("Response From Capillary {0}", VIG_requestmob.ApiResponse);
                        customerGetResponse = JsonConvert.DeserializeObject<ISG_VIG_Brands.Models.Customers.GetCustomerResponse>(VIG_requestmob.ApiResponse);
                        if (customerGetResponse.response.status.code != 500)
                        {
                            IsTransaction = true;
                            responseTrans.TransactionsEx.Result = "0";
                        }
                        else
                        {
                            IsTransaction = false;
                            failedResponse.TransactionsEx.Result = "1";
                            failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = customerGetResponse.response.customers.customer[0].item_status.code.ToString();
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = customerGetResponse.response.customers.customer[0].item_status.message.ToString();
                            break;

                        }


                    }
                    else if (transactionrequest.INPBUF.CHECK.chmode != "3")
                    {
                        _logger.Info("Card Type is coupon");
                        VIG_request VIG_requestcoupon = new VIG_request();
                        client = new RestAPIRestClient(configs[2], HttpVerb.GET);
                        if (!String.IsNullOrEmpty(mobileNumber) && identifier.cardcode != mobileNumber)
                        {
                            client.MakeRequest("&mobile=" + mobileNumber + "&code=" + identifier.cardcode.ToString() + "&details=extended", VIG_requestcoupon, configs[5], configs[6]);
                            XmlDocument docResponse = new XmlDocument();
                            docResponse.LoadXml(VIG_requestcoupon.ApiResponse);
                            _logger.Info("Response From Capillary {0}", VIG_requestcoupon.ApiResponse);
                            string status = docResponse.SelectSingleNode("//response//status//code").InnerText;
                            if (status == "200")
                            {
                                IsTransaction = true;
                                responseTrans.TransactionsEx.Result = "0";
                            }
                            else
                            {
                                isSuccess = false;
                                IsTransaction = false;
                                failedResponse.TransactionsEx.Result = "1";
                                failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                                failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = docResponse.SelectSingleNode("//response//coupons//redeemable//item_status//code").InnerText;
                                failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = identifier.cardcode + " : " + docResponse.SelectSingleNode("//response//coupons//redeemable//item_status//message").InnerText;
                                break;
                            }
                        }
                        else
                        {
                            failedResponse.TransactionsEx.Result = "1";
                            failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = "500";
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = "Coupon Not Found";
                            break;
                        }
                    }
                }


            }
            else
            {
                failedResponse.TransactionsEx.Result = "1";
                failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = "500";
                failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = "Ch Mode is not 1 0r 3";
            }
            if (IsTransaction == true)
            {
                isSuccess = ChMode10(transactionrequest, _logger, root, failedResponse, responseTrans, configs);
            }
            XmlDocument doc = new XmlDocument();
            if (isSuccess)
            {
                doc.LoadXml(responseTrans.SerializerXml());
            }
            else
            {
                doc.LoadXml(failedResponse.SerializerXml());

            }
            return doc;
            //return response;
        }

        public static async Task<XmlDocument> PosttransctionAsync(RkeeperTransactionclientrequest transactionrequest, List<string> configs)
        {
            return await Task.Run(() => Posttransction(transactionrequest, configs));
        }
        private static bool ChMode10(RkeeperTransactionclientrequest transactionrequest, Logger _logger, ISG_VIG_Brands.Models.TransactionsRequest.Capillary_Transaction_Request root, TransactionFailedResponse failedResponse, TransactionResponseRoot responseTrans, List<string> configs)
        {
            //bool isTrue = false;
            // TransactionResponseRoot responseTrans = new TransactionResponseRoot();
            //TransactionFailedResponse failedResponse = new TransactionFailedResponse();
            var sales = from sale in transactionrequest.INPBUF.CHECK.CHECKDATA.CHECKLINES.LINE
                        where !sale.type.Contains("modify")
                        select sale;
            var salesCombo = from combo in transactionrequest.INPBUF.CHECK.CHECKDATA.CHECKLINES.LINE
                             where combo.type.Contains("combo")
                             select combo;
            var salesmodify = from combo in transactionrequest.INPBUF.CHECK.CHECKDATA.CHECKLINES.LINE
                              where combo.type.Contains("modify")
                              select combo;
            double finalbillamount = 0;
            double billDiscount = 0;


            ISG_VIG_Brands.Models.TransactionsRequest.LineItem comboitem = new ISG_VIG_Brands.Models.TransactionsRequest.LineItem();
            if (salesmodify.Count() > 0)
            {
                foreach (var mod in salesmodify)
                {
                    foreach (var sale in sales)
                    {
                        if (mod.parent == sale.uni)
                        {
                            ISG_VIG_Brands.Models.TransactionsRequest.LineItem item = new ISG_VIG_Brands.Models.TransactionsRequest.LineItem();
                            double sum = Convert.ToDouble(sale.sum);
                            item.item_code = sale.code;
                            item.description = sale.name;
                            item.rate = sale.price;
                            item.qty = sale.quantity;
                            item.value = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity));
                            item.amount = sale.sum;
                            item.discount = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                            billDiscount += Convert.ToDouble(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                            finalbillamount += sum;
                            item.addon_items.addon_item.Add(
                                new ISG_VIG_Brands.Models.TransactionsRequest.AddonItem()
                                {
                                    item_code = mod.code,
                                    description = mod.name,
                                    quantity = mod.quantity,
                                    rate = mod.price,
                                    value = mod.sum
                                });
                            root.root.transaction[0].line_items.line_item.Add(item);
                            //IsModify = true;

                        }
                        else
                        {
                            if (sale.type != "combo" && sale.parent == null)
                            {
                                ISG_VIG_Brands.Models.TransactionsRequest.LineItem singleitem = new ISG_VIG_Brands.Models.TransactionsRequest.LineItem();
                                double sum1 = Convert.ToDouble(sale.sum);
                                singleitem.item_code = sale.code;
                                singleitem.description = sale.name;
                                singleitem.rate = sale.price;
                                singleitem.qty = sale.quantity;
                                singleitem.value = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity));
                                singleitem.amount = sale.sum;
                                singleitem.variant = sale.uni;
                                billDiscount += Convert.ToDouble(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                                singleitem.discount = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                                root.root.transaction[0].line_items.line_item.Add(singleitem);
                                finalbillamount += sum1;

                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var sale in sales)
                {
                    if (sale.type != "combo" && sale.parent == null)
                    {
                        billDiscount += Convert.ToDouble(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                        ISG_VIG_Brands.Models.TransactionsRequest.LineItem singleitem = new ISG_VIG_Brands.Models.TransactionsRequest.LineItem();
                        double sum1 = Convert.ToDouble(sale.sum);
                        singleitem.item_code = sale.code;
                        singleitem.description = sale.name;
                        singleitem.rate = sale.price;
                        singleitem.qty = sale.quantity;
                        singleitem.value = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity));
                        singleitem.amount = sale.sum;
                        singleitem.variant = sale.uni;
                        singleitem.discount = Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                        root.root.transaction[0].line_items.line_item.Add(singleitem);
                        finalbillamount += sum1;

                    }
                }
            }
            foreach (var singleCombo in salesCombo)
            {
                ISG_VIG_Brands.Models.TransactionsRequest.LineItem item = new ISG_VIG_Brands.Models.TransactionsRequest.LineItem();
                double sum = Convert.ToDouble(singleCombo.sum);
                item.item_code = singleCombo.code;
                item.description = singleCombo.name;
                item.rate = singleCombo.price;
                item.qty = singleCombo.quantity;
                //Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity));
                item.value = Convert.ToString(Convert.ToDouble(singleCombo.price) * Convert.ToDouble(singleCombo.quantity));
                item.amount = singleCombo.sum;
                //Convert.ToString(Convert.ToDouble(sale.price) * Convert.ToDouble(sale.quantity) - Convert.ToDouble(sale.sum));
                billDiscount += Convert.ToDouble(Convert.ToDouble(singleCombo.price) * Convert.ToDouble(singleCombo.quantity) - Convert.ToDouble(singleCombo.sum));
                item.discount = Convert.ToString(Convert.ToDouble(singleCombo.price) * Convert.ToDouble(singleCombo.quantity) - Convert.ToDouble(singleCombo.sum));
                finalbillamount += sum;
                root.root.transaction[0].line_items.line_item.Add(item);
                foreach (var sale in sales)
                {
                    if (sale.parent != null && sale.parent == singleCombo.uni)
                    {
                        item.combo_items.combo_item.Add(
                        new ISG_VIG_Brands.Models.TransactionsRequest.ComboItem()
                        {
                            item_code = sale.code,
                            description = sale.name,
                            quantity = sale.quantity
                        }
                            );
                    }
                }
            }

            try
            {

                try
                {
                    var IPOSpaymentmodes = transactionrequest.INPBUF.CHECK.CHECKDATA.CHECKPAYMENTS.PAYMENT;
                    foreach (var payment in IPOSpaymentmodes)
                    {

                        ISG_VIG_Brands.Models.TransactionsRequest.Payment cappayments = new ISG_VIG_Brands.Models.TransactionsRequest.Payment();
                        cappayments.mode = payment.name;
                        cappayments.value = payment.sum;
                        root.root.transaction[0].payment_details.payment.Add(cappayments);
                    }

                }
                catch (Exception ex)
                {

                    // logger.Error("payment mode", ex);
                }

                String mobilenovaild = root.root.transaction[0].customer.mobile = mobileNumber;
                Match mobileno = Regex.Match(mobilenovaild, @"(\d{6,})");
                if (mobileno.Success)
                {
                    _logger.Info("Regular Bill or Return Bill");
                    if (IsNotInterested == false)
                    {
                        if (transactionrequest.INPBUF.CHECK.chmode == "1")
                        {
                            root.root.transaction[0].type = "regular";
                            root.root.transaction[0].customer.mobile = mobileNumber;
                        }
                        else
                        {
                            root.root.transaction[0].type = "return";
                            root.root.transaction[0].return_type = "Line_item";
                            root.root.transaction[0].customer.mobile = mobileNumber;
                        }
                    }
                    else
                    {
                        root.root.transaction[0].type = "not_interested";
                    }
                    root.root.transaction[0].number = transactionrequest.INPBUF.CHECK.CHECKDATA.checknum;
                    root.root.transaction[0].amount = finalbillamount.ToString();

                    //root.root.transaction[0].discount = transactionrequest.Transactions.TransactionsEx[0].Summa;
                    root.root.transaction[0].discount = Convert.ToString(billDiscount);
                }
                else
                {
                    root.root.transaction[0].type = "not_interested";
                    root.root.transaction[0].number = transactionrequest.INPBUF.CHECK.CHECKDATA.checknum;
                    root.root.transaction[0].amount = finalbillamount.ToString();

                }

                var client = new RestAPIRestClient(configs[1], HttpVerb.POST);
                string request = root.Serialise();
                _logger.Info("Intouch Transaction Request {0}", request);
                if (request != null)
                {

                    List<ISG_VIG_Brands.Models.Transaction> trans = new List<ISG_VIG_Brands.Models.Transaction>();
                    trans.Add(
                        new ISG_VIG_Brands.Models.Transaction()
                        {
                            number = transactionrequest.INPBUF.CHECK.CHECKDATA.checknum,
                            amount = finalbillamount.ToString()
                        });
                    VIG_request vIG_Request = new VIG_request();
                    vIG_Request.requestXml = vIG_Request.ApiRequest = request;
                    _logger.Info("Calling Transaction Post API");
                    client.MakeRequest("", vIG_Request, configs[5], configs[6]);
                    ISG_VIG_Brands.Models.Transactions.POSTTranscationResponse transactionResponse = null;
                    if (!string.IsNullOrWhiteSpace(vIG_Request.ApiResponse))
                    {
                        transactionResponse = vIG_Request.ApiResponse.Deserializer<ISG_VIG_Brands.Models.Transactions.POSTTranscationResponse>();


                        if (transactionResponse.response.status.code == "200" && transactionrequest.INPBUF.CHECK.chmode == "1")
                        {
                            _logger.Info("Transaction post Successs");
                            try
                            {
                                RedeemedVoucherResponse reedemResponse = new RedeemedVoucherResponse();
                                string mobile = string.Empty;
                                var clientRedeem = new RestAPIRestClient(configs[3], HttpVerb.POST);
                                var identifiers = transactionrequest.INPBUF.CHECK.EXTINFO.INTERFACES.INTERFACE.HOLDERS.ITEM;
                                foreach (var identifier in identifiers)
                                {
                                    VoucherRedeem redeem = new VoucherRedeem();
                                    Match mobileRegex = Regex.Match(identifier.cardcode, @"(\d{7,})");
                                    string mobileRegexCountryCode = identifier.cardcode.Substring(0, 2);
                                    if (mobileRegex.Success == true && mobileRegexCountryCode == "84" && !String.IsNullOrEmpty(mobileNumber))
                                    {
                                        mobile = mobileNumber;

                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(mobileNumber))
                                            redeem.root.coupon.Add(new ISG_VIG_Brands.Models.Coupon()
                                            {
                                                customer = new ISG_VIG_Brands.Models.Customer()
                                                {
                                                    mobile = mobileNumber
                                                },
                                                code = identifier.cardcode,
                                                transaction = trans

                                            });
                                        var clientVoucherRedeem = new RestAPIRestClient(configs[3], HttpVerb.POST);
                                        string requestRedeem = redeem.Serialise();
                                        VIG_request vIG_RequestRedeem = new VIG_request();
                                        vIG_RequestRedeem.requestXml = vIG_RequestRedeem.ApiRequest = requestRedeem;
                                        clientVoucherRedeem.MakeRequest("", vIG_RequestRedeem, configs[5], configs[6]);
                                        reedemResponse = vIG_RequestRedeem.ApiResponse.Deserializer<RedeemedVoucherResponse>();
                                        if (reedemResponse.response.status.code == 500)
                                        {
                                            isSuccess = false;
                                            failedResponse.TransactionsEx.Result = "1";
                                            failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = reedemResponse.response.coupons.coupon.item_status.code.ToString();
                                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = "Coupon Code : " + identifier.cardcode + " : " + reedemResponse.response.coupons.coupon.item_status.message.ToString();
                                            break;
                                        }
                                        else
                                        {
                                            responseTrans.TransactionsEx.Result = "0";
                                            isSuccess = true;
                                        }

                                    }

                                }
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }

                        else if (transactionResponse.response.status.code == "200" && transactionrequest.INPBUF.CHECK.chmode == "3")
                        {
                            responseTrans.TransactionsEx.Result = "0";
                            isSuccess = true;
                        }
                        else
                        {
                            isSuccess = false;
                            failedResponse.TransactionsEx.Result = "1";
                            failedResponse.TransactionsEx.OutBuf.OutKind = "1";
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Error_code = transactionResponse.response.transactions.transaction[0].item_status.code.ToString();
                            failedResponse.TransactionsEx.OutBuf.TRRESPONSE.Err_text = transactionResponse.response.transactions.transaction[0].item_status.message;
                        }
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
            return isSuccess;
        }
    }
}
