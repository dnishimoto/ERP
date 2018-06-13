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
    public class CustomerRepository:Repository<Customer>
    {
        private ApplicationViewFactory applicationViewFactory;

        Entities _dbContext;
        public CustomerRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public IList<InvoiceView> GetInvoicesByCustomerId(int customerId, Expression<Func<Customer, bool>> customerPredicate, int? invoiceId)
        {
            IEnumerable<Invoice> invoiceList = null;
            var resultList = base.GetObjectsAsync(customerPredicate, "invoices").FirstOrDefault();

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
        /*
        public IList<PurchaseOrderView> GetPurchaseOrdersByCustomerId(int customerId)
        { }
        public IList<AccountsReceiveableView> GetAccountsReceivablesByCustomerId(int customerId)
        { }
        public IList<ContractView> GetContractsByCustomerId(int customerId)
        { }
        public IList<SalesOrderView> GetSalesOrdersByCustomerId(int customerId)
        { }
        public IList<ScheduleEventView> GetScheduleEventsByCustomerId(int customerId)
        { }
        public IList<ShipmentView> GetShipmentsByCustomerId(int customerId)
        { }
        */
    }
}
