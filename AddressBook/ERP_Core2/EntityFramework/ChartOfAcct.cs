namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChartOfAcct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChartOfAcct()
        {
            AccountBalances = new HashSet<AccountBalance>();
            AcctPays = new HashSet<AcctPay>();
            AcctRecs = new HashSet<AcctRec>();
            Budgets = new HashSet<Budget>();
            BudgetRanges = new HashSet<BudgetRange>();
            GeneralLedgers = new HashSet<GeneralLedger>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        [Key]
        public long AccountId { get; set; }

        [StringLength(10)]
        public string Location { get; set; }

        [StringLength(10)]
        public string BusUnit { get; set; }

        [StringLength(10)]
        public string Subsidiary { get; set; }

        [StringLength(10)]
        public string SubSub { get; set; }

        [StringLength(30)]
        public string Account { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(10)]
        public string CompanyNumber { get; set; }

        [StringLength(3)]
        public string GenCode { get; set; }

        [StringLength(3)]
        public string SubCode { get; set; }

        [StringLength(20)]
        public string ObjectNumber { get; set; }

        [StringLength(10)]
        public string SupCode { get; set; }

        [StringLength(20)]
        public string ThirdAccount { get; set; }

        [StringLength(10)]
        public string CategoryCode1 { get; set; }

        [StringLength(10)]
        public string CategoryCode2 { get; set; }

        [StringLength(10)]
        public string CategoryCode3 { get; set; }

        [StringLength(10)]
        public string PostEditCode { get; set; }

        public long CompanyId { get; set; }

        public int Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountBalance> AccountBalances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctPay> AcctPays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctRec> AcctRecs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Budget> Budgets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BudgetRange> BudgetRanges { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedger> GeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
