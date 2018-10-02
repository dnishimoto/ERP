using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            AcctPay = new HashSet<AcctPay>();
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
        }

        public long PurchaseOrderId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public decimal? GrossAmount { get; set; }
        public string Remark { get; set; }
        public DateTime? Gldate { get; set; }
        public long AccountId { get; set; }
        public long SupplierId { get; set; }
        public long? ContractId { get; set; }
        public long? PoquoteId { get; set; }
        public string Description { get; set; }
        public string Ponumber { get; set; }
        public string TakenBy { get; set; }
        public long? BuyerId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? PromisedDeliveredDate { get; set; }
        public decimal? Tax { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public string ShippedToName { get; set; }
        public string ShippedToAddress1 { get; set; }
        public string ShippedToAddress2 { get; set; }
        public string ShippedToCity { get; set; }
        public string ShippedToZipcode { get; set; }
        public string ShippedToState { get; set; }
        public string TaxCode1 { get; set; }
        public string TaxCode2 { get; set; }

        public virtual ChartOfAccts Account { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AcctPay> AcctPay { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
    }
}
