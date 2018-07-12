namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NextNumber")]
    public partial class NextNumber
    {
        public long NextNumberId { get; set; }

        [StringLength(20)]
        public string NextNumberName { get; set; }

        public long NextNumberValue { get; set; }
    }
}
