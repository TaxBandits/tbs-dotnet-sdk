﻿using Form1099NecSDK.Models.Base;
using System.Runtime.Serialization;

namespace Form1099NecSDK.Models.Form1099NECStatus
{

    [DataContract]
    public class Form1099NecStatusResponse : BaseResponseStatus
    {

        [DataMember(Order = 1)]
        public Guid? SubmissionId { get; set; }

        [DataMember(Order = 2)]
        public Guid? BusinessId { get; set; }

        [DataMember(Order = 3)]
        public string PayerRef { get; set; }

        [DataMember(Order = 5)]
        public Form1099NecRecords Form1099Records { get; set; }

        [DataMember(Order = 6)]
        public List<ErrorV3> Errors { get; set; }
    }
}
