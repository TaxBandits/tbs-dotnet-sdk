using Form1099MISCSDK.Models.Form1099MISCCreate;
using System.Runtime.Serialization;

namespace Form1099MISCSDK.Models.Form1099MISCList
{
    [DataContract]
    public class Form1099MiscListResponse
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }

        [DataMember]
        public Guid? BusinessId { get; set; }

        [DataMember]
        public string BusinessNm { get; set; }

        [DataMember]
        public string EINorSSN { get; set; }

        [DataMember]
        public MiscRecipient Recipient { get; set; }
    }
    [DataContract]
    public class MiscRecipient
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? RecipientId { get; set; }
        [DataMember]
        public string RecipientNm { get; set; }
    }
}
