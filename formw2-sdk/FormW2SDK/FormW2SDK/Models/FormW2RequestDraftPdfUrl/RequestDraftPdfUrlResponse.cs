using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2RequestDraftPdfUrl
{
    [DataContract]
    public class RequestDraftPdfUrlResponse
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? EmployeeId { get; set; }
        [DataMember]
        public string DraftPdfUrl { get; set; }
        [DataMember]
        public ErrorV3 Error { get; set; }
    }

}
