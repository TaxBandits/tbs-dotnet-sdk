using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2Records
    {
        /// <summary>
        /// It will show the detailed information about the success status of Form W-2 Records
        /// </summary>
        [DataMember]
        public List<SuccessW2Records> SuccessRecords { get; set; }

        /// <summary>
        /// It will show the detailed information about the error status of Form W-2 Records
        /// </summary>
        [DataMember]
        public List<ErrorW2Records> ErrorRecords { get; set; }
    }
    [DataContract]
    public class SuccessW2Records
    {
        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public Guid? EmployeeId { get; set; }
        [DataMember]
        public FederalReturn FederalReturn { get; set; }
        [DataMember]
        public List<StateReturns> StateReturns { get; set; }
        [DataMember]
        public PostalResponse Postal { get; set; }
        [DataMember]
        public OnlineAccessResponse OnlineAccess { get; set; }

    }

    [DataContract]
    public class ErrorW2Records
    {
        [DataMember]
        public string SequenceId { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public List<ErrorV3> Errors { get; set; }
    }
    [DataContract]
    public class FederalReturn
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StatusTs { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public List<ErrorV3> Errors { get; set; }
    }

    [DataContract]
    public class StateReturns
    {
        [DataMember]
        public string StateCd { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StatusTs { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public List<ErrorV3> Errors { get; set; }
    }

    [DataContract]
    public class PostalResponse
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StatusTs { get; set; }
        [DataMember]
        public string Info { get; set; }
    }


    [DataContract]
    public class OnlineAccessResponse
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Info { get; set; }
    }
}
