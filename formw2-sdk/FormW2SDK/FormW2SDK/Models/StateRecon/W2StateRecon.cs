using System.Runtime.Serialization;

namespace FormW2SDK.Models.StateRecon
{
    [DataContract]
    public class W2StateRecon
    {
        [DataMember]
        public AlabamaRecon AL { get; set; }
        [DataMember]
        public IdahoRecon ID { get; set; }
        [DataMember]
        public ConnecticutRecon CT { get; set; }
        [DataMember]
        public LouisianaRecon LA { get; set; }
        [DataMember]
        public KansasRecon KS { get; set; }
        [DataMember]
        public NewJerseyRecon NJ { get; set; }
        [DataMember]
        public Maryland MD { get; set; }
        [DataMember]
        public PennsylvaniaRecon PA { get; set; }
        [DataMember]
        public WestVirginiaRecon WV { get; set; }
        [DataMember]
        public VermontRecon VT { get; set; }
        [DataMember]
        public ArizonaRecon AZ { get; set; }
        [DataMember]
        public IndianaRecon IN { get; set; }


    }
}
