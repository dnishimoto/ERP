namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContractContent")]
    public partial class ContractContent
    {
        public long ContractContentId { get; set; }

        public long ContractId { get; set; }

        [StringLength(50)]
        public string WBS { get; set; }

        public string TextMemo { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
