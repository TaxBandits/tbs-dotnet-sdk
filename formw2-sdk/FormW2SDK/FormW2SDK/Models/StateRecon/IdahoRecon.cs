using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class IdahoRecon
    {
        [DataMember]
        public ReconForm967 Form967 { get; set; }
    }

    [DataContract]
    public class ReconForm967
    {
        [DataMember]
        public String IDWithHoldingID { get; set; }
        [DataMember]
        public decimal TotWagesW2 { get; set; }
        [DataMember]
        public decimal TotTaxWHW2 { get; set; }
        [DataMember]
        public int NumOfW2Forms { get; set; }
        [DataMember]
        public decimal TotTaxWH1099 { get; set; }
        [DataMember]
        public int NumOf1099Forms { get; set; }
        [DataMember]
        public int TotNumOf1099W2 { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public decimal WHTaxPaid { get; set; }
        [DataMember]
        public decimal BalanceDue { get; set; }
        [DataMember]
        public decimal Overpayment { get; set; }
        [DataMember]
        public decimal PenaltyOnBalanceDue { get; set; }
        [DataMember]
        public decimal InterestOnBalanceDue { get; set; }
        [DataMember]
        public decimal BalanceDuePenaltyInt { get; set; }
        [DataMember]
        public decimal LateFilingPenalty { get; set; }
        [DataMember]
        public decimal TotBalanceDue { get; set; }
        [DataMember]
        public decimal TotOverPayment { get; set; }
        [DataMember]
        public bool Is1099CFSF { get; set; }
        [DataMember]
        public string FilingCycle { get; set; }
    }
}
