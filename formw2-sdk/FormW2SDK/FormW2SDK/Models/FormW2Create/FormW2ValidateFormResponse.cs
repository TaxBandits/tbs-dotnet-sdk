using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2ValidateFormResponse : BaseResponseStatus
    {
        [DataMember(Order = 1)]
        public List<ErrorW2Records> ErrorRecords { get; set; }

        [DataMember(Order = 2)]
        public List<ErrorV3> Errors { get; set; }
    }
}
