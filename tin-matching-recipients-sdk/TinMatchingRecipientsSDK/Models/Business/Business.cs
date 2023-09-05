﻿using System.Runtime.Serialization;

namespace TinMatchingRecipientsSDK.Models.Business
{
    [DataContract]
    public class Business
    {
        /// <summary>
        /// Business Identifier (Autogenerated).
        /// </summary>
        ///
        [DataMember]
        public Guid? BusinessId { get; set; }
        [DataMember]
        public string PayerRef { get; set; }

        /// <summary>
        /// Name of the Business you want to create a return.
        /// </summary>
        ///
        [DataMember]
        //[Required(ErrorMessage = "ERR-BNAME1-01:Business Name is required")]
        //[MaxLength(75, ErrorMessage = "ERR-BNAME1-02:Business Name can only have a maximum of 75 characters ")]
        //[RegularExpression(@"[0-9a-zA-Z\s-\(\)\&\,\.'s]+$", ErrorMessage = "ERR-BNAME1-03:Enter valid Business name")]

        public string BusinessNm { get; set; }

        /// <summary>
        /// Trade Name (DBA) of the business. This is optional.
        /// </summary>
        [DataMember]
        //[MaxLength(75, ErrorMessage = "ERR-TRADENME-02:Trade Name can only have a maximum of 75 characters")]
        //[RegularExpression(@"[0-9a-zA-Z\s-\(\)\&\,\.'s]+$", ErrorMessage = "ERR-TRADENME-01:Enter valid Trade Name")]

        public string TradeNm { get; set; }

        [DataMember]
        public bool IsEIN { get; set; }

        /// <summary>
        /// Employer Identification Number or Social Security Number
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-EIN-01:EIN/SSN is required")]
        //[RegularExpression(@"^\d{9}$", ErrorMessage = "ERR-EIN-02:Enter Valid EINorSSN")]
        //[MaxLength(9, ErrorMessage = "ERR-EIN-03:EINorSSN length must be 9 digits")]

        public string EINorSSN { get; set; }

        /// <summary>
        /// Employer's email address.
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-EMAIL-01:Email address is required")]
        //[RegularExpression("^(?:[a-zA-Z0-9!#$%&'*+\\=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+\\=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)*$", ErrorMessage = "ERR-EMAIL-02:Enter Valid Email")]
        ////[RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "ERR-EMAIL-02:Enter Valid Email")]
        //[MaxLength(40, ErrorMessage = "ERR-EMAIL-03:Email Address field can only have a maximum of 40 characters")]

        public string Email { get; set; }

        /// <summary>
        /// Name of a person who could be contacted by the IRS if needed.
        /// </summary>
        [DataMember]
        // [Required(ErrorMessage = "ERR-CNAME-01:Business Contact Name is required")]
        //[MaxLength(27, ErrorMessage = "ERR-CNAME-02:Business Contact Name can only have a maximum of 27 characters ")]
        //[RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*", ErrorMessage = "ERR-CNAME-03:Enter a valid Business Contact Name")]

        public string ContactNm { get; set; }


        /// <summary>
        /// Employer's phone number, including the area code.
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-PHONE-01:Business Phone Number is required")]
        //[MaxLength(10, ErrorMessage = "ERR-PHONE-02:Business Phone Number should be 10 digits only")]
        //[MinLength(10, ErrorMessage = "ERR-PHONE-03:Enter a 10 digit Business phone Number")]
        //[RegularExpression("([0-9]+$)", ErrorMessage = "ERR-PHONE-04:Enter a valid Business Phone Number ")]

        public string Phone { get; set; }

        /// <summary>
        /// Phone extension number. Optional.
        /// </summary>
        [DataMember]
        //[MaxLength(5, ErrorMessage = "ERR-PHNETX-01:Business Phone Number Extension can ony be a maximum of 5 digits")]
        //[RegularExpression("([0-9]+$)", ErrorMessage = "ERR-PHNETX-02:Only numbers are allowed for Business Phone Number Extension")]

        public string PhoneExtn { get; set; }

