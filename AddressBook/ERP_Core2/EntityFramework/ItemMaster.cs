namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemMaster")]
    public partial class ItemMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemMaster()
        {
            Inventories = new HashSet<Inventory>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
            ShipmentsDetails = new HashSet<ShipmentsDetail>();
            SupplierInvoiceDetails = new HashSet<SupplierInvoiceDetail>();
        }

        [Key]
        public long ItemId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(20)]
        public string UnitOfMeasure { get; set; }

        [StringLength(10)]
        public string CommodityCode { get; set; }

        [StringLength(255)]
        public string Description2 { get; set; }

        [Required]
        [StringLength(20)]
        public string ItemNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipmentsDetail> ShipmentsDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierInvoiceDetail> SupplierInvoiceDetails { get; set; }
    }
}
