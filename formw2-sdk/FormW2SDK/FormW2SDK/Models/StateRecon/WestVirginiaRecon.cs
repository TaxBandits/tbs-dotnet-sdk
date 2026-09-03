using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class WestVirginiaRecon
    {
        [DataMember]
        public ReconFormIT103 FormIT103 { get; set; }
    }

    [DataContract]
    public class ReconFormIT103
    {
        [DataMember]
        public string WVWithHoldingID { get; set; }
        [DataMember]
        public int NumOf1099W2 { get; set; }
        [DataMember]
        public decimal TotalTaxWH1099W2 { get; set; }
        [DataMember]
        public WithholdingTaxStatements WHTaxDue { get; set; }

    }

    [DataContract]
    public class WithholdingTaxStatements
    {
        [DataMember]
        public decimal WVTaxQ1 { get; set; }
        [DataMember]
        public decimal WVTaxQ2 { get; set; }
        [DataMember]
        public decimal WVTaxQ3 { get; set; }
        [DataMember]
        public decimal WVTaxQ4 { get; set; }
        [DataMember]
        public decimal TotalForYear { get; set; }
    }
}
