using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollEmployeeSequence
    {
        public long EmployeePaySequenceId { get; set; }
        public long PayRollPaySequenceNumber { get; set; }
        public long Employee { get; set; }
        public DateTime PayRollBeginDate { get; set; }
        public DateTime PayRollEndDate { get; set; }
        public long PaySequence { get; set; }
        public int PayRollCode { get; set; }

    }
}