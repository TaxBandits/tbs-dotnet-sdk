using Form1099NecSDK.Models.Business;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Form1099NecSDK.Models.Form1099NEC
{
    /// <summary>
    ///  Recipient Details Model
    /// </summary>
    [DataContract]
    public class Recipient
    {
        [DataMember]
        public Guid? RecipientId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        [DataMember]
        public string TINType { get; set; }
        /// <summary>
        /// Employer Identification Number (EIN) or Social Security Number (SSN).
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-SSN-01:SSN is required")]
        //[RegularExpression(@"^\d{9}$", ErrorMessage = "ERR-EIN-02:Enter Valid SSN")]
        //[MaxLength(9, ErrorMessage = "ERR-SSN-03:Maximum 9 digits only allowed")]

        public string TIN { get; set; }

        /// <summary>
        /// Recipient's First Name you want to create return.
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-FNAME-01:Recipient's First Name is required")]
        //[RegularExpression(@"[0-9a-zA-Z\s-\(\)\&\,\.'s]+$", ErrorMessage = "ERR-TRADENME-01:Enter valid First Name")]

        public string FirstPayeeNm { get; set; }

        /// <summary>
        /// Recipient's Last Name you want to create return.
        /// </summary>
        [DataMember]
        //[RegularExpression(@"[0-9a-zA-Z\s-\(\)\&\,\.'s]+$", ErrorMessage = "ERR-TRADENME-01:Enter valid Last Name")]

        public string SecondPayeeNm { get; set; }

        /// <summary>
        /// True for other than US address.
        /// </summary>
        [DataMember]
        public bool? IsForeign { get; set; }
        [JsonIgnore]
        public bool IsForeignNullable { get { return IsForeign ?? false; } set { IsForeign = value; } }
        /// <summary>
        /// </summary>
        [DataMember]
        public USAddress USAddress { get; set; }
        /// <summary>
        /// </summary>
        [DataMember]
        public ForeignAddress ForeignAddress { get; set; }
        /// <summary>
        /// Recipient's email address.
        /// </summary>
        [DataMember]
        //[RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "ERR-EMAIL-02:Enter Valid Email")]
        //[MaxLength(40, ErrorMessage = "ERR-EMAIL-03:Maximum 40 characters only allowed")]

        public string Email { get; set; }

        /// <summary>
        /// Recipient's Fax number, if applicable.
        /// </summary>
        //[DataMember]
        //[MaxLength(10, ErrorMessage = "ERR-FAX-01:Maximum 10 characters only allowed")]
        //[MinLength(10, ErrorMessage = "ERR-FAX-02:Fax Number can be only 10 digits")]
        //[RegularExpression("([0-9]+$)", ErrorMessage = "ERR-FAX-03:Only numbers are allowed")]
        [DataMember]

        public string Fax { get; set; }

        /// <summary>
        /// Recipient's complete phone number, including the area code.
        /// </summary>
        [DataMember]
        //[MaxLength(10, ErrorMessage = "ERR-PHONE-02:Phone Number can be only 10 digits")]
        //[MinLength(10, ErrorMessage = "ERR-PHONE-03:Phone Number can be only 10 digits")]

        //[RegularExpression("([0-9]+$)", ErrorMessage = "ERR-PHONE-04:Only numbers are allowed")]
        public string Phone { get; set; }
    }
}