        /// <summary>
        /// Employer's Fax number. Optional.
        /// </summary>
        [DataMember]
        //[MaxLength(10, ErrorMessage = "ERR-FAX-01:Business Fax number can only be a maximum of 10 digits")]
        //[MinLength(10, ErrorMessage = "ERR-FAX-02:Enter a 10 digit Business Fax Number")]
        //[RegularExpression("([0-9]+$)", ErrorMessage = "ERR-FAX-03:Only numbers are allowed for Business Fax Number")]

        public string Fax { get; set; }

        /// <summary>
        /// Type of business entity. The value must be ESTE, PART, CORP, EORG, SPRO. Optional for W2/1099 and required for Form 94x.
        /// </summary>
        [DataMember]
        //[Required(ErrorMessage = "ERR-BTYPE-01:Business Type is required")]
        //[MaxLength(4, ErrorMessage = "ERR-BTYPE-02:Business Type should be of 4 characters")]
        //[MinLength(4, ErrorMessage = "ERR-BTYPE-03:Business Type should be of 4 characters")]
        //[RegularExpression(@"[A-Za-z]+$", ErrorMessage = "ERR-BTYPE-04:Enter a valid Business Trade Name")]
        public string BusinessType { get; set; }

        /// <summary>
        /// Authorized person information to sign the return. Optional for W2/1099 and required for Form 94x.
        /// </summary>
        [DataMember]
        public SigningAuthority SigningAuthority { get; set; }

        /// <summary>
        /// Employer type. The value must be FEDERALGOVT, STATEORLOCAL501C, NONGOVT501C, STATEORLOCALNON501C, NONEAPPLY. Required only for Form W-2 filing
        /// </summary>
        [DataMember]

        public string KindOfEmployer { get; set; }

        /// <summary>
        /// Kind Of Payer based on the Employer's Federal Tax Return. The value must be REGULAR941, REGULAR944, AGRICULTURAL943, HOUSEHOLD, MILITARY, MEDICAREQUALGOVEM, RAILROADFORMCT1. Required only for Form W-2 filing
        /// </summary>
        [DataMember]

        public string KindOfPayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsBusinessTerminated { get; set; }

        /// <summary>
        /// </summary>
        [DataMember]
        public bool? IsForeign { get; set; }  
        [DataMember]
        public bool IsForeignNullable { get { return IsForeign ?? false; } set { IsForeign = value; } }

        /// <summary>
        /// </summary>
        [DataMember]
        public USAddress USAddress { get; set; }

        /// <summary>
        /// </summary>
        [DataMember]
        public ForeignAddress ForeignAddress { get; set; }

        public string PayerAccNum { get; set; }

        [DataMember]
        public bool IsGovernmentalUnit { get; set; }
        public bool IsOnlineAccess { get; set; }

    }

    public class USAddress
    {
        /// <summary>
        /// Employer/Payer address (street address or post office box for the employer/payer).
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// The suite, apartment, number of the employer/payer, if applicable. This is optional.
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Employer/Payer city.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State code of the employer/payer. Refer Static values.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Employer/Payer ZIP Code.
        /// </summary>       
        public string ZipCd { get; set; }
    }
    public class ForeignAddress
    {
        /// <summary>
        /// Employer/Payer address (street address or post office box for the employer/payer).
        /// </summary>

        public string Address1 { get; set; }

        /// <summary>
        /// The suite, apartment, number of the employer/payer, if applicable. This is optional.
        /// </summary>

        public string Address2 { get; set; }

        /// <summary>
        /// Employer/Payer city.
        /// </summary>

        public string City { get; set; }

        /// <summary>
        /// Employer/Payer province or state Name
        /// </summary>

        public string ProvinceOrStateNm { get; set; }

        /// <summary>
        /// Employer/Payer country code
        /// </summary>

        public string Country { get; set; }

        /// <summary>
        /// Employer/Payer Postal code
        /// </summary>

        public string PostalCd { get; set; }
    }
    public class SigningAuthority
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string BusinessMemberType { get; set; }
    }
}
