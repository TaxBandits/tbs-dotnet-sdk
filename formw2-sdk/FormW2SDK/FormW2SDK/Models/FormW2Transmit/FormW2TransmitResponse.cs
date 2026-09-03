using FormW2SDK.Models.Base;
using FormW2SDK.Models.FormW2Delete;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Transmit
{
    [DataContract]
    public class FormW2TransmitResponse:BaseResponseStatus
    {
        /// <summary>
        /// Submission ID
        /// </summary>
        [DataMember(Order = 1)]
        public Guid SubmissionId { get; set; }

        /// <summary>
        /// Form W-2 records success and error status
        /// </summary>
        [DataMember(Order = 2)]
        public FormW2ResponseRecords FormW2Records { get; set; }

        /// <summary>
        /// It will show the detailed information about the error.
        /// </summary>
        [DataMember(Order = 3)]
        public List<ErrorV3> Errors { get; set; }
    }
}
