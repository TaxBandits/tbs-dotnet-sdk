using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    public class ArizonaRecon
    {
        [DataMember]
        public ArizonaReconForm FormA1R { get; set; }
    }
    [DataContract]
    public class ArizonaReconForm
    {
        [DataMember]
        public string AZWithHoldingID { get; set; }
        [DataMember]
        public int TotNumOfEmp { get; set; }
        [DataMember]
        public int TotNumOfForms { get; set; }
        [DataMember]
        public decimal TotWages { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public decimal Penalty { get; set; }
        [DataMember]
        public bool IsEarlyReturn { get; set; }
        [DataMember]
        public bool IsCancelDueToMrge { get; set; }
        [DataMember]
        public bool IsAmtDiff { get; set; }
        [DataMember]
        public AmountReportedFormA1R AmtReportedOnA1QRT { get; set; }
        [DataMember]
        public PredecessorDetails PrevErDetails { get; set; }

    }
    [DataContract]
    public class AmountReportedFormA1R
    {
        [DataMember]
        public decimal Qtr1st { get; set; }
        [DataMember]
        public decimal Qtr2nd { get; set; }
        [DataMember]
        public decimal Qtr3rd { get; set; }
        [DataMember]
        public decimal Qtr4th { get; set; }
        [DataMember]
        public decimal TotReported { get; set; }
    }
    [DataContract]
    public class PredecessorDetails
    {
        [DataMember]
        public string PrevErName { get; set; }
        [DataMember]
        public string PrevErEIN { get; set; }
    }
}
