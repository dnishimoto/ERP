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

namespace MillenniumERP.InvoicesDomain
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
    public class InvoiceRepository: Repository<Invoice>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public InvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<bool> AddInvoice(Invoice invoice)
        {
                    
            var query = await (from a in _dbContext.Invoices
                         where a.InvoiceNumber==invoice.InvoiceNumber
                         select a).FirstOrDefaultAsync<Invoice>();
            if (query == null)
            {
                AddObject(invoice);
            }
           
            
            return true;
        }
        public async Task<bool> UpdateInvoice(Invoice invoice)
        {
            try
            {
                var query = GetObjectAsync((int)invoice.InvoiceId);

                Invoice invoiceBase = query.Result;

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
                
            }
            return false;
            }
        public async Task<bool> DeleteInvoice(Invoice invoice)
        {
            try
            {
                DeleteObject(invoice);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
