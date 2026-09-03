using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Delete
{
    [DataContract]

    public class FormW2DeleteResponse : BaseResponseStatus
    {
        [DataMember(Order = 1)]
        public Guid SubmissionId { get; set; }
        /// <summary>
        /// List of all Record Status
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
