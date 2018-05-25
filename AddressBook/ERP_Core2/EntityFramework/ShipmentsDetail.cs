namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShipmentsDetail")]
    public partial class ShipmentsDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ShipmentDetailId { get; set; }

        public long ShipmentId { get; set; }

        public long ItemId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Amount { get; set; }

        public long SalesOrderDetailId { get; set; }
    }
}
