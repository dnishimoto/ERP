

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{
    public class SupplierInvoiceDetailView

    {
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public long? ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public long? SupplierInvoiceId { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public long? SupplierInvoiceDetailId { get; set; }
        public string Description { get; set; }
        public long? InvoiceId { get; set; }
        public long? InvoiceDetailId { get; set; }
        public long SupplierInvoiceDetailNumber { get; set; }

        public string InvoiceDocument { get; set; }
    }
    public class SupplierInvoiceDetailRepository : Repository<SupplierInvoiceDetail>, ISupplierInvoiceDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public SupplierInvoiceDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<SupplierInvoiceDetail>> getEntitiesByInvoiceId(long? supplierInvoiceId)
        {
            try
            {
                List<SupplierInvoiceDetail> list = await (from detail in _dbContext.SupplierInvoiceDetail
                                   where detail.SupplierInvoiceId == supplierInvoiceId
                                   select detail).ToListAsync<SupplierInvoiceDetail>();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierInvoiceDetail> GetEntityById(long ? supplierInvoiceDetailId)
        {
            try
            {
                return await _dbContext.FindAsync<SupplierInvoiceDetail>(supplierInvoiceDetailId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierInvoiceDetail> GetEntityByNumber(long supplierInvoiceDetailNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.SupplierInvoiceDetail
                                   where detail.SupplierInvoiceDetailNumber == supplierInvoiceDetailNumber
                                   select detail).FirstOrDefaultAsync<SupplierInvoiceDetail>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
      


    }
}
