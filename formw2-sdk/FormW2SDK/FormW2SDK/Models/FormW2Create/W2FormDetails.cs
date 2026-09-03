using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class W2FormDetails
    {
        #region Form Details
        /// <summary>
        /// Total taxable wages or salary, before any payroll deductions, that was paid to the employee during the year.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX1-01:Maximum 12 characters only allowed")]
        public decimal B1Wages { get; set; }

        /// <summary>
        /// Total federal income tax withheld from the employee's wages for the year. Include the 20% excise tax withheld on excess parachute payments.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX2-01:Maximum 12 characters only allowed")]
        public decimal B2FedTaxWH { get; set; }

        /// <summary>
        /// Total wages paid (before payroll deductions) subject to employee social security tax.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX3-01:Maximum 12 characters only allowed")]
        public decimal B3SocSecWages { get; set; }

        /// <summary>
        /// Total employee social security tax (not your share) withheld, including social security tax on tips.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX4-01:Maximum 12 characters only allowed")]
        public decimal B4SocSecTaxWH { get; set; }

        /// <summary>
        /// Medicare wages and tips reported. The wages and tips subject to Medicare tax are the same as those subject to social security tax except that there is no wage base limit for Medicare tax.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX5-01:Maximum 12 characters only allowed")]
        public decimal B5MediWages { get; set; }

        /// <summary>
        /// Total employee Medicare tax (not your share) withheld.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX6-01:Maximum 12 characters only allowed")]
        public decimal B6MediTaxWH { get; set; }

        /// <summary>
        /// Tips that the employee reported to you even if you did not have enough employee funds to collect the social security tax for the tips.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX7-01:Maximum 12 characters only allowed")]
        public decimal B7SocSecTips { get; set; }

        /// <summary>
        /// If you are a food or beverage establishment, show the tips allocated to the employee.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX8-01:Maximum 12 characters only allowed")]
        public decimal B8AllocatedTips { get; set; }

        /// <summary>
        /// Total amount of dependent care benefits that you paid or was incurred by you for your employee.
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX10-01:Maximum 12 characters only allowed")]
        public decimal B10DependtCareBenefits { get; set; }
        /// <summary>
        /// Report distributions to an employee from a nonqualified plan
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX11-01:Maximum 12 characters only allowed")]
        public decimal B11Sec457Plan { get; set; }
        /// <summary>
        /// Report distributions to an employee from a nongovernmental section 457(b) plan
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX11-02:Maximum 12 characters only allowed")]
        public decimal B11NonSec457Plan { get; set; }
        /// <summary>
        /// Box 12a Code
        /// </summary>
        [DataMember]
        //[MaxLength(2, ErrorMessage = "ERR-BOX12-01:Maximum 2 characters only allowed")]
        //[RegularExpression(@"[A-Z]+$", ErrorMessage = "ERR-VALBOX12-01:Enter valid Box12 value")]
        public string B12aCd { get; set; }
        /// <summary>
        /// Box 12a Amount
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX12-02:Maximum 12 characters only allowed")]
        public decimal B12aAmt { get; set; }
        /// <summary>
        /// Box 12b Code
        /// </summary>
        [DataMember]
        //[MaxLength(2, ErrorMessage = "ERR-BOX12-03:Maximum 2 characters only allowed")]
        //[RegularExpression(@"[A-Z]+$", ErrorMessage = "ERR-VALBOX12-02:Enter valid Box12 value")]
        public string B12bCd { get; set; }
        /// <summary>
        /// Box 12b Amount
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX12-04:Maximum 12 characters only allowed")]
        public decimal B12bAmt { get; set; }
        /// <summary>
        /// Box 12c Code
        /// </summary>
        [DataMember]
        //[MaxLength(2, ErrorMessage = "ERR-BOX12-05:Maximum 2 characters only allowed")]
        //[RegularExpression(@"[A-Z]+$", ErrorMessage = "ERR-VALBOX12-03:Enter valid Box12 value")]
        public string B12cCd { get; set; }

        /// <summary>
        /// Box 12c Amount
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX12-06:Maximum 12 characters only allowed")]
        public decimal B12cAmt { get; set; }
        /// <summary>
        /// Box 12d Code
        /// </summary>
        [DataMember]
        //[MaxLength(2, ErrorMessage = "ERR-BOX12-07:Maximum 2 characters only allowed")]
        //[RegularExpression(@"[A-Z]+$", ErrorMessage = "ERR-VALBOX12-04:Enter valid Box12 value")]
        public string B12dCd { get; set; }

        /// <summary>
        /// Box 12d Amount
        /// </summary>
        [DataMember]
        //[Range(0, 999999999.99, ErrorMessage = "ERR-BOX12-08:Maximum 12 characters only allowed")]
        public decimal B12dAmt { get; set; }
        /// <summary>
        /// Statutory employees whose earnings are subject to social security and Medicare taxes but not subject to federal income tax withholding. Statutory employees are usually salespeople or other employees who work on commission. This will reflect whether the employee is a Statutory employee or not
        /// </summary>
        [DataMember]
        public bool B13IsStatEmp { get; set; }
        /// <summary>
        /// This will show whether the employee is a member of a retirement plan or not.
        /// </summary>
        [DataMember]
        public bool B13IsRetPlan { get; set; }
        /// <summary>
        /// This will show whether your business is reporting third-party sick pay or not
        /// </summary>
        [DataMember]
        public bool B13Is3rdPartySickPay { get; set; }
        /// <summary>
        /// Any additional information of benefits the employer wants the employee to have and cannot be reported in boxes 1-13 can be listed here. Examples would be auto allowance, travel reimbursement, relocation expenses, job uniforms, after-tax employee contributions to an HSA, etc.
        /// </summary>
        [DataMember]
        //[MaxLength(51, ErrorMessage = "ERR-BOX-14:Maximum 51 characters only allowed")]
   
        public string B14Other { get; set; }

        /// <summary>
        /// Control Number
        /// </summary>
        [DataMember]
  
        public string ControlNum { get; set; }

        [DataMember]
        public List<FormW2StateDetails> States { get; set; }
        [DataMember]
        public EmployeeStateSpecificData EmployeeStateSpecificData { get; set; }
        #endregion
    }
}
