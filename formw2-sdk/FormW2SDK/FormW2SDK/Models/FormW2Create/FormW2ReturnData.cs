using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2ReturnData
    {
        [DataMember]
        public Guid? RecordId { get; set; }

        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public bool? IsOnlineAccess { get; set; }
        [DataMember]
        public bool? IsPostal { get; set; }

        [DataMember]
        public FormW2Employee Employee { get; set; }

        [DataMember]
        public W2FormDetails W2FormData { get; set; }
    }
}
