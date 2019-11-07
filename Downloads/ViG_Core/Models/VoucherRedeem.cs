using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISG_VIG_Brands.Models
{
    public class Customer
    {
        public string mobile { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class CustomField
    {
        public Field field { get; set; }
    }

    public class Transaction
    {
        public string number { get; set; }
        public string amount { get; set; }
    }

    public class Coupon
    {
        public Coupon()
        {
            transaction = new List<Transaction>();
            customer = new Customer();
        }
        public string code { get; set; }
        public Customer customer { get; set; }
        public List<CustomField> custom_fields { get; set; }
        public List<Transaction> transaction { get; set; }
    }

    public class Root
    {
        public Root()
        {
            coupon = new List<Coupon>();
        }
        public List<Coupon> coupon { get; set; }
    }

    public class VoucherRedeem
    {
        public VoucherRedeem()
        {
            root = new Root();
        }
        public Root root { get; set; }
    }
}