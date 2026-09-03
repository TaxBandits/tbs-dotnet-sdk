using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class IndianaRecon
    {
        [DataMember]
        public FormWH3 FormWH3 { get; set; }

    }

    [DataContract]
    public class FormWH3
    {
        [DataMember]
        public string INWithHoldingID { get; set; }
        [DataMember]
        public int NumOfW2WH18W2G1099Forms { get; set; }
        [DataMember]
        public decimal StateTaxWHW2WH18W2G1099 { get; set; }
        [DataMember]
        public decimal CountyTaxWHW2WH18W2G1099 { get; set; }
        [DataMember]
        public decimal TotTaxWH { get; set; }
        [DataMember]
        public decimal TotDeposits { get; set; }
        [DataMember]
        public decimal BalDue { get; set; }
        [DataMember]
        public decimal Overpayment { get; set; }
        [DataMember]
        public decimal TotCountyTaxWH { get; set; }
        [DataMember]
        public List<CountyTaxWH> CountyTaxWH { get; set; }

    }
    [DataContract]
    public class CountyTaxWH
    {
        [DataMember]
        public string CountyNm { get; set; }
        [DataMember]
        public decimal TaxWH { get; set; }

    }
}
