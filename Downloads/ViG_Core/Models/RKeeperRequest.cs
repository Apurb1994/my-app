using System;
using System.Xml.Serialization;

namespace ISG_VIG_Brands.Models
{
    [Serializable]
    [XmlRoot("QRY")]
    public class QRY
    {
        [XmlAttribute("Card")]
        public string Card { get; set; }
        [XmlAttribute("Restaurant")]
        public string Restaurant { get; set; }
       [XmlAttribute("UnitNo")]
        public string UnitNo { get; set; }
        //public string @Locked { get; set; }
        //public string @Seize { get; set; }
        //public string @Discount { get; set; }
        //public string @Bonus { get; set; }
        //public string @Summa { get; set; }
        //public string @DiscLimit { get; set; }
        //public string @Holder { get; set; }
        //public string @Sum2 { get; set; }
        //public string @Sum3 { get; set; }
        //public string @Sum4 { get; set; }
        //public string @Sum5 { get; set; }
        //public string @Sum6 { get; set; }
        //public string @Sum7 { get; set; }
        //public string @Sum8 { get; set; }
        //public string @DopInfo { get; set; }
        //public string @WhyLock { get; set; }
        //public string @ScrMessage { get; set; }
        //public string @PrnMessage { get; set; }
        //public string @Result { get; set; }
    }

    [Serializable]
    [XmlRoot("ROOT")]
    public class RKeeperRequest
    {
        public QRY QRY { get; set; }
    }

    //[Serializable]
    ////[XmlRoot("RKeeperRequest")]
    //public class RKeeperRequest
    //{
    //    public RKeeperRequest Root { get; set; }
    //}

}
