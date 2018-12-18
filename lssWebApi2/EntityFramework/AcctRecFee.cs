using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class AcctRecFee
    {
        public long AcctRecFeeId { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public long CustomerId { get; set; }
        public long DocNumber { get; set; }
        public string AcctRecDocType { get; set; }
        public long AcctRecId { get; set; }

        public virtual AcctRec AcctRec { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
