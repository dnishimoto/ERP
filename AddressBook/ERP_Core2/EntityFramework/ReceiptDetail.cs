namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReceiptDetail")]
    public partial class ReceiptDetail
    {
        [Key]
        [Column(Order = 0)]
        public long ReceiptDetailId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ReceiptId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitCost { get; set; }

        public decimal? ExtendedCost { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
