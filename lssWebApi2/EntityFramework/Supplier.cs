using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
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

        public virtual AddressBook Address { get; set; }
        public virtual AcctPay AcctPay { get; set; }
        public virtual ICollection<PackingSlip> PackingSlip { get; set; }
        public virtual ICollection<Poquote> Poquote { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual ICollection<SupplierInvoice> SupplierInvoice { get; set; }
        public virtual ICollection<SupplierLedger> SupplierLedger { get; set; }

    }
}