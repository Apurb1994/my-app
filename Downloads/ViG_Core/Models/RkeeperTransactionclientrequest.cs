
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using System.Xml.Serialization;


namespace ISG_VIG_Brands.Models.POS.Rkeeper
{
    [Serializable]
    [XmlRoot("TransactionsEx")]
    public class TransactionsEx
    {
        [XmlAttribute]
        public string @Card { get; set; }
        public string @PersonID { get; set; }
        public string @Account { get; set; }
        public string @Kind { get; set; }
        public string @Summa { get; set; }
        public string @Restaurant { get; set; }
        public string @RKDate { get; set; }
        public string @RKUnit { get; set; }
        public string @RKCheck { get; set; }
        public string @VatSumA { get; set; }
        public string @VatPrcA { get; set; }
        public string @VatSumB { get; set; }
        public string @VatPrcB { get; set; }
        public string @VatSumC { get; set; }
        public string @VatPrcC { get; set; }
        public string @VatSumD { get; set; }
        public string @VatPrcD { get; set; }
        public string @VatSumE { get; set; }
        public string @VatPrcE { get; set; }
        public string @VatSumF { get; set; }
        public string @VatPrcF { get; set; }
        public string @VatSumG { get; set; }
        public string @VatPrcG { get; set; }
        public string @VatSumH { get; set; }
        public string @VatPrcH { get; set; }
    }
    [Serializable]
    [XmlRoot("Transactions")]
    public class Transactions
    {
        [XmlElement("TransactionsEx")]
        public TransactionsEx[] TransactionsEx { get; set; }
    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class ITEM
    {
        [XmlAttribute]
        public string @cardcode { get; set; }
    }
    [Serializable]
    [XmlRoot("HOLDERS")]
    public class HOLDERS
    {
        [XmlElement]
        public List<ITEM> ITEM { get; set; }
    }
    [Serializable]
    [XmlRoot("INTERFACE")]
    public class INTERFACE
    {
        public string @type { get; set; }
        public string @id { get; set; }
        public string @mode { get; set; }
        public string @interface { get; set; }
        public HOLDERS HOLDERS { get; set; }
    }
    [Serializable]
    [XmlRoot("INTERFACES")]
    public class INTERFACES
    {
        public string @current { get; set; }
        public INTERFACE INTERFACE { get; set; }
    }
    [Serializable]
    [XmlRoot("EXTINFO")]

