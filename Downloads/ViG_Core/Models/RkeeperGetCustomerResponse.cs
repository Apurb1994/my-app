using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Models.Customers
{

   public class Status
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public string success_count { get; set; }
    }

    public class RegisteredStore
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class RegisteredTill
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class FraudDetails
    {
        public string status { get; set; }
        public string marked_by { get; set; }
        public string modified_on { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class CustomFields
    {
        public Field[] field { get; set; }
    }

    public class ExtendedFields
    {
        public object[] field { get; set; }
    }

    public class Transactions
    {
        public object[] transaction { get; set; }
    }

    public class Coupon
    {
        public string id { get; set; }
        public string series_id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string created_date { get; set; }
        public string valid_till { get; set; }
        public string redeemed { get; set; }
    }

    public class Coupons
    {
        public Coupon[] coupon { get; set; }
    }

    public class Warnings
    {
        public object[] warning { get; set; }
    }

    public class ItemStatus
    {
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public Warnings warnings { get; set; }
    }

    public class Customer
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string external_id { get; set; }
        public string lifetime_points { get; set; }
        public int lifetime_purchases { get; set; }
        public string loyalty_points { get; set; }
        public string current_slab { get; set; }
        public string registered_on { get; set; }
        public string updated_on { get; set; }
        public string type { get; set; }
        public string source { get; set; }
        public object[] identifiers { get; set; }
        public object gender { get; set; }
        public string registered_by { get; set; }
        public RegisteredStore registered_store { get; set; }
        public RegisteredTill registered_till { get; set; }
        public FraudDetails fraud_details { get; set; }
        public string trackers { get; set; }
        public object current_nps_status { get; set; }
        public CustomFields custom_fields { get; set; }
        public ExtendedFields extended_fields { get; set; }
        public Transactions transactions { get; set; }
        public Coupons coupons { get; set; }
        public object[] notes { get; set; }
        public ItemStatus item_status { get; set; }
    }

    public class Customers
    {
        public Customer[] customer { get; set; }
    }

    public class Response
    {
        public Status status { get; set; }
        public Customers customers { get; set; }
    }

    public class GetCustomerResponse
    {
        public Response response { get; set; }
    }

}