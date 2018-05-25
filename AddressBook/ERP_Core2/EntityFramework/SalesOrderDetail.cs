namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesOrderDetail")]
    public partial class SalesOrderDetail
    {
        public long SalesOrderDetailId { get; set; }

        public long SalesOrderId { get; set; }

        public long ItemId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? Quantity { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        public decimal? UnitPrice { get; set; }

        public long? InvoiceDetailId { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
    }
}
