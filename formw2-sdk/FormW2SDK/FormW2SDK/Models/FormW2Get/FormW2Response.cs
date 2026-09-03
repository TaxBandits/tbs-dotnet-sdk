using FormW2SDK.Models.Business;
using FormW2SDK.Models.FormW2Create;
using FormW2SDK.Models.StateRecon;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Get
{
    [DataContract]
    public class FormW2Response
    {
        [DataMember]
        public SubmissionLevelManifestV2 SubmissionManifest { get; set; }
        [DataMember]
        public APIReturnHeader ReturnHeader { get; set; }
        [DataMember]
        public List<FormW2ReturnData> ReturnData { get; set; }
        [DataMember]
        public W2StateRecon StateReconData { get; set; }
    }
}
