using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Supplier
    {
        public Supplier()
        {
            PackingSlip = new HashSet<PackingSlip>();
            Poquote = new HashSet<Poquote>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
            SupplierInvoice = new HashSet<SupplierInvoice>();
            SupplierLedger = new HashSet<SupplierLedger>();
        }

        public long SupplierId { get; set; }
        public long AddressId { get; set; }
        public string Identification { get; set; }

        public AddressBook Address { get; set; }
        public AcctPay AcctPay { get; set; }
        public ICollection<PackingSlip> PackingSlip { get; set; }
        public ICollection<Poquote> Poquote { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrder { get; set; }
        public ICollection<SupplierInvoice> SupplierInvoice { get; set; }
        public ICollection<SupplierLedger> SupplierLedger { get; set; }
    }
}
