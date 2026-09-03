using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class PennsylvaniaRecon
    {
        [DataMember]
        public ReconFormREV1667 FormREV1667 { get; set; }

    }
    [DataContract]
    public class ReconFormREV1667
    {
        [DataMember]
        public string PAWithHoldingID { get; set; }

        [DataMember]
        public int? NumOfW2eTIDES { get; set; }
        [DataMember]
        public int? NumOfW2Others { get; set; }
        [DataMember]
        public int? NumOf1099ReTIDES { get; set; }
        [DataMember]
        public int? NumOf1099ROthers { get; set; }
        [DataMember]
        public int? NumOf1099NECMISCeTIDES { get; set; }

        [DataMember]
        public int? NumOf1099NECMISCOthers { get; set; }
        [DataMember]
        public decimal TotWagesAndDistribution { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public WagesAndDistribution WagesAndDistribution { get; set; }
        [DataMember]
        public TaxWH TaxWH { get; set; }

    }
    [DataContract]
    public class WagesAndDistribution
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
        public decimal TotWagesAllQtrs { get; set; }
    }
    [DataContract]
    public class TaxWH
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
        public decimal TotTaxWHAllQtrs { get; set; }
    }
}
