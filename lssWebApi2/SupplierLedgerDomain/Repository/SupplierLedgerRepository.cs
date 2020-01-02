

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.SupplierLedgerDomain
{

    public class SupplierLedgerView
    {
        public long SupplierLedgerId { get; set; }
        public long SupplierId { get; set; }
        public long InvoiceId { get; set; }
        public long AcctPayId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Gldate { get; set; }
        public long AccountId { get; set; }
        public long GeneralLedgerId { get; set; }
        public long DocNumber { get; set; }
        public string Comment { get; set; }
        public long AddressId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DocType { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public int FiscalYear { get; set; }
        public int FiscalPeriod { get; set; }
        public long SupplierLedgerNumber { get; set; }
    }
    public class SupplierLedgerRepository : Repository<SupplierLedger>, ISupplierLedgerRepository
    {
        public ListensoftwaredbContext _dbContext;
        public SupplierLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<SupplierLedger> GetEntityByDocNumber(long? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.SupplierLedger
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<SupplierLedger>();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<SupplierLedger> GetEntityByView(SupplierLedgerView view)
        {
            var query = await (from e in _dbContext.SupplierLedger
                               where e.AccountId == view.AccountId
                               && e.Amount == view.Amount
                               && e.Gldate == view.Gldate
                               && e.DocNumber == view.DocNumber
                               select e
                            ).FirstOrDefaultAsync<SupplierLedger>();
            return query;

        }
        public async Task<SupplierLedger> GetEntityById(long ? supplierLedgerId)
        {
            try
            {
                return await _dbContext.FindAsync<SupplierLedger>(supplierLedgerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierLedger> GetEntityByNumber(long supplierLedgerNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.SupplierLedger
                                   where detail.SupplierLedgerNumber == supplierLedgerNumber
                                   select detail).FirstOrDefaultAsync<SupplierLedger>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
      

    }
}
