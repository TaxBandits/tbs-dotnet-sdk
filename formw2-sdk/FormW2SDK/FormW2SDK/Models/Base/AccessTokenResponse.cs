using System.Runtime.Serialization;

namespace FormW2SDK.Models.Base
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public List<Error> Errors { get; set; }

        public int StatusCode { get; set; }

        /// <summary>
        /// It will return the name of status code.
        /// </summary>


        public string StatusName { get; set; }

        /// <summary>
        /// It will return the detailed message of status code.
        /// </summary>


        public string StatusMessage { get; set; }
    }
    public class Error
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
    [DataContract]
    public class BaseResponseStatus
    {
        /// <summary>
        /// It will return the status codes like 200, 300, etc., Refer Status Codes.
        /// </summary>
        [DataMember]
        public int StatusCode { get; set; }

        /// <summary>
        /// It will return the name of status code.
        /// </summary>
        [DataMember]

        public string StatusName { get; set; }

        /// <summary>
        /// It will return the detailed message of status code.
        /// </summary>
        [DataMember]

        public string StatusMessage { get; set; }
    }
}
