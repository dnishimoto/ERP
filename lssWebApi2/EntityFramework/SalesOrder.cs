using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
        }

        public long SalesOrderId { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountOpen { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public long CustomerId { get; set; }
        public string TakenBy { get; set; }
        public decimal? FreightAmount { get; set; }
        public string PaymentInstrument { get; set; }
        public string PaymentTerms { get; set; }
        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }

    }
}
