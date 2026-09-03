using FormW2SDK.Models.FormW2Create;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2List
{
    [DataContract]
    public class FormW2ListResponse
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }

        [DataMember]
        public Guid? BusinessId { get; set; }

        [DataMember]
        public string PayerRef { get; set; }

        [DataMember]
        public string BusinessNm { get; set; }

        [DataMember]
        public string EIN { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string TaxYear { get; set; }

        //[DataMember]
        //public string EFileStatus { get; set; }

        [DataMember]
        public EmployeeFormW2 Employee { get; set; }
    }
    [DataContract]
    public class EmployeeFormW2
    {
        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? EmployeeId { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string SSN { get; set; }

        [DataMember]
        public FederalReturn FederalReturn { get; set; }
        [DataMember]
        public List<StateReturns> StateReturns { get; set; }
        [DataMember]
        public PostalResponse Postal { get; set; }
        [DataMember]
        public OnlineAccessResponse OnlineAccess { get; set; }
    }
}
