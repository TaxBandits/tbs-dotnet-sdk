using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2RequestPdfUrls
{
    [DataContract]
    public class RequestPdfURLRequest
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }
        [DataMember]
        public List<RequestRecordIds> RecordIds { get; set; }
        [DataMember]
        public CustomizatedType Customization { get; set; }
       

    }
    [DataContract]
    public class RequestRecordIds
    {
        [DataMember]
        public Guid? RecordId { get; set; }
    }
    [DataContract]
    public class CustomizatedType
    {
        [DataMember]
        public string TINMaskType { get; set; }
    }
}
