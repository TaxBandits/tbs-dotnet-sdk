using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Delete
{
    [DataContract]
    public class StateReturnResponse
    {
        /// <summary>
        /// State Code
        /// </summary>
        public string StateCode { get; set; }
        /// <summary>
        /// Record Status
        /// </summary>
        [DataMember]
        public string RecordStatus { get; set; }
    }
}
