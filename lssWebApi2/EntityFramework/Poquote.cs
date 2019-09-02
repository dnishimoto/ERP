using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Poquote
    {
        public Poquote()
        {
            AcctPay = new HashSet<AcctPay>();
        }

        public long PoquoteId { get; set; }
        public decimal? QuoteAmount { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public long PoId { get; set; }
        public long DocNumber { get; set; }
        public string Remarks { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AcctPay> AcctPay { get; set; }

    }
}