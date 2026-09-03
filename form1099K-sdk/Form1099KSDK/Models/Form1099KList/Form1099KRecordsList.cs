using Form1099KSDK.Models.Form1099KCreate;
using System.Runtime.Serialization;

namespace Form1099KSDK.Models.Form1099KList
{
    [DataContract]
    public class Form1099KRecordsList
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
        public KRecipientRecord Recipient { get; set; }
    }
    [DataContract]
    public class KRecipientRecord
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? RecipientId { get; set; }
        [DataMember]
        public string RecipientNm { get; set; }   
    }
}
