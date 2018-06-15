namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceInformationInvoice")]
    public partial class ServiceInformationInvoice
    {
        public long ServiceInformationInvoiceId { get; set; }

        public long InvoiceId { get; set; }

        public long ServiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual ServiceInformation ServiceInformation { get; set; }
    }
}
