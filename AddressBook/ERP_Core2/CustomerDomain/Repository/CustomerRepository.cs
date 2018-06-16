using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.CustomerDomain
{
    public class InvoiceView
    {
        public InvoiceView() { }
        public InvoiceView(Invoice invoice)
        {
            this.InvoiceId = invoice.InvoiceId;
            this.InvoiceNumber = invoice.InvoiceNumber;
            this.InvoiceDate = invoice.InvoiceDate;
            this.Amount = invoice.Amount;
            this.CustomerName = invoice.Customer.AddressBook.Name;
            this.Description = invoice.Description;
            this.TaxAmount = invoice.TaxAmount;
            this.PaymentDueDate = invoice.PaymentDueDate;
            this.PaymentTerms = invoice.PaymentTerms;
            this.CompanyName = invoice.Company.CompanyName;
            this.CompanyStreet = invoice.Company.CompanyStreet;
            this.CompanyCity = invoice.Company.CompanyCity;
            this.CompanyZipcode = invoice.Company.CompanyZipcode;

        }
        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public string CustomerName { get; set; }
       public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string PaymentTerms { get; set; }
        public virtual ICollection InvoiceViewDetails { get; set; }
    }
    public class InvoiceDetailView
    {
        public InvoiceDetailView() { }
        public InvoiceDetailView(InvoiceDetail invoiceDetail)
        {
            this.InvoiceDetailId = invoiceDetail.InvoiceDetailId;
            this.UnitOfMeasure = invoiceDetail.UnitOfMeasure;
            this.Quantity = invoiceDetail.Quantity;
            this.UnitPrice = invoiceDetail.UnitPrice;
            this.Amount = invoiceDetail.Amount;
            this.DiscountPercent = invoiceDetail.DiscountPercent;
            this.DiscountAmount = invoiceDetail.DiscountAmount;
            this.ItemNumber = invoiceDetail.ItemMaster.ItemNumber;
            this.ItemDescription = invoiceDetail.ItemMaster.Description;
            this.ItemDescription2 = invoiceDetail.ItemMaster.Description2;
            this.ExtendedDescription = invoiceDetail.ExtendedDescription;
        }
            public int? Quantity { get; set; }
            public string UnitOfMeasure { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal? Amount { get; set; }
            //todo public long? PurchaseOrderLineId { get; set; }
            //todo public long? SalesOrderDetailId { get; set; }
            public decimal? DiscountPercent { get; set; }
            public decimal? DiscountAmount { get; set; }
            //todo    public long? ShipmentDetailId { get; set; }
            //todo maybe public string InvoiceNumber { get; set; }
            //public virtual Invoice Invoice { get; set; }

            public string ItemNumber { get; set; }
            public string ItemDescription { get; set; }
            public string ItemDescription2 { get; set; }
     
            public long? InvoiceDetailId { get; set; }
            public string ExtendedDescription { get; set; }
    }
    public class CustomerClaimView
    {
        public CustomerClaimView() { }
        public CustomerClaimView(CustomerClaim customerClaim)
        {
            this.ClaimId = customerClaim.ClaimId;
            this.Classification = customerClaim.UDC.Value;
            this.CustomerId = customerClaim.CustomerId;
            this.CustomerName = customerClaim.Customer.AddressBook.Name;
            this.Configuration = customerClaim.Configuration;
            this.Note = customerClaim.Note;
            this.EmployeeName = customerClaim.Employee.AddressBook.Name;
            this.GroupId = customerClaim.UDC1.Value;
            this.ProcessedDate = customerClaim.ProcessedDate;
            this.CreatedDate = customerClaim.CreatedDate;
    }

        public long? ClaimId { get; set; }
        public string Classification { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Configuration { get; set; }
        public string Note { get; set; }
        public string EmployeeName { get; set; }
        public string GroupId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
  
    }
        public class PurchaseOrderView
    {
        public long? PurchaseOrderId { get; set; }
    }
    public class AccountsReceiveableView
    {
        
    }
    public class ScheduleEventView
    {
        public ScheduleEventView() { }
        public ScheduleEventView(ScheduleEvent scheduleEvent)
        {
            this.ScheduleEventId = scheduleEvent.ScheduleEventId;
            this.EmployeeId = scheduleEvent.EmployeeId;
            this.EmployeeName = scheduleEvent.Employee.AddressBook.Name;
            this.EventDateTime = scheduleEvent.EventDateTime;
            this.ServiceId = scheduleEvent.ServiceId;
            this.DurationMinutes = scheduleEvent.DurationMinutes;
            this.CustomerId = scheduleEvent.CustomerId;
            this.CustomerName = scheduleEvent.Customer.AddressBook.Name;
            this.ServiceDescription = scheduleEvent.ServiceInformation.ServiceDescription;
            this.Price = scheduleEvent.ServiceInformation.Price;
            this.AddOns = scheduleEvent.ServiceInformation.AddOns;
            this.ServiceTypeXRefId = scheduleEvent.ServiceInformation.ServiceTypeXRefId;
            this.ServiceType = scheduleEvent.ServiceInformation.UDC.Value;
            this.CreatedDate = scheduleEvent.ServiceInformation.CreatedDate;

            this.SquareFeetOfStructure = scheduleEvent.ServiceInformation.SquareFeetOfStructure;
            this.LocationDescription = scheduleEvent.ServiceInformation.LocationDescription;
            this.LocationGPS = scheduleEvent.ServiceInformation.LocationGPS;
            this.Comments = scheduleEvent.ServiceInformation.Comments;

           
        }
        public long ScheduleEventId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? EventDateTime { get; set; }
        public long ServiceId { get; set; }
        public long? DurationMinutes { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }


        //Service Information
        public string ServiceDescription { get; set; }
        public decimal? Price { get; set; }
        public string AddOns { get; set; }
        public long? ServiceTypeXRefId { get; set; }
        public string ServiceType { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? SquareFeetOfStructure { get; set; }
        public string LocationDescription { get; set; }
        public string LocationGPS { get; set; }
        public string Comments { get; set; }
        public LocationAddressView LocationAddressView { get; set; }
        public ContractView ContractView { get; set; }
 
    }
    public class LocationAddressView
    {
        public LocationAddressView() { }
        public LocationAddressView(LocationAddress locationAddress)
        {
            this.Address_Line1 = locationAddress.Address_Line_1;
            this.Address_Line2 = locationAddress.Address_Line_2;
            this.City = locationAddress.City;
            this.State = locationAddress.UDC.Value;
            this.Zipcode = locationAddress.Zipcode;
        }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
}
    public class ContractView
    {
        public ContractView() { }
        public ContractView(Contract contract)
        {
             this.ContractId=contract.ContractId;
            this.CustomerId = contract.CustomerId;
            this.CustomerName = contract.Customer.AddressBook.Name;
            this.ServiceTypeXRefId = contract.ServiceTypeXRefId;
            this.ServiceType = contract.UDC.Value;
            this.StartDate = contract.StartDate;
            this.EndDate = contract.EndDate;
            this.Cost = contract.Cost;
            this.RemainingBalance = contract.RemainingBalance;
    }

        public long ContractId { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? ServiceTypeXRefId { get; set; }
        public string ServiceType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public decimal? RemainingBalance { get; set; }
 
    }
    public class PhoneView
    {
        public PhoneView() { }
        public PhoneView(Phone phone)
        {
            this.PhoneId = phone.PhoneId;
            this.PhoneNumber = phone.PhoneNumber;
            this.PhoneType = phone.PhoneType;
            this.Extension = phone.Extension;
        }
        public long PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string Extension { get; set; }
    }
        public class EmailView {
        public EmailView() { }
        public EmailView(Email email)
        {
            this.EmailId = email.EmailId;
            this.EmailText = email.Email1;
        }
        public long EmailId { get; set; }
 
        public string EmailText { get; set; }
    }
    public class CustomerRepository:Repository<Customer>
    {
        private ApplicationViewFactory applicationViewFactory;

        Entities _dbContext;
        public CustomerRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public IList<InvoiceView> GetInvoicesByCustomerId(int customerId, int? invoiceId=null)
        {
            IEnumerable<Invoice> invoiceList = null;
            var resultList = base.GetObjectsAsync(e => e.CustomerId == customerId, "invoices").FirstOrDefault();

            IList<InvoiceView> list = new List<InvoiceView>();
            if (invoiceId != null)
            {
                invoiceList = resultList.Invoices.Where(f => f.InvoiceId == invoiceId);
            }
            else
            {
                invoiceList = resultList.Invoices;
            }
            
            foreach (var item in invoiceList)
            {
                list.Add(applicationViewFactory.MapInvoiceView(item));
            }
            return list;

        }

        public IList<CustomerClaimView> GetCustomerClaimsByCustomerId(int customerId)
        {
            var resultList = base.GetObjectsAsync(e => e.CustomerId == customerId, "customerclaims").FirstOrDefault();

            IList<CustomerClaimView> list = new List<CustomerClaimView>();
            foreach (var item in resultList.CustomerClaims)
            {
                list.Add(applicationViewFactory.MapCustomerClaimView(item));
            }
            return list;
        }
        public IList<ScheduleEventView> GetScheduleEventsByCustomerId(int customerId,int? serviceId=null)
        {
            IEnumerable<ScheduleEvent> scheduleEventList = null;
            var resultList = base.GetObjectsAsync(e => e.CustomerId == customerId, "scheduleEvents").FirstOrDefault();

            IList<ScheduleEventView> list = new List<ScheduleEventView>();
            if (serviceId != null)
            {
                scheduleEventList = resultList.ScheduleEvents.Where(f => f.ServiceId== serviceId);
            }
            else
            {
                scheduleEventList = resultList.ScheduleEvents;
            }

            foreach (var item in scheduleEventList)
            {
                Contract contract= item.ServiceInformation.Contract;

                long? locationId = item.ServiceInformation.LocationId;

                Task<LocationAddress> locationAddressTask=

                   (from e in _dbContext.LocationAddresses
                                    where e.LocationId == locationId
                                    select e).FirstOrDefaultAsync<LocationAddress>();

                Task.WaitAny(locationAddressTask);
                ScheduleEventView scheduleEventView = applicationViewFactory.MapScheduleEventView(item);
                if (contract != null)
                {
                    scheduleEventView.ContractView = applicationViewFactory.MapContractView(contract);
                }
                if (locationAddressTask.Result !=null)
                {
                    scheduleEventView.LocationAddressView = applicationViewFactory.MapLocationAddressView(locationAddressTask.Result);
                }
                list.Add(scheduleEventView);
            }
            return list;
        }
        public IList<ContractView> GetContractsByCustomerId(int customerId, int? contractId = null)
        {
            IEnumerable<Contract> contractList = null;
            var resultList = base.GetObjectsAsync(e => e.CustomerId == customerId, "contracts").FirstOrDefault();

            IList<ContractView> list = new List<ContractView>();
            if (contractId != null)
            {
                contractList = resultList.Contracts.Where(f => f.ContractId == contractId);
            }
            else
            {
                contractList = resultList.Contracts;
            }

            foreach (var item in contractList)
            {
                list.Add(applicationViewFactory.MapContractView(item));
            }
            return list;
        }
   
        public IList<LocationAddressView> GetLocationAddressByCustomerId(int customerId)
        {
            Task<Customer> customerTask = base.GetObjectAsync(customerId);

            long addressId = customerTask.Result.AddressId;


            Task<List<LocationAddress>> locationAddressTask = 

               (from e in _dbContext.LocationAddresses
                where e.AddressId==addressId
                select e).ToListAsync<LocationAddress>();


            IList<LocationAddressView> list = new List<LocationAddressView>();
            foreach (var item in locationAddressTask.Result)
            {
                list.Add(applicationViewFactory.MapLocationAddressView(item));
            }
            return list;


        }
        
     public IList<PhoneView> GetPhonesByCustomerId(int customerId)
        {
            Task<Customer> customerTask = base.GetObjectAsync(customerId);

            long addressId = customerTask.Result.AddressId;

            Task<List<Phone>> phoneTask =

             (from e in _dbContext.Phones
              where e.AddressId == addressId
              select e).ToListAsync<Phone>();


            IList<PhoneView> list = new List<PhoneView>();
            foreach (var item in phoneTask.Result)
            {
                list.Add(applicationViewFactory.MapPhoneView(item));
            }
            return list;

        }
     public IList<EmailView> GetEmailsByCustomerId(int customerId)
     {
            Task<Customer> customerTask = base.GetObjectAsync(customerId);

            long addressId = customerTask.Result.AddressId;
            Task<List<Email>> emailTask =

            (from e in _dbContext.Emails
             where e.AddressId == addressId
             select e).ToListAsync<Email>();


            IList<EmailView> list = new List<EmailView>();
            foreach (var item in emailTask.Result)
            {
                list.Add(applicationViewFactory.MapEmailView(item));
            }
            return list;

        }
 
        /*
        public IList<PurchaseOrderView> GetPurchaseOrdersByCustomerId(int customerId)
        { }
        public IList<AccountsReceiveableView> GetAccountsReceivablesByCustomerId(int customerId)
        { }
        public IList<SalesOrderView> GetSalesOrdersByCustomerId(int customerId)
        { }
        
        public IList<ShipmentView> GetShipmentsByCustomerId(int customerId)
        { }
        */
    }
}
