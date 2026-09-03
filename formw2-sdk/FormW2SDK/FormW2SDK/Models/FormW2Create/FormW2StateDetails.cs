using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2StateDetails
    {
        [DataMember]
        public string B15StateCd { get; set; }
        [DataMember]
        public decimal B16StateWages { get; set; }
        [DataMember]
        public string B15StateIdNum { get; set; }
        [DataMember]
        public decimal B17StateTax { get; set; }
        [DataMember]
        public List<FormW2LocalityDetails> LocalityData { get; set; }
    }
    [DataContract]
    public class FormW2LocalityDetails
    {
        [DataMember]
        public decimal LocalWages { get; set; }
        [DataMember]
        public decimal LocalTax { get; set; }
        [DataMember]
        public string LocalityNm { get; set; }
        [DataMember]
        public string TaxTypeCd { get; set; }
        [DataMember]
        public string County { get; set; }
        [DataMember]
        public string SchoolDistrictNum { get; set; }
    }
}
