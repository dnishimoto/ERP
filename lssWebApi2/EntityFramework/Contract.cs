﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Contract
    {
        public Contract()
        {
            ContractContent = new HashSet<ContractContent>();
            ServiceInformation = new HashSet<ServiceInformation>();
        }

        public long ContractId { get; set; }
        public long? CustomerId { get; set; }
        public long? ServiceTypeXrefId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public decimal? RemainingBalance { get; set; }
        public string Title { get; set; }

        public Customer Customer { get; set; }
        public Udc ServiceTypeXref { get; set; }
        public AcctPay AcctPay { get; set; }
        public ICollection<ContractContent> ContractContent { get; set; }
        public ICollection<ServiceInformation> ServiceInformation { get; set; }
    }
}
