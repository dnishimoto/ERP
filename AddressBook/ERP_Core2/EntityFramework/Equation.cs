namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Equation
    {
        public long Id { get; set; }

        [Column("equation")]
        [StringLength(255)]
        public string equation1 { get; set; }

        [StringLength(20)]
        public string queueid { get; set; }

        [StringLength(255)]
        public string evaluated { get; set; }

        [StringLength(10)]
        public string cellname { get; set; }
    }
}
