using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ServiceInformation
    {
        public ServiceInformation()
        {
            ScheduleEvent = new HashSet<ScheduleEvent>();
            ServiceInformationInvoice = new HashSet<ServiceInformationInvoice>();
        }

        public long ServiceId { get; set; }
        public string ServiceDescription { get; set; }
        public decimal? Price { get; set; }
        public string AddOns { get; set; }
        public long? ServiceTypeXrefId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long ContractId { get; set; }
        public int? SquareFeetOfStructure { get; set; }
        public string LocationDescription { get; set; }
        public string LocationGps { get; set; }
        public string Comments { get; set; }
        public bool Status { get; set; }

        public Contract Contract { get; set; }
        public Customer Customer { get; set; }
        public LocationAddress Location { get; set; }
        public Udc ServiceTypeXref { get; set; }
        public ICollection<ScheduleEvent> ScheduleEvent { get; set; }
        public ICollection<ServiceInformationInvoice> ServiceInformationInvoice { get; set; }
    }
}
