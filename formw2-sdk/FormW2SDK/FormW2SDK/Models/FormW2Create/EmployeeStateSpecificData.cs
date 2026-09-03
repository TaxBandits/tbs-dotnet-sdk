using System.Runtime.Serialization;

namespace FormW2SDK.Models.FormW2Create
{
    [DataContract]
    public class EmployeeStateSpecificData
    {
        [DataMember]
        public Maine ME { get; set; }
        [DataMember]
        public Oregon OR { get; set; }
        [DataMember]
        public Massachusetts MA { get; set; }

        [DataMember]
        public Kansas KS { get; set; }
        [DataMember]
        public NewJersey NJ { get; set; }
        [DataMember]
        public MarylandState MD { get; set; }

    }

    [DataContract]
    public class Maine
    {
        [DataMember]
        public string MEPERScontribution { get; set; }
    }

    [DataContract]
    public class Oregon
    {
        [DataMember]
        public string DateFirstEmployed { get; set; }
        [DataMember]
        public string DateOfSeparation { get; set; }
        [DataMember]
        public decimal TaxableWagesForStateTransitTax { get; set; }
        [DataMember]
        public decimal StateTransitTaxWH { get; set; }
    }

    [DataContract]
    public class Massachusetts
    {
        [DataMember]
        public decimal EeContributionMAPFML { get; set; }
    }

    [DataContract]
    public class Kansas
    {
        [DataMember]
        public decimal EmployeeContribution { get; set; }
    }

    [DataContract]
    public class NewJersey
    {
        [DataMember]
        public string PvtFmlLeaveInsuranceNum { get; set; }
        [DataMember]
        public decimal? FmlLeaveInsuranceWH { get; set; }
        [DataMember]
        public string PvtDisabilityPlanNum { get; set; }
        [DataMember]
        public decimal? DisabilityInsuranceWH { get; set; }
        [DataMember]
        public decimal? CombNJUnempInsWrkFrceDevProgHCSubWH { get; set; }
        [DataMember]
        public decimal? DeffCompAmt { get; set; }
        [DataMember]
        public bool? IsRetirementPlan { get; set; }
    }

    [DataContract]
    public class MarylandState
    {
        [DataMember]
        public int W4ExemptionCount { get; set; }
        [DataMember]
        public decimal StatePickupAmt { get; set; }
    }
}
