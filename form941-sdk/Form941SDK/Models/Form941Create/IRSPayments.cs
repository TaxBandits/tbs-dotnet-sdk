using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using static Form941SDK.Models.Base.EntityBase;

namespace Form941SDK.Models.Form941Create
{
    [DataContract]
    public class IRSPayments
    {

        /// <summary>
        /// EFW payment - Bank Routing Number
        /// </summary>
        [DataMember]
        public string BankRoutingNum { get; set; }


        /// <summary>
        /// EFW payment - Account Type (Checking or Saving)
        /// </summary>
        [DataMember]
        public string AccountType { get; set; }

        /// <summary>
        /// EFW payment - Bank Account Number
        /// </summary>
        [DataMember]
        public string BankAccountNum { get; set; }


        /// <summary>
        /// EFW payment - Phone
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
    }

    public class IRSPaymentsFromTY2022
    {

        /// <summary>
        /// EFW payment - Bank Routing Number
        /// </summary>
        [DataMember]
        public string BankRoutingNum { get; set; }


        /// <summary>
        /// EFW payment - Account Type (Checking or Saving)
        /// </summary>
        [DataMember]
        public string AccountType { get; set; }

        /// <summary>
        /// EFW payment - Bank Account Number
        /// </summary>
        [DataMember]
        public string BankAccountNum { get; set; }


        /// <summary>
        /// EFW payment - Phone
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
    }
}
