using Amazon.S3.Model;
using Form941SDK.Models.Base;
using System.Runtime.Serialization;

namespace Form941SDK.Models.Form941List
{
    [DataContract]
    public class Form941ListResponse
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }

        [DataMember]
        public Guid? BusinessId { get; set; }

        [DataMember]
        public string BusinessNm { get; set; }

        [DataMember]
        public string EIN { get; set; }

        [DataMember]
        public string BusinessType { get; set; }
        [DataMember]
        public Guid? RecordId { get; set; }

        [DataMember]
        public string TaxYr { get; set; }

        [DataMember]
        public string Qtr { get; set; }

        [DataMember]
        public string IRSPaymentType { get; set; }
        [DataMember]
        public string ReturnType { get; set; }
        [DataMember]
        public string EFileStatus { get; set; }

    }
}
