using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class ConnecticutRecon
    {
        [DataMember]
        public FormCTW3 FormCTW3 { get; set; }
    }
    [DataContract]
    public class FormCTW3
    {
        [DataMember]
        public string CTWithHoldingID { get; set; }
        [DataMember]
        public bool IsHouseHoldEr { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public decimal TotWages { get; set; }
        [DataMember]
        public int TotNumOfW2Forms { get; set; }
        [DataMember]
        public CTTaxWH CTTaxWH { get; set; }
    }

    [DataContract]
    public class CTTaxWH
    {
        [DataMember]
        public decimal Qtr1 { get; set; }
        [DataMember]
        public decimal Qtr2 { get; set; }
        [DataMember]
        public decimal Qtr3 { get; set; }
        [DataMember]
        public decimal Qtr4 { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
    }

}
