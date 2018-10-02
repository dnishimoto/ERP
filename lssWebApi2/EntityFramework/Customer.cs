using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Customer
    {
        public Customer()
        {
            AcctRec = new HashSet<AcctRec>();
            Contract = new HashSet<Contract>();
            CustomerClaim = new HashSet<CustomerClaim>();
            CustomerLedger = new HashSet<CustomerLedger>();
            Invoice = new HashSet<Invoice>();
            Poquote = new HashSet<Poquote>();
            SalesOrder = new HashSet<SalesOrder>();
            ScheduleEvent = new HashSet<ScheduleEvent>();
            ServiceInformation = new HashSet<ServiceInformation>();
            Shipments = new HashSet<Shipments>();
        }

        public long CustomerId { get; set; }
        public long AddressId { get; set; }
        public long? PrimaryShippedToLocationId { get; set; }
        public long? PrimaryEmailId { get; set; }
        public long? PrimaryPhoneId { get; set; }
        public long? MailingLocationId { get; set; }
        public long? PrimaryBillingLocationId { get; set; }
        public string TaxIdentification { get; set; }

        public virtual AddressBook Address { get; set; }
        public virtual ICollection<AcctRec> AcctRec { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<CustomerClaim> CustomerClaim { get; set; }
        public virtual ICollection<CustomerLedger> CustomerLedger { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Poquote> Poquote { get; set; }
        public virtual ICollection<SalesOrder> SalesOrder { get; set; }
        public virtual ICollection<ScheduleEvent> ScheduleEvent { get; set; }
        public virtual ICollection<ServiceInformation> ServiceInformation { get; set; }
        public virtual ICollection<Shipments> Shipments { get; set; }
    }
}
