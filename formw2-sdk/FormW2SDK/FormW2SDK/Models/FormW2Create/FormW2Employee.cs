using FormW2SDK.Models.Business;
using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class FormW2Employee
    {
        [DataMember]
        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// Social Security Number (SSN).
        /// </summary>
        [DataMember]
        public string SSN { get; set; }

        /// <summary>
        /// Employee's First Name
        /// </summary>
        [DataMember]
        public string FirstNm { get; set; }

        /// <summary>
        /// Employee's Middle Name
        /// </summary>
        [DataMember]
        public string MiddleNm { get; set; }

        /// <summary>
        /// Employee's Last Name
        /// </summary>
        [DataMember]
        public string LastNm { get; set; }

        /// <summary>
        /// Employee's Suffix. This is optional
        /// </summary>
        [DataMember]
        public string Suffix { get; set; }

        /// <summary>
        /// Employee's email address.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Employee's Fax number, if applicable.
        /// </summary>
        [DataMember]
        public string Fax { get; set; }

        /// <summary>
        /// Employee's complete phone number, including the area code.
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public bool? IsForeign { get; set; }
        [DataMember]
        public bool IsForeignNullable { get { return IsForeign ?? false; } set { IsForeign = value; } }
        /// <summary>
        /// Employee's US Address.
        /// </summary>
        [DataMember]
        public USAddress USAddress { get; set; }

        [DataMember]
        public ForeignAddress ForeignAddress { get; set; }
    }
}
