using BusinessApiSDK.Models.Utilities;
using System.Runtime.Serialization;

namespace BusinessApiSDK.Models.Business
{
    public class BusinessCreateResponse : BaseResponseStatus
    {
        /// <summary>
        /// Business Identifier
        /// </summary>
        [DataMember(Order = 1)]
        public Guid BusinessId { get; set; }

        [DataMember(Order = 2)]
        public string PayerRef { get; set; }

        [DataMember(Order = 3)]
        public bool IsEIN { get; set; }

        [DataMember(Order = 4)]
        public string EINorSSN { get; set; }

        [DataMember(Order = 5)]
        public string BusinessNm { get; set; }

        [DataMember(Order = 6)]
        public List<Error> Errors { get; set; }
    }

    [DataContract]
    public class Error
    {
 
        /// <summary>
        /// It will return the validation error Id
        /// </summary>
        [DataMember]

        public string Id { get; set; }
        /// <summary>
        /// It will return the validation error code
        /// </summary>
        [DataMember]

        public string Code { get; set; }

        /// <summary>
        /// It will return the name of the validation error
        /// </summary>
        [DataMember]

        public string Name { get; set; }

        /// <summary>
        /// It will return the detailed message of the validation error
        /// </summary>
        [DataMember]

        public string Message { get; set; }

        /// <summary>
        /// It will show the type of an error
        /// </summary>
        [DataMember]

        public string Type { get; set; }
    }
}
