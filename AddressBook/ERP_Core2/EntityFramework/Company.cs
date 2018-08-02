namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            ChartOfAccts = new HashSet<ChartOfAcct>();
            Invoices = new HashSet<Invoice>();
        }

        public long CompanyId { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(10)]
        public string CompanyCode { get; set; }

        [StringLength(100)]
        public string CompanyStreet { get; set; }

        [StringLength(50)]
        public string CompanyCity { get; set; }

        [StringLength(20)]
        public string CompanyState { get; set; }

        [StringLength(20)]
        public string CompanyZipcode { get; set; }

        [StringLength(20)]
        public string TaxCode1 { get; set; }

        [StringLength(20)]
        public string TaxCode2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChartOfAcct> ChartOfAccts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
