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

        public AddressBook Address { get; set; }
        public ICollection<AcctRec> AcctRec { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<CustomerClaim> CustomerClaim { get; set; }
        public ICollection<CustomerLedger> CustomerLedger { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<Poquote> Poquote { get; set; }
        public ICollection<SalesOrder> SalesOrder { get; set; }
        public ICollection<ScheduleEvent> ScheduleEvent { get; set; }
        public ICollection<ServiceInformation> ServiceInformation { get; set; }
        public ICollection<Shipments> Shipments { get; set; }
    }
}
