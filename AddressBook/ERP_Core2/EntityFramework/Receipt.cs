namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Receipt")]
    public partial class Receipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Receipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public long ReceiptId { get; set; }

        public long SupplierId { get; set; }

        public DateTime ReceiptDate { get; set; }

        [StringLength(50)]
        public string ReceiptDocument { get; set; }

        [StringLength(50)]
        public string OrderNumber { get; set; }

        public string Remark { get; set; }

        public long ReceiptTypeXrefId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual UDC UDC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
