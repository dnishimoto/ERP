using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.InvoiceDomain.Repository;
using System.Linq.Expressions;
using lssWebApi2.InvoiceDetailDomain;

namespace lssWebApi2.InvoicesDomain
{
    public class InvoiceView
    {
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceDocument { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public long? CustomerId { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FreightCost { get; set; }
        public string PaymentTerms { get; set; }
        public long InvoiceNumber { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? SupplierId { get; set; }
        public long TaxRatesByCodeId { get; set; }
        public IList<InvoiceDetailView> InvoiceDetailViews { get; set; }

        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public string TaxCode { get; set; }
    }
    public class InvoiceFlatView
    {
        public long? InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public string InvoiceDescription { get; set; }
        public decimal? InvoiceTaxAmount { get; set; }
        public DateTime? InvoicePaymentDueDate { get; set; }
        public string InvoicePaymentTerms { get; set; }
        public long IM_ItemId { get; set; }
        public string IM_ItemCode { get; set; }
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
        public long InvoiceNumber { get; set; }


    }
    public class InvoiceRepository : Repository<Invoice>,IInvoiceRepository
    {
        public ListensoftwaredbContext _dbContext;
        public ApplicationViewFactory applicationViewFactory;
        public InvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<Decimal> GetInvoicedAmountByPurchaseOrderId(long? purchaseOrderId)
        {
           var query = await (from detail in _dbContext.Invoice
                               where detail.PurchaseOrderId == purchaseOrderId
                               select detail).ToListAsync<Invoice>();

            decimal invoiceAmount = query.Sum(e => e.Amount)??0;

            return invoiceAmount;
        }
        public async Task<IList<Invoice>> GetEntitiesByPurchaseOrderId(long? purchaseOrderId)
        {
            var query = await (from detail in _dbContext.Invoice
                               where detail.PurchaseOrderId == purchaseOrderId
                               select detail).ToListAsync<Invoice>();
            return query;
        }
        public async Task<IQueryable<Invoice>> GetQueryableByCustomerId(long ? customerId)
        {
            var query = (from invoice in _dbContext.Invoice
                         join customer in _dbContext.Customer
                                   on invoice.CustomerId equals customer.CustomerId
                         where customer.CustomerId == customerId
                         select invoice);
            await Task.Yield();
            return query;
        }
        public async Task<Invoice> GetEntityById(long ? invoiceId)
        {
            try
            {
                return await _dbContext.FindAsync<Invoice>(invoiceId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Invoice> GetEntityByNumber(long ?invoiceNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.Invoice
                                   where detail.InvoiceNumber == invoiceNumber
                                   select detail).FirstOrDefaultAsync<Invoice>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<Invoice>> GetEntitiesByExpression(Expression<Func<Invoice, bool>> predicate)
        {
            return await (_dbContext.Set<Invoice>().Where(predicate)).ToListAsync<Invoice>();

        }
        public async Task<Invoice> FindEntityByExpression(Expression<Func<Invoice, bool>> predicate)
        {
            IQueryable<Invoice> result = _dbContext.Set<Invoice>().Where(predicate);

            return await result.FirstOrDefaultAsync<Invoice>();
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
                            IM_ItemCode = itemMaster.ItemCode,
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
        public async Task<Invoice> GetEntityByInvoiceDocument(string invoiceDocument)
        {
            try
            {
                Invoice invoice = await (from a in _dbContext.Invoice
                                         where a.InvoiceDocument == invoiceDocument
                                         select a).FirstOrDefaultAsync<Invoice>();
                return invoice;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<Invoice> FindEntityByInvoiceDocument(string invoiceDocument)
        {
            var query = await (from a in _dbContext.Invoice
                               where a.InvoiceDocument == invoiceDocument
                               select a).FirstOrDefaultAsync<Invoice>();
            return query;

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
