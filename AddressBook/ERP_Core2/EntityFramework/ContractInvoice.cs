namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContractInvoice")]
    public partial class ContractInvoice
    {
        public long ContractInvoiceId { get; set; }

        public long ContractId { get; set; }

        public long InvoiceId { get; set; }
    }
}
