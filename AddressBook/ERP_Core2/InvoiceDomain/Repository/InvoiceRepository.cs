using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;
using ERP_Core2.AbstractFactory;
using System.Collections;
using MillenniumERP.InvoiceDetailsDomain;

namespace MillenniumERP.InvoicesDomain
{
    public class InvoiceView
    {
        public InvoiceView() {
            InvoiceDetailViews=new List<InvoiceDetailView>();
        }
        public InvoiceView(Invoice invoice)
        {
            this.InvoiceId = invoice.InvoiceId;
            this.InvoiceNumber = invoice.InvoiceNumber;
            this.InvoiceDate = invoice.InvoiceDate;
            this.Amount = invoice.Amount;
            this.CustomerId = invoice.Customer.CustomerId;
            this.CustomerName = invoice.Customer.AddressBook.Name;
            this.Description = invoice.Description;
            this.TaxAmount = invoice.TaxAmount;
            this.PaymentDueDate = invoice.PaymentDueDate;
            this.DiscountAmount = invoice.DiscountAmount;
            this.PaymentTerms = invoice.PaymentTerms;
            this.CompanyId = invoice.Company.CompanyId;
            this.CompanyName = invoice.Company.CompanyName;
            this.CompanyStreet = invoice.Company.CompanyStreet;
            this.CompanyCity = invoice.Company.CompanyCity;
            this.CompanyZipcode = invoice.Company.CompanyZipcode;
            this.DiscountDueDate = invoice.DiscountDueDate;
            this.FreightCost = invoice.FreightCost;
            InvoiceDetailViews = new List<InvoiceDetailView>();

        }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FreightCost { get; set; }
        public string PaymentTerms { get; set; }
        public IList<InvoiceDetailView> InvoiceDetailViews { get; set; }
    }
   
    public class InvoiceRepository: Repository<Invoice>
    {
        public Entities _dbContext;
        public ApplicationViewFactory applicationViewFactory;
        public InvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<bool> AddInvoice(Invoice invoice)
        {
         
            try
            {
                var query = await (from a in _dbContext.Invoices
                                   where a.InvoiceNumber == invoice.InvoiceNumber
                                   select a).FirstOrDefaultAsync<Invoice>();
                if (query == null)
                {
                    AddObject(invoice);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateInvoice(Invoice invoice)
        {
            try
            {
                var query = await GetObjectAsync(invoice.InvoiceId);

                Invoice invoiceBase = query;

                invoiceBase.Amount = invoice.Amount;
                invoiceBase.CompanyId = invoice.CompanyId;
                invoiceBase.CustomerId = invoice.CustomerId;
                invoiceBase.Description = invoice.Description;
                invoiceBase.InvoiceDate = invoice.InvoiceDate;
                invoiceBase.InvoiceNumber = invoice.InvoiceNumber;
                invoiceBase.PaymentDueDate = invoice.PaymentDueDate;
                invoiceBase.PaymentTerms = invoice.PaymentTerms;
                invoiceBase.TaxAmount = invoice.TaxAmount;
                
                UpdateObject(invoiceBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
            
            }
        public bool DeleteInvoice(Invoice invoice)
        {
            try
            {
                DeleteObject(invoice);
                //TODO delete all detail
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
          
        }
    }
}
