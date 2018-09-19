namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralLedger")]
    public partial class GeneralLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralLedger()
        {
            CustomerLedgers = new HashSet<CustomerLedger>();
            SupplierLedgers = new HashSet<SupplierLedger>();
        }

        public long GeneralLedgerId { get; set; }

        public long DocNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string DocType { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(10)]
        public string LedgerType { get; set; }

        public DateTime GLDate { get; set; }

        public long AccountId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long AddressId { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? CreditAmount { get; set; }

        public int? FiscalYear { get; set; }

        public int? FiscalPeriod { get; set; }

        [StringLength(50)]
        public string CheckNumber { get; set; }

        [StringLength(50)]
        public string PurchaseOrderNumber { get; set; }

        public decimal? Units { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLedger> CustomerLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierLedger> SupplierLedgers { get; set; }
    }
}
