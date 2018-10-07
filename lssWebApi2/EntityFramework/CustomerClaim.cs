﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class CustomerClaim
    {
        public long ClaimId { get; set; }
        public long ClassificationXrefId { get; set; }
        public long CustomerId { get; set; }
        public string Configuration { get; set; }
        public string Note { get; set; }
        public long EmployeeId { get; set; }
        public long GroupIdXrefId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ContractId { get; set; }

        public virtual Udc ClassificationXref { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Udc GroupIdXref { get; set; }
    }
}