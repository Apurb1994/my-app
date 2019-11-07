using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISG_VIG_Brands.Models.TransactionFailedResponse
{
    [XmlRoot(ElementName = "TRRESPONSE")]
    public class TRRESPONSE
    {
        [XmlAttribute(AttributeName = "error_code")]
        public string Error_code { get; set; }
        [XmlAttribute(AttributeName = "err_text")]
        public string Err_text { get; set; }
    }

    [XmlRoot(ElementName = "OutBuf")]
    public class OutBufResponse
    {
        public OutBufResponse()
        {
            TRRESPONSE = new TRRESPONSE();
        }
        [XmlElement(ElementName = "TRRESPONSE")]
        public TRRESPONSE TRRESPONSE { get; set; }
        [XmlAttribute(AttributeName = "OutKind")]
        public string OutKind { get; set; }
    }

    [XmlRoot(ElementName = "TransactionsEx")]
    public class TransactionsEx
    {
        public TransactionsEx()
        {
            OutBuf = new OutBufResponse();
        }
        [XmlElement(ElementName = "OutBuf")]
        public OutBufResponse OutBuf { get; set; }
        [XmlAttribute(AttributeName = "Result")]
        public string Result { get; set; }
    }

    [XmlRoot(ElementName = "Root")]
    public class TransactionFailedResponse
    {
        public TransactionFailedResponse()
        {
            TransactionsEx = new TransactionsEx();
        }
        [XmlElement(ElementName = "TransactionsEx")]
        public TransactionsEx TransactionsEx { get; set; }
    }
}