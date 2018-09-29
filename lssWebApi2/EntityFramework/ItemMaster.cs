using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
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

        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
        public ICollection<ShipmentsDetail> ShipmentsDetail { get; set; }
        public ICollection<SupplierInvoiceDetail> SupplierInvoiceDetail { get; set; }
    }
}
