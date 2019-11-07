using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace ISG_VIG_Brands.Models.Voucher
{
    [XmlRoot(ElementName = "status")]
    public class Status
    {
        [XmlElement(ElementName = "success")]
        public string Success { get; set; }
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }
    }

    [XmlRoot(ElementName = "customer")]
    public class Customer
    {
        [XmlElement(ElementName = "user_id")]
        public string User_id { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }

    [XmlRoot(ElementName = "issued_store")]
    public class Issued_store
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "redemption_info")]
    public class Redemption_info
    {
        [XmlElement(ElementName = "redeemed")]
        public string Redeemed { get; set; }
        [XmlElement(ElementName = "redeemed_on")]
        public string Redeemed_on { get; set; }
        [XmlElement(ElementName = "redeemed_at")]
        public string Redeemed_at { get; set; }
    }

    [XmlRoot(ElementName = "redeemed_by")]
    public class Redeemed_by
    {
        [XmlElement(ElementName = "firstname")]
        public string Firstname { get; set; }
        [XmlElement(ElementName = "lastname")]
        public string Lastname { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
    }

    [XmlRoot(ElementName = "store")]
    public class Store
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "item_status")]
    public class Item_status
    {
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }
    }

    [XmlRoot(ElementName = "coupon")]
    public class Coupon
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "valid_till")]
        public string Valid_till { get; set; }
        [XmlElement(ElementName = "issued_on")]
        public string Issued_on { get; set; }
        [XmlElement(ElementName = "customer")]
        public Customer Customer { get; set; }
        [XmlElement(ElementName = "pincode")]
        public string Pincode { get; set; }
        [XmlElement(ElementName = "issued_store")]
        public Issued_store Issued_store { get; set; }
        [XmlElement(ElementName = "series_id")]
        public string Series_id { get; set; }
        [XmlElement(ElementName = "is_absolute")]
        public string Is_absolute { get; set; }
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "redemption_info")]
        public Redemption_info Redemption_info { get; set; }
        [XmlElement(ElementName = "redeemed_by")]
        public Redeemed_by Redeemed_by { get; set; }
        [XmlElement(ElementName = "store")]
        public Store Store { get; set; }
        [XmlElement(ElementName = "transaction")]
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "item_status")]
        public Item_status Item_status { get; set; }
    }

    [XmlRoot(ElementName = "coupons")]
    public class Coupons
    {
        [XmlElement(ElementName = "coupon")]
        public Coupon Coupon { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class Response
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "coupons")]
        public Coupons Coupons { get; set; }
    }

}