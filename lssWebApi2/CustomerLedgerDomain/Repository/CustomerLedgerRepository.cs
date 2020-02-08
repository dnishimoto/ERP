using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using System.Data.SqlClient;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.GeneralLedgerDomain.Repository;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections;

namespace lssWebApi2.CustomerLedgerDomain
{
    public class CustomerLedgerView
    {
        public long CustomerId { get; set; }
        public long GeneralLedgerId { get; set; }
        public long InvoiceId { get; set; }
        public long AccountReceivableId { get; set; }
        public long CustomerLedgerId { get; set; }
        public long DocNumber { get; set; }
        public string DocType { get; set; }
        public decimal Amount { get; set; }
        public string LedgerType { get; set; }
        public DateTime GLDate { get; set; }
        public long AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AddressId { get; set; }
        public string Comment { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public int FiscalPeriod { get; set; }
        public int FiscalYear { get; set; }
        public string CheckNumber { get; set; }
        public long CustomerLedgerNumber { get; set; }

        public string CustomerName { get; set; }
        public string InvoiceDocument { get; set; }
        public string Account { get; set; }
        public string AccountDescription { get; set; }
    }

    public class CustomerLedgerRepository : Repository<CustomerLedger>, ICustomerLedgerRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public CustomerLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<IList<CustomerLedger>> GetEntitiesByCustomerId(long customerId)
        {
            IList<CustomerLedger> list = await (from detail in _dbContext.CustomerLedger
                                                where detail.CustomerId == customerId
                                                select detail).ToListAsync<CustomerLedger>();
            return list;
        }
        public async Task<CustomerLedger> GetEntityById(long ? customerLedgerId)
        {
            try
            {
                return await _dbContext.FindAsync<CustomerLedger>(customerLedgerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CustomerLedger> GetEntityByNumber(long customerLedgerNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.CustomerLedger
                                   where detail.CustomerLedgerNumber == customerLedgerNumber
                                   select detail).FirstOrDefaultAsync<CustomerLedger>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
      

    public async Task<IList<CustomerLedger>> GetCustomerLedgersByCustomerId(long customerId)
        {
            try
            {
                List<CustomerLedger>list = await(from e in _dbContext.CustomerLedger
                                  where e.CustomerId==customerId
                                  select e
                                  ).ToListAsync<CustomerLedger>();
               
                              
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateBalanceByAccountId(long? accountId, int? fiscalYear, int? fiscalPeriod)
        {

            try
            {
                SqlParameter param1 = new SqlParameter("@AccountId", accountId);
                SqlParameter param2 = new SqlParameter("@FiscalPeriod", fiscalPeriod);
                SqlParameter param3 = new SqlParameter("@FiscalYear", fiscalYear);
                //params Object[] parameters;


                var result = await _dbContext.Database.ExecuteSqlCommandAsync("usp_RollupCustomerLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear", param1, param2, param3);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CustomerLedger> FindEntityByGeneralLedgerId(long? generalLedgerId)
        {
            var query = await (from e in _dbContext.CustomerLedger
                               where e.GeneralLedgerId == generalLedgerId


                               select e
                 ).FirstOrDefaultAsync<CustomerLedger>();
            return query;
        }

        public async Task<CustomerLedger> GetCustomerLedgerByDocNumber(long ? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.CustomerLedger
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<CustomerLedger>();

                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
      

      
    }
}
