namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackingSlip")]
    public partial class PackingSlip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PackingSlip()
        {
            PackingSlipDetails = new HashSet<PackingSlipDetail>();
        }

        public long PackingSlipId { get; set; }

        public long SupplierId { get; set; }

        public DateTime ReceivedDate { get; set; }

        [StringLength(50)]
        public string SlipDocument { get; set; }

        [StringLength(50)]
        public string PONumber { get; set; }

        public string Remark { get; set; }

        [StringLength(20)]
        public string SlipType { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackingSlipDetail> PackingSlipDetails { get; set; }
    }
}
