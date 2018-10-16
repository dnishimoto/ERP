using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.InvoiceDetailsDomain;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.InvoicesDomain
{
    public class InvoiceView
    {
        public InvoiceView()
        {
            InvoiceDetailViews = new List<InvoiceDetailView>();
        }
        public InvoiceView(Invoice invoice)
        {
            this.InvoiceId = invoice.InvoiceId;
            this.InvoiceNumber = invoice.InvoiceNumber;
            this.InvoiceDate = invoice.InvoiceDate;
            this.Amount = invoice.Amount;
            this.CustomerId = invoice.Customer.CustomerId;
            this.CustomerName = invoice.Customer.Address.Name;
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
    public class InvoiceFlatView
    {
        public long? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public string InvoiceDescription { get; set; }
        public decimal? InvoiceTaxAmount { get; set; }
        public DateTime? InvoicePaymentDueDate { get; set; }
        public string InvoicePaymentTerms { get; set; }
        public long IM_ItemId { get; set; }
        public string IM_ItemNumber { get; set; }
        public string IM_Description { get; set; }
        public decimal? ID_UnitPrice { get; set; }
        public string ID_UnitOfMeasure { get; set; }
        public int? ID_Quantity { get; set; }
        public decimal? ID_Amount { get; set; }
        public decimal? ID_DiscountPercent { get; set; }
        public decimal? ID_DiscountAmount { get; set; }
        public string ID_ExtendedDescription { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public string CustomerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string AddressType { get; set; }

    }
    public class InvoiceRepository : Repository<Invoice>
    {
        public ListensoftwareDBContext _dbContext;
        public ApplicationViewFactory applicationViewFactory;
        public InvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate)
        {
            try
            { 
            List<InvoiceFlatView> list = (from invoice in _dbContext.Invoice
                        join invoiceDetail in _dbContext.InvoiceDetail
                        on invoice.InvoiceId equals invoiceDetail.InvoiceId
                        join itemMaster in _dbContext.ItemMaster
                        on invoiceDetail.ItemId equals itemMaster.ItemId
                        join company in _dbContext.Company
                        on invoice.CompanyId equals company.CompanyId
                        join customer in _dbContext.Customer
                        on invoice.CustomerId equals customer.CustomerId
                        join customerAddressBook in _dbContext.AddressBook
                        on customer.AddressId equals customerAddressBook.AddressId
                        join customerLocationAddress in _dbContext.LocationAddress
                        on customer.AddressId equals customerLocationAddress.AddressId into ljCustomerLocationAddress
                        from customerLocationAddress in ljCustomerLocationAddress.DefaultIfEmpty()
                        join udcType in _dbContext.Udc
                        on customerLocationAddress.TypeXrefId equals udcType.XrefId
                        where customerLocationAddress.TypeXrefId == 60
                        && invoice.InvoiceDate >= startInvoiceDate 
                        && invoice.InvoiceDate <=endInvoiceDate
                        select new InvoiceFlatView
                        {
                     
                            InvoiceId = invoice.InvoiceId,
                            InvoiceNumber = invoice.InvoiceNumber,
                            InvoiceDate = invoice.InvoiceDate,
                            InvoiceAmount = invoice.Amount,
                            InvoiceDescription = invoice.Description,
                            InvoiceTaxAmount = invoice.TaxAmount,
                            InvoicePaymentDueDate = invoice.PaymentDueDate,
                            InvoicePaymentTerms = invoice.PaymentTerms,
                            IM_ItemId = itemMaster.ItemId,
                            IM_ItemNumber = itemMaster.ItemNumber,
                            IM_Description = itemMaster.Description,
                            ID_UnitPrice = invoiceDetail.UnitPrice,
                            ID_UnitOfMeasure= invoiceDetail.UnitOfMeasure,
                            ID_Quantity = invoiceDetail.Quantity,
                            ID_Amount = invoiceDetail.Amount,
                            ID_DiscountPercent = invoiceDetail.DiscountPercent,
                            ID_DiscountAmount = invoiceDetail.DiscountAmount,
                            ID_ExtendedDescription = invoiceDetail.ExtendedDescription,
                            CompanyId = invoice.CompanyId,
                            CompanyName = company.CompanyName,
                            CompanyCode = company.CompanyCode,
                            CompanyStreet = company.CompanyStreet,
                            CompanyCity = company.CompanyCity,
                            CompanyState = company.CompanyState,
                            CompanyZipcode = company.CompanyZipcode,
                            CustomerName = customerAddressBook.Name,

                            AddressLine1 = customerLocationAddress.AddressLine1,
                            AddressLine2 = customerLocationAddress.AddressLine2,
                            City = customerLocationAddress.City,
                            State = customerLocationAddress.State,
                            Zipcode = customerLocationAddress.Zipcode,
                            AddressType = udcType.Value
                       }).ToList<InvoiceFlatView>();

                return list;
             }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Invoice> GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                Invoice invoice = await (from a in _dbContext.Invoice
                                         where a.InvoiceNumber == invoiceNumber
                                         select a).FirstOrDefaultAsync<Invoice>();
                return invoice;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<CreateProcessStatus> CreateInvoiceByView(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoice = new Invoice();

                applicationViewFactory.MapInvoiceEntity(ref invoice, invoiceView);

                var query = await (from a in _dbContext.Invoice
                                   where a.InvoiceNumber == invoice.InvoiceNumber
                                   select a).FirstOrDefaultAsync<Invoice>();
                if (query == null)
                {
                    AddObject(invoice);
                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<bool> AddInvoice(Invoice invoice)
        {

            try
            {
                var query = await (from a in _dbContext.Invoice
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
