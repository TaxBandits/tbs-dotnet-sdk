using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class LouisianaRecon
    {
        [DataMember]
        public ReconFormL3 FormL3 { get; set; }

    }

    [DataContract]
    public class ReconFormL3
    {
        [DataMember]
        public String LAWithHoldingID { get; set; }
        [DataMember]
        public int NumOfW2Forms { get; set; }
        [DataMember]
        public int NumOf1099W2GForms { get; set; }
        [DataMember]
        public int TotNumOf1099W2W2GForms { get; set; }
        [DataMember]
        public TotWagesInformationReturn TotWagesInformationReturn { get; set; }
        [DataMember]
        public TotTaxWHInformationReturn TotTaxWHInformationReturn { get; set; }
        [DataMember]
        public TotTaxWHL1 TotTaxWHL1 { get; set; }


    }
    [DataContract]
    public class TotWagesInformationReturn
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
        public decimal TotWages { get; set; }

    }
    [DataContract]
    public class TotTaxWHInformationReturn
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
        public decimal TotWH { get; set; }

    }
    [DataContract]
    public class TotTaxWHL1
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
        public decimal TotWHL1 { get; set; }

    }
}
