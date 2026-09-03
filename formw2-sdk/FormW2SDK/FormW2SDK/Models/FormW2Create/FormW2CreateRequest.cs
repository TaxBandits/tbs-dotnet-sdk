using FormW2SDK.Models.Business;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using FormW2SDK.Models.StateRecon;

namespace FormW2SDK.Models.FormW2Create
{
    public class FormW2CreateRequest
    {
        [DataMember]
        public SubmissionLevelManifestV2 SubmissionManiFest { get; set; }
        [DataMember]
        public APIReturnHeader ReturnHeader { get; set; }
        [DataMember]
        public List<FormW2ReturnData> ReturnData { get; set; }
        [DataMember]
        public W2StateRecon StateReconData { get; set; }
    }
    [DataContract]
    public class SubmissionLevelManifestV2
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }
        [DataMember]
        public string TaxYear { get; set; }
        [DataMember]
        public bool IsFederalFilingNullable { get { return IsFederalFiling ?? false; } set { IsFederalFiling = value; } }
        [DataMember]
        public bool IsStateFilingNullable { get { return IsStateFiling ?? false; } set { IsStateFiling = value; } }
        [DataMember]
        public bool IsPostalNullable { get { return IsPostal ?? false; } set { IsPostal = value; } }
        [DataMember]
        public bool IsOnlineAccessNullable { get { return IsOnlineAccess ?? false; } set { IsOnlineAccess = value; } }
        [DataMember]
        public bool? IsFederalFiling { get; set; }
        [DataMember]
        public bool? IsStateFiling { get; set; }
        [DataMember]
        public bool? IsPostal { get; set; }
        [DataMember]
        public bool? IsOnlineAccess { get; set; }
    }
}
