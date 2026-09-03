using System.Runtime.Serialization;

namespace FormW2SDK.Models.Business
{
    [DataContract]
    public class APIReturnHeader
    {
        [DataMember]
        public Business Business { get; set; }
    }
}
