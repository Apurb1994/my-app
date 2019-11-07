using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ISG_VIG_Brands.Models.TransactionsRequest
{
    public class Customer
    {
        public string mobile { get; set; }
        public string email { get; set; }
        public string external_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }


    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class ExtendedFields
    {
        public IList<Field> field { get; set; }
    }

    public class CustomFields
    {
        public IList<Field> field { get; set; }
    }

    public class Attribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Attributes
    {
        public IList<Attribute> attribute { get; set; } = new List<Attribute>();
    }

    public class Payment
    {
        public object mode { get; set; }
        public object value { get; set; }
        //public Attributes attributes { get; set; }
        public string notes { get; set; }
    }

    public class PaymentDetails
    {
        public IList<Payment> payment { get; set; } = new List<Payment>();
    }

    public class AddonItem
    {
        public string item_code { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
    }

    public class AddonItems
    {
        public IList<AddonItem> addon_item { get; set; } = new List<AddonItem>();
    }

    public class ComboItem
    {
        public string item_code { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }

    }

    public class ComboItems
    {

        public IList<ComboItem> combo_item { get; set; } = new List<ComboItem>();
    }

    public class SplitItem
    {
        public string item_code { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
    }

    public class SplitItems
    {
        public IList<SplitItem> split_item { get; set; } = new List<SplitItem>();
    }

    public class LineItem
    {
        public LineItem()
        {
            combo_items = new ComboItems();
            addon_items = new AddonItems();
        }
        public string serial { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public string item_code { get; set; }
        public ExtendedFields extended_fields { get; set; }
        public string variant { get; set; }
        public AddonItems addon_items { get; set; }
        public ComboItems combo_items { get; set; }
        //public SplitItems split_items { get; set; }
        public string qty { get; set; }
        public string discount { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        //public Attributes attributes { get; set; } = new Attributes();
        public string transaction_number { get; set; }
    }

    public class LineItems
    {
        public IList<LineItem> line_item { get; set; } = new List<LineItem>();
    }

    public class AssociateDetails
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Transaction
    {
        public string bill_client_id { get; set; }
        public string type { get; set; }

        public string return_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string lineitem_type { get; set; }
        public string number { get; set; }
        public string amount { get; set; }
        public string currency_code { get; set; }
        public string notes { get; set; }
        public string billing_time { get; set; }
        public string gross_amount { get; set; }
        public string discount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer customer { get; set; } = new Customer();
        public ExtendedFields extended_fields { get; set; } = new ExtendedFields();
        public PaymentDetails payment_details { get; set; } = new PaymentDetails();
        public CustomFields custom_fields { get; set; } = new CustomFields();
        public LineItems line_items { get; set; } = new LineItems();
    }

    public class Root
    {
        public IList<Transaction> transaction { get; set; } = new Transaction[1] { new Transaction() };
    }

    public class Capillary_Transaction_Request
    {
        public Root root { get; set; } = new Root();
    }
}
