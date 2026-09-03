using FormW2SDK.Models.Base;
using FormW2SDK.Models.FormW2Create;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Delete
{
    [DataContract]
    public class FormW2ResponseRecords
    {
        [DataMember]
        public List<SuccessResponseW2> SuccessRecords { get; set; }
        [DataMember]
        public List<ErrorResponseW2> ErrorRecords { get; set; }
    }
    [DataContract]
    public class SuccessResponseW2
    {
        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StatusTs { get; set; }
        [DataMember]
        public List<StateReturnResponse> StateReturns { get; set; }
        [DataMember]
        public OnlineAccessResponse OnlineAccess { get; set; }
        [DefaultValue(null)]
        [Newtonsoft.Json.JsonProperty("Postal", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        [DataMember]
        public PostalResponse Postal { get; set; }
    }

    [DataContract]
    public class ErrorResponseW2
    {
        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public List<ErrorV3> Errors { get; set; }
    }
}
