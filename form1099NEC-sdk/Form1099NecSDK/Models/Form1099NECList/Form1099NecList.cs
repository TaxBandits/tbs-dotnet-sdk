using Form1099NecSDK.Models.Base;
using System.Runtime.Serialization;

namespace Form1099NecSDK.Models.Form1099NecList
{
    [DataContract]
    public class Form1099NecList
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
        public NecRecipient Recipient { get; set; }
    }

    [DataContract]
    public class NecRecipient
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? RecipientId { get; set; }
        [DataMember]
        public string RecipientNm { get; set; }
    }
}
