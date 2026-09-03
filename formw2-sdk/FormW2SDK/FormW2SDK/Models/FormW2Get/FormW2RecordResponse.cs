using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Get
{
    [DataContract]
    public class FormW2RecordResponse : BaseResponseStatus
    {

        /// <summary>
        /// A detailed information about the Business, Employees and Form W-2 records
        /// </summary>
        [DataMember(Order = 2)]
        public FormW2Response FormW2Records { get; set; }
        /// <summary>
        /// It will show the detailed information about the error.
        /// </summary>
        [DataMember(Order = 3)]
        public List<ErrorV3> Errors { get; set; }
    }
}
