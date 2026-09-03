using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2RequestDraftPdfUrl
{
    [DataContract]
    public class FromW2RequestDraftPdfUrl
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public string TINMaskType { get; set; }
        [DataMember]
        public string TaxYear { get; set; } 
    }
}
