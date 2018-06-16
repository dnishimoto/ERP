using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.AbstractFactory
{
    public abstract partial class AbstractFactory
    {
        public abstract SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor);
        public abstract EmployeeView MapEmployeeView(Employee employee);
        public abstract SupplierView MapSupplierView(Supplier supplier);
        public abstract CarrierView MapCarrierView(Carrier carrier);
    }
    public abstract partial class AbstractFactory
    {
        public abstract BuyerView MapBuyerView(Buyer buyer);
        public abstract InvoiceView MapInvoiceView(Invoice invoice);
        public abstract InvoiceDetailView MapInvoiceDetails(InvoiceDetail invoiceDetail);
        public abstract CustomerClaimView MapCustomerClaimView(CustomerClaim customerClaim);
        public abstract ScheduleEventView MapScheduleEventView(ScheduleEvent scheduleEvent);
        public abstract ContractView MapContractView(Contract contract);
        public abstract LocationAddressView MapLocationAddressView(LocationAddress locationAddress);
        public abstract PhoneView MapPhoneView(Phone phone);
        public abstract EmailView MapEmailView(Email email);
    }
    public abstract class BusinessViewFactory : AbstractFactory
    {
    }
    
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor)
        {
            return new SupervisorView(supervisor, parentSupervisor);
        }
        public override EmployeeView MapEmployeeView(Employee employee)
        {
            return new EmployeeView(employee);
        }
        public override SupplierView MapSupplierView(Supplier supplier)
        {
            return new SupplierView(supplier);
        }
        public override CarrierView MapCarrierView(Carrier carrier)
        {
            return new CarrierView(carrier);
        }
    }
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override BuyerView MapBuyerView(Buyer buyer)
        {
            return new BuyerView(buyer);
        }
        public override InvoiceView MapInvoiceView(Invoice invoice)
        {
            InvoiceView invoiceView= new InvoiceView(invoice);
            List<InvoiceDetailView> list = new List<InvoiceDetailView>();
            

            foreach (var item in invoice.InvoiceDetails)
            {
                list.Add(MapInvoiceDetails(item));
            }
            invoiceView.InvoiceViewDetails = list;
            return invoiceView;
        }
        public override InvoiceDetailView MapInvoiceDetails(InvoiceDetail invoiceDetail)
        {
            return new InvoiceDetailView(invoiceDetail);
        }
        public override CustomerClaimView MapCustomerClaimView(CustomerClaim customerClaim)
        {
            return new CustomerClaimView(customerClaim);
        }
        public override ScheduleEventView MapScheduleEventView(ScheduleEvent scheduleEvent)
        {
            return new ScheduleEventView(scheduleEvent);
        }
        public override ContractView MapContractView(Contract contract)
        {
            return new ContractView(contract);
        }
        public override LocationAddressView MapLocationAddressView(LocationAddress locationAddress)
        {
            return new LocationAddressView(locationAddress);
        }
        public override PhoneView MapPhoneView(Phone phone)
        {
            return new PhoneView(phone);
        }
        public override EmailView MapEmailView(Email email)
        {
            return new EmailView(email);
        }

    }
}
