using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Models.RedeemedVoucher
{
    public class Status
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Customer
    {
        public string mobile { get; set; }
        public string email { get; set; }
        public object external_id { get; set; }
    }

    public class Transaction
    {
        public string amount { get; set; }
        public string number { get; set; }
    }

    public class ItemStatus
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Coupon
    {
        public Coupon()
        {
            item_status = new ItemStatus();
            customer = new Customer();
            transaction = new Transaction();
        }
        public Customer customer { get; set; }
        public string code { get; set; }
        public string discount_code { get; set; }
        public string series_code { get; set; }
        public List<object> series_info { get; set; }
        public string is_absolute { get; set; }
        public string coupon_value { get; set; }
        public Transaction transaction { get; set; }
        public List<object> side_effects { get; set; }
        public ItemStatus item_status { get; set; }
    }

    public class Coupons
    {
        public Coupons()
        {
            coupon = new Coupon();
        }
        public Coupon coupon { get; set; }
    }

    public class Response
    {
        public Response()
        {
            status = new Status();
            coupons = new Coupons();
        }
        public Status status { get; set; }
        public Coupons coupons { get; set; }
    }

    public class RedeemedVoucherResponse
    {
        public RedeemedVoucherResponse()
        {
            response = new Response();
        }
        public Response response { get; set; }
    }
}