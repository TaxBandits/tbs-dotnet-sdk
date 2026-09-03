using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class AlabamaRecon
    {
        [DataMember]
        public ReconFormA3 FormA3 { get; set; }
    }
    [DataContract]
    public class ReconFormA3
    {
        [DataMember]
        public string ALWithHoldingID { get; set; }
        //[DataMember]
        //public int NumOfW2 { get; set; }
        [DataMember]
        public int NumOf1099W2 { get; set; }
        [DataMember]
        public List<IncomTaxWithheldandRemitt> IncomeTaxWHAndRemitt { get; set; }
        [DataMember]
        public AlabamaPaymentDetails PaymentDetails { get; set; }
        [DataMember]
        public AlabamaEFTDebitInfo EFTDebitInfo { get; set; }
        [DataMember]
        public FundingSource FundingSource { get; set; }

    }
    [DataContract]
    public class IncomTaxWithheldandRemitt
    {
        [DataMember]
        public string Month { get; set; }
        [DataMember]
        public decimal TaxWH { get; set; }
        [DataMember]
        public decimal TaxRemitt { get; set; }
    }

    [DataContract]
    public class AlabamaPaymentDetails
    {
        [DataMember]
        public decimal TotTaxRemitt { get; set; }
        [DataMember]
        public decimal TotTaxWH1099W2 { get; set; }
        [DataMember]
        public decimal TotTaxDue { get; set; }
        [DataMember]
        public decimal TotOverpayment { get; set; }
        [DataMember]
        public string OverPaymentType { get; set; }
        [DataMember]
        public string PaymentMethod { get; set; }
        [DataMember]
        public Boolean? IsInternationalACHTxn { get; set; }
        [JsonIgnore]
        public bool IsInternationalACHTxnNullable { get { return IsInternationalACHTxn ?? false; } set { IsInternationalACHTxn = value; } }
    }

    [DataContract]
    public class AlabamaEFTDebitInfo
    {
        [DataMember]
        public string BankAccType { get; set; }
        [DataMember]
        public string BankAccNum { get; set; }
        [DataMember]
        public string BankRoutingNum { get; set; }
        [DataMember]
        public string PaymentDate { get; set; }
    }
    [DataContract]
    public class FundingSource
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string ZipExtn { get; set; }
    }
}
