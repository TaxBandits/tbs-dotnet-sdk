using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Transmit
{
    [DataContract]
    public class FormW2TransmitRequest
    {
        /// <summary>
        /// Submission ID
        /// </summary>
        [DataMember]
        public Guid? SubmissionId { get; set; }

        /// <summary>
        /// List of all record IDs
        /// </summary>
        [DataMember]
        public List<Guid> RecordIds { get; set; }
    }
}
