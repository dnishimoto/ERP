using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class ItemMaster
    {
        public ItemMaster()
        {
            Inventory = new HashSet<Inventory>();
            InvoiceDetail = new HashSet<InvoiceDetail>();
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
            ShipmentsDetail = new HashSet<ShipmentsDetail>();
            SupplierInvoiceDetail = new HashSet<SupplierInvoiceDetail>();
        }

        public long ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CommodityCode { get; set; }
        public string Description2 { get; set; }
        public string ItemNumber { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Branch { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitOfMeasure { get; set; }
        public decimal? Volume { get; set; }
        public string VolumeUnitOfMeasure { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual ICollection<ShipmentsDetail> ShipmentsDetail { get; set; }
        public virtual ICollection<SupplierInvoiceDetail> SupplierInvoiceDetail { get; set; }

    }
}
