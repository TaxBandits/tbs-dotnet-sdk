using FormW2SDK.Models.Base;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.Business
{
    public class BusinessListResponse : BaseResponseStatus
    {
        /// <summary>
        /// Business Details of all the business
        /// </summary>
        [DataMember(Order = 1)]
        public List<Business> Businesses { get; set; }
        public List<Error> Errors { get; set; }
    }
}
