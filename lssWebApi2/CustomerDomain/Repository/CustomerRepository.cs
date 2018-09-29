using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.CustomerDomain
{

    public class CustomerClaimView
    {
        public CustomerClaimView() { }
        public CustomerClaimView(CustomerClaim customerClaim)
        {
            this.ClaimId = customerClaim.ClaimId;
            this.Classification = customerClaim.ClassificationXref.Value;
            this.CustomerId = customerClaim.CustomerId;
            this.CustomerName = customerClaim.Customer.Address.Name;
            this.Configuration = customerClaim.Configuration;
            this.Note = customerClaim.Note;
            this.EmployeeName = customerClaim.Employee.Address.Name;
            this.GroupId = customerClaim.GroupIdXref.Value;
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
   
  
    public class ScheduleEventView
    {
        public ScheduleEventView() { }
        public ScheduleEventView(ScheduleEvent scheduleEvent)
        {
            this.ScheduleEventId = scheduleEvent.ScheduleEventId;
            this.EmployeeId = scheduleEvent.EmployeeId;
            this.EmployeeName = scheduleEvent.Employee.Address.Name;
            this.EventDateTime = scheduleEvent.EventDateTime;
            this.ServiceId = scheduleEvent.ServiceId;
            this.DurationMinutes = scheduleEvent.DurationMinutes;
            this.CustomerId = scheduleEvent.CustomerId;
            this.CustomerName = scheduleEvent.Customer.Address.Name;
            this.ServiceDescription = scheduleEvent.Service.ServiceDescription;
            this.Price = scheduleEvent.Service.Price;
            this.AddOns = scheduleEvent.Service.AddOns;
            this.ServiceTypeXRefId = scheduleEvent.Service.ServiceTypeXrefId;
            this.ServiceType = scheduleEvent.Service.ServiceTypeXref.Value;
            this.CreatedDate = scheduleEvent.Service.CreatedDate;

            this.SquareFeetOfStructure = scheduleEvent.Service.SquareFeetOfStructure;
            this.LocationDescription = scheduleEvent.Service.LocationDescription;
            this.LocationGPS = scheduleEvent.Service.LocationGps;
            this.Comments = scheduleEvent.Service.Comments;


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
  
    public class ContractView
    {
        public ContractView() { }
        public ContractView(Contract contract)
        {
            this.ContractId = contract.ContractId;
            this.CustomerId = contract.CustomerId;
            this.CustomerName = contract.Customer.Address.Name;
            this.ServiceTypeXRefId = contract.ServiceTypeXrefId;
            this.ServiceType = contract.ServiceTypeXref.Value;
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
        public PhoneView(Phones phone)
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
   
    public class CustomerView
    {
        public CustomerView() {
            //LocationAddress = new List<LocationAddressView>(); 
                }
        public CustomerView(Customer customer)
        {
            this.AddressId = customer.AddressId;
            this.CustomerName = customer.Address.Name;
            this.FirstName = customer.Address.FirstName;
            this.LastName = customer.Address.LastName;
         }
        public long AddressId { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public IList<LocationAddressView> LocationAddress { get; set; }
        public EmailView AccountEmail { get; set; }
    }
    public class CustomerRepository : Repository<Customer>
    {
        private ApplicationViewFactory applicationViewFactory;

        ListensoftwareDBContext _dbContext;
        public CustomerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateCustomer(CustomerView customerView)
        {
            try
            {
                var query = await (from e in _dbContext.Customer
                                   where e.AddressId == customerView.AddressId
                                   select e).FirstOrDefaultAsync<Customer>();
                if (query == null)
                {
                    Customer customer = new Customer();
                    applicationViewFactory.MapCustomerEntity(ref customer, customerView);
                    AddObject(customer);
                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;
                
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<CustomerLedgerView> GetCustomerLedgersByCustomerId(long customerId)
        {
            try
            {
                IEnumerable<CustomerLedger> ledgerList = null;
                var resultList = base.GetObjectsQueryable(e => e.CustomerId == customerId, "customerledgers").FirstOrDefault();
                IList<CustomerLedgerView> list = new List<CustomerLedgerView>();

                ledgerList = resultList.CustomerLedger;

                foreach (var item in ledgerList)
                {
                    list.Add(applicationViewFactory.MapCustomerLedgerView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<InvoiceView> GetInvoicesByCustomerId(long customerId, long? invoiceId = null)
        {
            try
            {
                IEnumerable<Invoice> invoiceList = null;
                var resultList = base.GetObjectsQueryable(e => e.CustomerId == customerId, "invoices").FirstOrDefault();

                IList<InvoiceView> list = new List<InvoiceView>();
                if (invoiceId != null)
                {
                    invoiceList = resultList.Invoice.Where(f => f.InvoiceId == invoiceId);
                }
                else
                {
                    invoiceList = resultList.Invoice;
                }

                foreach (var item in invoiceList)
                {
                    list.Add(applicationViewFactory.MapInvoiceView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }

        public IList<CustomerClaimView> GetCustomerClaimsByCustomerId(long customerId)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(e => e.CustomerId == customerId, "customerclaims").FirstOrDefault();

                IList<CustomerClaimView> list = new List<CustomerClaimView>();
                foreach (var item in resultList.CustomerClaim)
                {
                    list.Add(applicationViewFactory.MapCustomerClaimView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ScheduleEventView> GetScheduleEventsByCustomerId(long customerId, long? serviceId = null)
        {
            try
            {
                IEnumerable<ScheduleEvent> scheduleEventList = null;
                var resultList = base.GetObjectsQueryable(e => e.CustomerId == customerId, "scheduleEvents").FirstOrDefault();

            
                IList<ScheduleEventView> list = new List<ScheduleEventView>();
                if (serviceId != null)
                {
                    scheduleEventList = resultList.ScheduleEvent.Where(f => f.ServiceId == serviceId);
                }
                else
                {
                    scheduleEventList = resultList.ScheduleEvent;
                }

                foreach (var item in scheduleEventList)
                {
                    Contract contract = item.Service.Contract;

                    long? locationId = item.Service.LocationId;

                    Task<LocationAddress> locationAddressTask =

                       (from e in _dbContext.LocationAddress
                        where e.LocationId == locationId
                        select e).FirstOrDefaultAsync<LocationAddress>();

                    Task.WaitAny(locationAddressTask);
                    ScheduleEventView scheduleEventView = applicationViewFactory.MapScheduleEventView(item);
                    if (contract != null)
                    {
                        scheduleEventView.ContractView = applicationViewFactory.MapContractView(contract);
                    }
                    if (locationAddressTask.Result != null)
                    {
                        scheduleEventView.LocationAddressView = applicationViewFactory.MapLocationAddressView(locationAddressTask.Result);
                    }
                    list.Add(scheduleEventView);
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ContractView> GetContractsByCustomerId(long customerId, long? contractId = null)
        {
            try
            {
                IEnumerable<Contract> contractList = null;
                var resultList = base.GetObjectsQueryable(e => e.CustomerId == customerId, "contracts").FirstOrDefault();

                IList<ContractView> list = new List<ContractView>();
                if (contractId != null)
                {
                    contractList = resultList.Contract.Where(f => f.ContractId == contractId);
                }
                else
                {
                    contractList = resultList.Contract;
                }

                foreach (var item in contractList)
                {
                    list.Add(applicationViewFactory.MapContractView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<LocationAddressView> GetLocationAddressByCustomerId(long customerId)
        {
            try
            {
                Task<Customer> customerTask = base.GetObjectAsync(customerId);

                long addressId = customerTask.Result.AddressId;


                Task<List<LocationAddress>> locationAddressTask =

                   (from e in _dbContext.LocationAddress
                    where e.AddressId == addressId
                    select e).ToListAsync<LocationAddress>();

                Task.WaitAll(customerTask, locationAddressTask);

                IList<LocationAddressView> list = new List<LocationAddressView>();
                foreach (var item in locationAddressTask.Result)
                {
                    list.Add(applicationViewFactory.MapLocationAddressView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }

        public IList<PhoneView> GetPhonesByCustomerId(long customerId)
        {
            try
            {
                Task<Customer> customerTask = base.GetObjectAsync(customerId);

                long addressId = customerTask.Result.AddressId;

                Task<List<Phones>> phoneTask =

                 (from e in _dbContext.Phones
                  where e.AddressId == addressId
                  select e).ToListAsync<Phones>();

                Task.WaitAll(customerTask, phoneTask);

                IList<PhoneView> list = new List<PhoneView>();
                foreach (var item in phoneTask.Result)
                {
                    list.Add(applicationViewFactory.MapPhoneView(item));
                }
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public IList<EmailView> GetEmailsByCustomerId(long customerId)
        {
            try
            {
                Task<Customer> customerTask = base.GetObjectAsync(customerId);
            

                long addressId = customerTask.Result.AddressId;
                Task<List<Emails>> emailTask =

                (from e in _dbContext.Emails
                 where e.AddressId == addressId
                 select e).ToListAsync<Emails>();

                Task.WaitAll(emailTask, customerTask);


                IList<EmailView> list = new List<EmailView>();
                foreach (var item in emailTask.Result)
                {
                    list.Add(applicationViewFactory.MapEmailView(item));
                }
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public IList<AccountReceiveableView> GetAccountReceivablesByCustomerId(long customerId)
        {
            try
            {
                //IEnumerable<AcctRec> acctRecList = null;
                var query =

                              (from e in _dbContext.AcctRec
                               where e.CustomerId == customerId
                               && e.OpenAmount > 0
                               select e);

                //Task.WaitAny(acctRecTask);

                //acctRecList = acctRecTask.;

                IList<AccountReceiveableView> list = new List<AccountReceiveableView>();

                foreach (var item in query)
                {
                    list.Add(applicationViewFactory.MapAccountReceivableView(item));
                }
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        /*
        public IList<PurchaseOrderView> GetPurchaseOrdersByCustomerId(int customerId)
        { }
        
        public IList<SalesOrderView> GetSalesOrdersByCustomerId(int customerId)
        { }
        
        public IList<ShipmentView> GetShipmentsByCustomerId(int customerId)
        { }
        */
    }
}
