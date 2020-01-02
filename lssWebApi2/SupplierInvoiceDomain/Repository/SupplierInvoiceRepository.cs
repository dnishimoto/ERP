

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.SupplierInvoiceDetailDomain;

namespace lssWebApi2.SupplierInvoiceDomain
{
    public class SupplierInvoiceView
    {
        public long? SupplierInvoiceId { get; set; }
        public long SupplierInvoiceNumber { get; set; }
        public DateTime? SupplierInvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? FreightCost { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string PaymentTerms { get; set; }
        public string PONumber { get; set; }
        public long? SupplierId { get; set; }
        public string InvoiceDocument { get; set; }
        public long ? PurchaseOrderId { get; set; }
        public long ? InvoiceId { get; set; }
        public IList<SupplierInvoiceDetailView> SupplierInvoiceDetailViews { get; set; }


        public string SupplierName { get; set; }

    }
    public class SupplierInvoiceRepository : Repository<SupplierInvoice>, ISupplierInvoiceRepository
    {
        ListensoftwaredbContext _dbContext;
        public SupplierInvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<SupplierInvoice> GetEntityById(long ? supplierInvoiceId)
        {
            try
            {
                return await _dbContext.FindAsync<SupplierInvoice>(supplierInvoiceId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierInvoice> GetEntityByPONumber(string poNumber)
        {
            try
            {
                var query = await (from e in _dbContext.SupplierInvoice
                                   where e.Ponumber == poNumber

                                   select e).FirstOrDefaultAsync<SupplierInvoice>();

                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierInvoice> GetEntityByNumber(long ? supplierInvoiceNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.SupplierInvoice
                                   where detail.SupplierInvoiceNumber == supplierInvoiceNumber
                                   select detail).FirstOrDefaultAsync<SupplierInvoice>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
       


    }
}
