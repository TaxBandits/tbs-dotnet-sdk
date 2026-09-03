using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class VermontRecon
    {
        [DataMember]
        public ReconFormWHT434 FormWHT434 { get; set; }

    }

    [DataContract]
    public class ReconFormWHT434
    {
        public long VermontWH434ID { get; set; }
        [DataMember]
        public string VTWithHoldingID { get; set; }
        [DataMember]
        public string PayFreq { get; set; }
        [DataMember]
        public bool IsBusinessCeasedWantToCloseAcc { get; set; }
        [DataMember]
        public string BusinessCeaseDate { get; set; }
        [DataMember]
        public bool IsReportingSickPay { get; set; }
        [DataMember]
        public decimal AggErHealthInsCoverage { get; set; }
        [DataMember]
        public int TotNumOfW2Forms { get; set; }
        [DataMember]
        public decimal TotWagesPerW2 { get; set; }
        [DataMember]
        public decimal TaxWHPerW2 { get; set; }
        [DataMember]
        public int TotNumOf1099Forms { get; set; }
        [DataMember]
        public decimal Tot1099Payments { get; set; }
        [DataMember]
        public decimal TaxWHPer1099 { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
    }
}