    public class EXTINFO
    {
        public string @reservation { get; set; }
        public INTERFACES INTERFACES { get; set; }
    }
    [Serializable]
    [XmlRoot("PERSON")]
    public class PERSON
    {
        public string @id { get; set; }
        public string @name { get; set; }
        public string @code { get; set; }
        public string @role { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECKPERSONS")]
    public class CHECKPERSONS
    {
        public string @count { get; set; }
        public PERSON PERSON { get; set; }
    }
    [Serializable]
    [XmlRoot("TAX")]
    public class TAX
    {
        public string @id { get; set; }
        public string @sum { get; set; }
    }
    [Serializable]
    [XmlRoot("LINETAXES")]
    public class LINETAXES
    {
        public string @count { get; set; }
        public TAX TAX { get; set; }
    }
    [Serializable]

    public class LINE
    {
        [XmlAttribute]
        public string @id { get; set; }
        [XmlAttribute]
        public string @code { get; set; }
        [XmlAttribute]
        public string @name { get; set; }
        [XmlAttribute]
        public string @uni { get; set; }
        [XmlAttribute]
        public string @type { get; set; }
        [XmlAttribute]
        public string @price { get; set; }
        [XmlAttribute]
        public string @categ_id { get; set; }
        [XmlAttribute]
        public string @servprint_id { get; set; }
        [XmlAttribute]
        public string @servprint { get; set; }
        [XmlAttribute]
        public string @quantity { get; set; }
        [XmlAttribute]
        public string @sum { get; set; }
        [XmlAttribute]
        public string @parent { get; set; }

        public LINETAXES LINETAXES { get; set; }
    }
    [Serializable]
    public class CHECKLINES
    {
        [XmlAttribute]
        public string @count { get; set; }

        [XmlElement("LINE")]
        public LINE[] LINE { get; set; }
    }
    [Serializable]
    [XmlRoot("CATEG")]
    public class CATEG
    {
        [XmlAttribute]
        public string @id { get; set; }
        [XmlAttribute]
        public string @code { get; set; }
        [XmlAttribute]
        public string @name { get; set; }
        [XmlAttribute]
        public string @sum { get; set; }
        [XmlAttribute]
        public string @discsum { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECKCATEGS")]
    public class CHECKCATEGS
    {
        [XmlAttribute]
        public string @count { get; set; }
        [XmlElement("CATEG")]
        public CATEG[] CATEG { get; set; }
    }
    [Serializable]
    [XmlRoot("DISCOUNT")]
    public class DISCOUNT
    {
        public string @id { get; set; }
        public string @code { get; set; }
        public string @name { get; set; }
        public string @uni { get; set; }
        public string @interface { get; set; }
        public string @cardcode { get; set; }
        public string @account { get; set; }
        public string @sum { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECKDISCOUNTS")]
    public class CHECKDISCOUNTS
    {
        public string @count { get; set; }
        public DISCOUNT DISCOUNT { get; set; }
    }
    [Serializable]

    public class PAYMENT
    {
        [XmlAttribute]
        public string @id { get; set; }
        [XmlAttribute]
        public string @code { get; set; }
        [XmlAttribute]
        public string @name { get; set; }
        [XmlAttribute]
        public string @uni { get; set; }
        [XmlAttribute]
        public string @paytype { get; set; }
        [XmlAttribute]
        public string @bsum { get; set; }
        [XmlAttribute]
        public string @sum { get; set; }
    }
    [Serializable]
    public class CHECKPAYMENTS
    {
        [XmlAttribute]
        //  [XmlElement("LINE")]
        public string @count { get; set; }
        [XmlElement("PAYMENT")]
        public PAYMENT[] PAYMENT { get; set; }
    }
    [Serializable]
    [XmlRoot("TAX2")]
    public class TAX2
    {
        public string @id { get; set; }
        public string @code { get; set; }
        public string @rate { get; set; }
        public string @sum { get; set; }
        public string @name { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECKTAX")]
    public class CHECKTAX
    {
        public string @count { get; set; }
        public TAX2 TAX { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECKDATA")]

    public class CHECKDATA
    {
        [XmlAttribute]
        public string @checknum { get; set; }
        public string @tablename { get; set; }
        public string @startservice { get; set; }
        public string @closedatetime { get; set; }
        public string @ordernum { get; set; }
        public string @guests { get; set; }
        public string @orderguid { get; set; }
        public string @checkguid { get; set; }
        public string @order_cat { get; set; }
        public string @order_type { get; set; }
        public CHECKPERSONS CHECKPERSONS { get; set; }
        public CHECKLINES CHECKLINES { get; set; }
        public CHECKCATEGS CHECKCATEGS { get; set; }
        public CHECKDISCOUNTS CHECKDISCOUNTS { get; set; }
        public CHECKPAYMENTS CHECKPAYMENTS { get; set; }
        public CHECKTAX[] CHECKTAXES { get; set; }
    }
    [Serializable]
    [XmlRoot("CHECK")]
    public class CHECK
    {
        public string @stationcode { get; set; }
        public string @restaurantcode { get; set; }
        public string @cashservername { get; set; }
        public string @generateddatetime { get; set; }
        [XmlAttribute]
        public string @chmode { get; set; }
        public string @locale { get; set; }
        public string @shiftdate { get; set; }
        public string @shiftnum { get; set; }
        public EXTINFO EXTINFO { get; set; }
        public CHECKDATA CHECKDATA { get; set; }
    }
    [Serializable]
    [XmlRoot("INPBUF")]
    public class INPBUF
    {
        public CHECK CHECK { get; set; }
    }

    [Serializable]
    [XmlRoot("ROOT")]
    public class RkeeperTransactionclientrequest
    {
        public Transactions Transactions { get; set; }
        public INPBUF INPBUF { get; set; }
    }
}
