using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2CreateResponse : BaseResponseStatus
    {
        [DataMember(Order = 1)]
        public Guid SubmissionId { get; set; }

        [DataMember(Order = 2)]
        public Guid? BusinessId { get; set; }
        [DataMember(Order = 3)]
        public string PayerRef { get; set; }
        /// <summary>
        /// Form W-2 records success and error status
        /// </summary>
        [DataMember(Order = 4)]
        public FormW2Records FormW2Records { get; set; }
        /// <summary>
        /// It will show the detailed information about the error.
        /// </summary>
        [DataMember(Order = 5)]
        public List<ErrorV3> Errors { get; set; }
    }
}
