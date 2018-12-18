using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class AcctRecInterest
    {
        public long AcctRecInterestId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? InterestFromDate { get; set; }
        public DateTime? InterestToDate { get; set; }
        public long DocNumber { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public long CustomerId { get; set; }
        public string AcctRecDocType { get; set; }
        public DateTime? LastInterestDueDate { get; set; }
        public long AcctRecId { get; set; }

        public virtual AcctRec AcctRec { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
