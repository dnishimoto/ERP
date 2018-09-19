namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BudgetRange")]
    public partial class BudgetRange
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BudgetRange()
        {
            Budgets = new HashSet<Budget>();
        }

        [Key]
        public long RangeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [StringLength(2)]
        public string Location { get; set; }

        [StringLength(3)]
        public string GenCode { get; set; }

        [StringLength(3)]
        public string SubCode { get; set; }

        [StringLength(10)]
        public string CompanyCode { get; set; }

        [StringLength(10)]
        public string BusinessUnit { get; set; }

        [StringLength(10)]
        public string Subsidiary { get; set; }

        public long? AccountId { get; set; }

        [StringLength(50)]
        public string SupervisorCode { get; set; }

        public DateTime? LastUpdated { get; set; }

        [StringLength(10)]
        public string ObjectNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Budget> Budgets { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }
    }
}
