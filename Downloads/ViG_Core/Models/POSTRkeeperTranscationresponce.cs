using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace ISG_VIG_Brands.Models.Rkeeper
{
    [Serializable]
    public class TransactionsEx
    {
        [XmlAttribute]
        public string @Result { get; set; }
    }
    [Serializable]
    public class TRRESPONSE
    {
        [XmlAttribute]
        public string error_code { get; set; }
        [XmlAttribute]
        public string err_text { get; set; }
    }

  
    [Serializable]
    //[XmlRoot("OutBuf")]
    public class OutBuf
    {
        [XmlAttribute]
        public string @OutKind { get; set; }
        [XmlElement("TRRESPONSE")]
        public TRRESPONSE TRRESPONSE { get; set; } = new TRRESPONSE();
    }




    [Serializable]
    [XmlRoot("Root")]
    public class POSTRkeeperTranscationresponce
    {
        [XmlElement("TransactionsEx")]
        public TransactionsEx TransactionsEx { get; set; } = new TransactionsEx();
        public OutBuf OutBuf { get; set; } = new OutBuf();
    }

    [XmlRoot(ElementName = "TransactionsEx")]
    public class TransactionsExResponse
    {
        [XmlAttribute(AttributeName = "Result")]
        public string Result { get; set; }
    }

    [XmlRoot(ElementName = "Root")]
    public class TransactionResponseRoot
    {
        public TransactionResponseRoot()
        {
            TransactionsEx = new TransactionsEx();
        }
        [XmlElement(ElementName = "TransactionsEx")]
        public TransactionsEx TransactionsEx { get; set; }
    }

















    //[Serializable]
    //public class Root
    //{
    //    [XmlElement]
    //    public TransactionsEx TransactionsEx { get; set; } = new TransactionsEx();
    //    [XmlElement]
    //    public OutBuf OutBuf { get; set; } = new OutBuf();
    //}

    //[Serializable]
    //[XmlRoot("Root")]
    //public class POSTRkeeperTranscationresponce
    //{
    //    public Root Root { get; set; } = new Root();
    //}

}
