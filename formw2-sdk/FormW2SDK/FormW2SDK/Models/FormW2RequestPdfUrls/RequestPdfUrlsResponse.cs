using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2RequestPdfUrls
{
    [DataContract]
    public class RequestPdfUrlsResponse
    {
        [DataMember]
        public Guid? SubmissionId { get; set; }
        [DataMember]
        public Form1099RequestPdfUrls FormW2Records { get; set; }
        [DataMember]
        public List<ErrorV3> Errors { get; set; }
    }
    [DataContract]
    public class Form1099RequestPdfUrls
    {
        [DataMember]
        public List<SuccessPdfUrlRecords> SuccessRecords { get; set; }
        [DataMember]
        public List<ErrorPdfUrlRecords> ErrorRecords { get; set; }
    }
    [DataContract]
    public class PrintCopyW2Files
    {
        [DataMember]
        public MaskedType Copy1 { get; set; }
        [DataMember]
        public MaskedType Copy2 { get; set; }
        [DataMember]
        public MaskedType CopyB { get; set; }
        [DataMember]
        public MaskedType CopyC { get; set; }
        [DataMember]
        public MaskedType CopyD { get; set; }
    }
    [DataContract]
    public class MaskedType
    {
        [DataMember]
        public string Unmasked { get; set; }
        [DataMember]
        public string Masked { get; set; }
    }

    [DataContract]
    public class SuccessPdfUrlRecords
    {
        [DataMember]
        public Guid RecordId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public PrintCopyW2Files Files { get; set; }
    }
    [DataContract]
    public class ErrorPdfUrlRecords
    {
        [DataMember]
        public Guid? RecordId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Message { get; set; }
        //[DataMember]
        //public PrintCopyW2Files Files { get; set; }
    }

}
