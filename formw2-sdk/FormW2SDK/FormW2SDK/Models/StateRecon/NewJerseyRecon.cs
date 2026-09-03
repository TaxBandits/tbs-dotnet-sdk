using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class NewJerseyRecon
    {
        [DataMember]
        public ReconFormNJW3M FormNJW3M { get; set; }
    }
    [DataContract]
    public class ReconFormNJW3M
    {
        [DataMember]
        public string NJWithHoldingID { get; set; }
        [DataMember]
        public int TotNumOfEmp { get; set; }
        [DataMember]
        public decimal TotWgsPensionAnnuityGamWin { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
    }
}
