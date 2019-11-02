using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollCurrentPaySequence
    {
        public long PayRollCurrentPaySequenceId { get; set; }
        public long PaySequence { get; set; }
        public long PayRollCurrentPaySequenceNumber { get; set; }
        public long PayRollCode { get; set; }
        public DateTime PayRollBeginDate { get; set; }
        public DateTime PayRollEndDate { get; set; }
        public string Frequency { get; set; }
        public bool Active { get; set; }

    }
}