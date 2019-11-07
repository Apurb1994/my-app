using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISG_VIG_Brands.Models
{
    [Serializable]
    [XmlRoot("ident")]
    public class Ident
    {
        [XmlAttribute("code")]
        public string @code { get; set; }
        [XmlAttribute("printname")]
        public string @printname { get; set; }
    }

    [Serializable]
    //[XmlRoot("group1")]
    public class Group
    {
        [XmlAttribute("printname")]
        public string @printname { get; set; }
        [XmlAttribute("order")]
        public string @order { get; set; }
        [XmlIgnore]
        public IList<Ident> idents { get; set; } = new List<Ident>();
        [XmlElement("ident")]
        public Ident[] ident { get { return idents.ToArray(); } set { } }
    }

    [Serializable]
    //[XmlRoot("ident_list")]
    public class IdentList
    {
        [XmlAttribute("hint")]
        public string @hint { get; set; }
        [XmlIgnore]
        public IList<Group> groups { get; set; } = new List<Group>() { new Group() };
        [XmlElement("group")]
        public Group[] group
        {
            get
            {
                return groups.ToArray();
            }
            set { }
        }
    }

    [Serializable]
    //[XmlRoot("OutBuf")]
    public class OutBuf
    {
        [XmlAttribute("OutKind")]
        public string @OutKind { get; set; }
        [XmlElement("ident_list")]
        public IdentList ident_list { get; set; } = new IdentList();
    }

    [Serializable]
    //[XmlElement("GetCardInfoEx")]
    public class GetCardInfoEx
    {
        [XmlAttribute("CardCode")]
        public string @CardCode { get; set; }
        [XmlAttribute("Account")]
        public string @Account { get; set; }
        [XmlAttribute("Deleted")]
        public string @Deleted { get; set; }
        [XmlAttribute("Locked")]
        public string @Locked { get; set; }
        [XmlAttribute("Seize")]
        public string @Seize { get; set; }
        [XmlAttribute("Discount")]
        public string @Discount { get; set; }
        [XmlAttribute("Bonus")]
        public string @Bonus { get; set; }
        [XmlAttribute]
        public string @Summa { get; set; }
        [XmlAttribute]
        public string @DiscLimit { get; set; }
        [XmlAttribute]
        public string @Holder { get; set; }
        [XmlAttribute]
        public string @Sum2 { get; set; }
        [XmlAttribute]
        public string @Sum3 { get; set; }
        [XmlAttribute]
        public string @Sum4 { get; set; }
        [XmlAttribute]
        public string @Sum5 { get; set; }
        [XmlAttribute]
        public string @Sum6 { get; set; }
        [XmlAttribute]
        public string @Sum7 { get; set; }
        [XmlAttribute]
        public string @Sum8 { get; set; }
        [XmlAttribute]
        public string @DopInfo { get; set; }
        [XmlAttribute]
        public string @WhyLock { get; set; }
        [XmlAttribute]
        public string @ScrMessage { get; set; }
        [XmlAttribute]
        public string @PrnMessage { get; set; }
        [XmlAttribute("Result")]
        public string @Result { get; set; }
        [XmlElement("OutBuf")]
        public OutBuf OutBuf { get; set; } = new OutBuf();
    }


    [Serializable]
    [XmlRoot("Root")]
    public class POSTRKeeperCustomerResponse
    {
        [XmlElement("GetCardInfoEx")]
        public GetCardInfoEx GetCardInfoEx { get; set; } = new GetCardInfoEx();
    }
}
