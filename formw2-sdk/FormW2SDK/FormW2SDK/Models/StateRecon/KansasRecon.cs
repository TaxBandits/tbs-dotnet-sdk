using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class KansasRecon
    {
        [DataMember]
        public ReconFormKW3 FormKW3 { get; set; }
    }
    [DataContract]
    public class ReconFormKW3
    {
        public long KansasReconKW3Id { get; set; }
        [DataMember]
        public string KSWithHoldingID { get; set; }
        [DataMember]
        public string WHAccClosedDate { get; set; }
        [DataMember]
        public int NumOf1099W2Forms { get; set; }
        [DataMember]
        public decimal TotTaxWH1099W2 { get; set; }
        [DataMember]
        public decimal WHTaxPaid { get; set; }
        [DataMember]
        public decimal CreditAvailable { get; set; }
        [DataMember]
        public decimal TotWHPayment { get; set; }
        [DataMember]
        public decimal Overpayment { get; set; }
        [DataMember]
        public decimal BalanceDue { get; set; }

        [DataMember]
        public decimal Penalty { get; set; }
        [DataMember]
        public decimal Interest { get; set; }

        [DataMember]
        public decimal TotTax { get; set; }

        [DataMember]
        public string FilingSchType { get; set; }

        [DataMember]
        public AnnualPayment Annual { get; set; }

        [DataMember]
        public QuarterPayment Quarterly { get; set; }
        [DataMember]
        public MonthPayment Monthly { get; set; }
        [DataMember]
        public SemiMonthPayment SemiMonthly { get; set; }
        [DataMember]
        public QuadMonthPayment QuadMonthly { get; set; }
        [DataMember]
        public decimal TotPayments { get; set; }

    }
    [DataContract]
    public class AnnualPayment
    {
        [DataMember]
        public decimal PaymentForYear { get; set; }
    }

    [DataContract]
    public class QuarterPayment
    {

        [DataMember]
        public decimal Qtr1 { get; set; }
        [DataMember]
        public decimal Qtr2 { get; set; }
        [DataMember]
        public decimal Qtr3 { get; set; }
        [DataMember]
        public decimal Qtr4 { get; set; }


    }

    [DataContract]
    public class MonthPayment
    {
        [DataMember]
        public decimal Jan { get; set; }
        [DataMember]
        public decimal Feb { get; set; }
        [DataMember]
        public decimal Mar { get; set; }
        [DataMember]
        public decimal Apr { get; set; }
        [DataMember]
        public decimal May { get; set; }
        [DataMember]
        public decimal Jun { get; set; }
        [DataMember]
        public decimal Jul { get; set; }
        [DataMember]
        public decimal Aug { get; set; }
        [DataMember]
        public decimal Sep { get; set; }

        [DataMember]
        public decimal Oct { get; set; }

        [DataMember]
        public decimal Nov { get; set; }
        [DataMember]
        public decimal Dec { get; set; }
    }

    [DataContract]
    public class SemiMonthPayment
    {
        [DataMember]
        public decimal Jan1to15 { get; set; }
        [DataMember]
        public decimal Jan16to31 { get; set; }
        [DataMember]
        public decimal Feb1to15 { get; set; }
        [DataMember]
        public decimal Feb16toEOM { get; set; }
        [DataMember]
        public decimal Mar1to15 { get; set; }
        [DataMember]
        public decimal Mar16to31 { get; set; }
        [DataMember]
        public decimal Apr1to15 { get; set; }
        [DataMember]
        public decimal Apr16to30 { get; set; }
        [DataMember]
        public decimal May1to15 { get; set; }
        [DataMember]
        public decimal May16to31 { get; set; }
        [DataMember]
        public decimal Jun1to15 { get; set; }
        [DataMember]
        public decimal Jun16to30 { get; set; }
        [DataMember]
        public decimal Jul1to15 { get; set; }
        [DataMember]
        public decimal Jul16to31 { get; set; }
        [DataMember]
        public decimal Aug1to15 { get; set; }
        [DataMember]
        public decimal Aug16to31 { get; set; }
        [DataMember]
        public decimal Sep1to15 { get; set; }
        [DataMember]
        public decimal Sep16to30 { get; set; }
        [DataMember]
        public decimal Oct1to15 { get; set; }
        [DataMember]
        public decimal Oct16to31 { get; set; }
        [DataMember]
        public decimal Nov1to15 { get; set; }
        [DataMember]
        public decimal Nov16to30 { get; set; }
        [DataMember]
        public decimal Dec1to15 { get; set; }
        [DataMember]
        public decimal Dec16to31 { get; set; }

    }

    [DataContract]
    public class QuadMonthPayment
    {
        [DataMember]
        public decimal Jan1to7 { get; set; }
        [DataMember]
        public decimal Jan8to15 { get; set; }
        [DataMember]
        public decimal Jan16to21 { get; set; }
        [DataMember]
        public decimal Jan22to31 { get; set; }
        [DataMember]
        public decimal Feb1to7 { get; set; }
        [DataMember]
        public decimal Feb8to15 { get; set; }
        [DataMember]
        public decimal Feb16to21 { get; set; }
        [DataMember]
        public decimal Feb22toEOM { get; set; }
        [DataMember]
        public decimal Mar1to7 { get; set; }
        [DataMember]
        public decimal Mar8to15 { get; set; }
        [DataMember]
        public decimal Mar16to21 { get; set; }
        [DataMember]
        public decimal Mar22to31 { get; set; }
        [DataMember]
        public decimal Apr1to7 { get; set; }
        [DataMember]
        public decimal Apr8to15 { get; set; }
        [DataMember]
        public decimal Apr16to21 { get; set; }
        [DataMember]
        public decimal Apr22to30 { get; set; }
        [DataMember]
        public decimal May1to7 { get; set; }
        [DataMember]
        public decimal May8to15 { get; set; }
        [DataMember]
        public decimal May16to21 { get; set; }
        [DataMember]
        public decimal May22to31 { get; set; }
        [DataMember]
        public decimal Jun1to7 { get; set; }
        [DataMember]
        public decimal Jun8to15 { get; set; }
        [DataMember]
        public decimal Jun16to21 { get; set; }
        [DataMember]
        public decimal Jun22to30 { get; set; }
        [DataMember]
        public decimal Jul1to7 { get; set; }
        [DataMember]
        public decimal Jul8to15 { get; set; }
        [DataMember]
        public decimal Jul16to21 { get; set; }
        [DataMember]
        public decimal Jul22to31 { get; set; }
        [DataMember]
        public decimal Aug1to7 { get; set; }
        [DataMember]
        public decimal Aug8to15 { get; set; }
        [DataMember]
        public decimal Aug16to21 { get; set; }
        [DataMember]
        public decimal Aug22to31 { get; set; }
        [DataMember]
        public decimal Sep1to7 { get; set; }
        [DataMember]
        public decimal Sep8to15 { get; set; }
        [DataMember]
        public decimal Sep16to21 { get; set; }
        [DataMember]
        public decimal Sep22to30 { get; set; }
        [DataMember]
        public decimal Oct1to7 { get; set; }
        [DataMember]
        public decimal Oct8to15 { get; set; }
        [DataMember]
        public decimal Oct16to21 { get; set; }
        [DataMember]
        public decimal Oct22to31 { get; set; }
        [DataMember]
        public decimal Nov1to7 { get; set; }
        [DataMember]
        public decimal Nov8to15 { get; set; }
        [DataMember]
        public decimal Nov16to21 { get; set; }
        [DataMember]
        public decimal Nov22to30 { get; set; }
        [DataMember]
        public decimal Dec1to7 { get; set; }
        [DataMember]
        public decimal Dec8to15 { get; set; }
        [DataMember]
        public decimal Dec16to21 { get; set; }
        [DataMember]
        public decimal Dec22to31 { get; set; }
    }

}
