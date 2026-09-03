using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class Maryland
    {
        [DataMember]
        public ReconFormMW508 FormMW508 { get; set; }
    }
    [DataContract]
    public class ReconFormMW508
    {
        [DataMember]
        public string MDWithHoldingID { get; set; }
        [DataMember]
        public string NAICSCd { get; set; }
        [DataMember]
        public long NumOfW2Forms { get; set; }
        [DataMember]
        public long NumOf1099Forms { get; set; }
        [DataMember]
        public long NumOfW21099Forms { get; set; }
        [DataMember]
        public decimal TotWagesW2 { get; set; }
        [DataMember]
        public decimal TotTaxWHW2 { get; set; }
        [DataMember]
        public decimal TotTaxWH1099 { get; set; }
        [DataMember]
        public decimal TotTaxWH1099W2 { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public decimal TaxReportedMW506 { get; set; }
        [DataMember]
        public decimal TaxExemptCreditMW508CR { get; set; }
        [DataMember]
        public decimal TotWHTaxPaid { get; set; }
        [DataMember]
        public decimal TotTaxDue { get; set; }
        [DataMember]
        public decimal OverpaymentAmt { get; set; }
        [DataMember]
        public decimal OverpaymentRefundAmt { get; set; }
        [DataMember]
        public decimal OverpaymentCreditAmt { get; set; }
        [DataMember]
        public decimal TotStatePickUpAmount { get; set; }
        [DataMember]
        public bool IsCompleteFiling { get; set; }
        [DataMember]
        public bool IsAdditional1099 { get; set; }
        [DataMember]
        public bool IsAdditionalW2 { get; set; }
        [DataMember]
        public EmployerRepre EmployerRep { get; set; }

    }
    [DataContract]
    public class EmployerRepre
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string PhNo { get; set; }
    }
}
