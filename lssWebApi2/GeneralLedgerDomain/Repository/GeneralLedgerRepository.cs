using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountsReceivableDomain;
using System.Data.SqlClient;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using lssWebApi2.GeneralLedgerDomain.Repository;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.GeneralLedgerDomain
{
    public class GeneralLedgerView
    {
        public long GeneralLedgerId { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public long? InvoiceId { get; set; }
        public long? AcctPayId { get; set; }
        public long DocNumber { get; set; }
        public string DocType { get; set; }
        public decimal Amount { get; set; }
        public decimal? Hours { get; set; }
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
        public string PurchaseOrderNumber { get; set; }
        public decimal? Units { get; set; }
    }
    public class IncomeView
    {

        public string Description { get; set; }
        public string Account { get; set; }
        public long AccountId { get; set; }
        public long GeneralLedgerId { get; set; }
        public string DocType { get; set; }
        public string LedgerType { get; set; }
        public long AddressId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime GLDate { get; set; }
        public int? FiscalPeriod { get; set; }
        public int? FiscalYear { get; set; }
    }


    public class IncomeShortView
    {
        public DateTime GLDate { get; set; }
        public String Comment { get; set; }
        public Decimal Amount { get; set; }
        public String CheckNumber { get; set; }
    }
    public class AccountSummaryView
    {

        private List<GeneralLedgerView> _ledgers = new List<GeneralLedgerView>();

        public long AccountId { get; set; }
        public int? FiscalPeriod { get; set; }
        public int? FiscalYear { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }

        public List<GeneralLedgerView> ledgers { get { return _ledgers; } }

    }
    public class AccountEntity
    {
        public string Account { get; set; }
        public string AccountId { get; set; }

    }
    public class IncomeStatementView
    {

        public string Account { get; set; }
        public string Description { get; set; }
        public int? FiscalPeriod { get; set; }
        public int? FiscalYear { get; set; }
        public decimal Amount { get; set; }
        public DateTime GLDate { get; set; }

    }


    public class GeneralLedgerRepository : Repository<GeneralLedger>, IGeneralLedgerRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public GeneralLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public IQueryable<GeneralLedger> GetEntitiesByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            var result = _dbContext.Set<GeneralLedger>().Where(predicate);

            return result;
        }

        public async Task<GeneralLedger> FindEntityByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            IQueryable<GeneralLedger> result = _dbContext.Set<GeneralLedger>().Where(predicate);

            return await result.FirstOrDefaultAsync<GeneralLedger>();
        }
        public async Task<IList<GeneralLedger>> FindEntitiesByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            IQueryable<GeneralLedger> result = _dbContext.Set<GeneralLedger>().Where(predicate);

            return await result.ToListAsync<GeneralLedger>();
        }
        public async Task<Decimal> GetGLAmountByDocNumber(long ? docNumber)

            {
            var gl_query = await (from e in _dbContext.GeneralLedger
                                      where e.DocNumber == docNumber
                                      && e.DocType == "PV"
                                      && e.LedgerType == "AA"

                                      group e by e.DocNumber
                                          into g

                                      select new { AmountPaid = g.Sum(e => e.Amount) }
                                          ).FirstOrDefaultAsync();
            return gl_query.AmountPaid;

}
        public async Task<GeneralLedger> GetEntityById(long? generalLedgerId)
        {
            try
            {
                return await _dbContext.FindAsync<GeneralLedger>(generalLedgerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<IList<IncomeStatementView>> GetIncomeStatementView(long fiscalYear)
        {
            IList<IncomeStatementView> list = await (from coa in _dbContext.ChartOfAccount
                                                    join gl in _dbContext.GeneralLedger
                                                    on coa.AccountId equals gl.AccountId into glLeftJoin
                                                    from gl in glLeftJoin.DefaultIfEmpty()
                                                    where (new string[] { "300", "310", "502" }).Contains(coa.ObjectNumber)
                                                    && gl.FiscalYear == fiscalYear
                                                    orderby coa.Account
                                                    select
                                                    (
                                                    new IncomeStatementView
                                                    {
                                                        Account = coa.Account,
                                                        Description = coa.Description,
                                                        FiscalPeriod = gl.FiscalPeriod == null ? DateTime.Now.Month : gl.FiscalPeriod,
                                                        FiscalYear = gl.FiscalYear == null ? DateTime.Now.Year : gl.FiscalYear,
                                                        Amount = gl.Amount,
                                                        GLDate = gl.Gldate == null ? DateTime.Now : gl.Gldate,
                                                  //select new { Amount = pg.Select(f => f.Amount).Sum() }


                                              })
            ).ToListAsync<IncomeStatementView>();

            return list;
        }
        public async Task<IList<IncomeView>> GetIncomeViews()
        {
            try
            {
                IList<IncomeView> list = await (from coa in _dbContext.ChartOfAccount
                                               join gl in _dbContext.GeneralLedger
                                                   on coa.AccountId equals gl.AccountId
                                               join ab in _dbContext.AddressBook
                                                   on gl.AddressId equals ab.AddressId
                                               where coa.BusUnit == "1200" && coa.ObjectNumber == "300"
                                               select new IncomeView
                                               {
                                                   Description = coa.Description,
                                                   Account = coa.Account,
                                                   AccountId = coa.AccountId,
                                                   GeneralLedgerId = gl.GeneralLedgerId,
                                                   DocType = gl.DocType,
                                                   LedgerType = gl.LedgerType,
                                                   AddressId = gl.AddressId,
                                                   Name = ab.Name,
                                                   Amount = gl.Amount,
                                                   GLDate = gl.Gldate,
                                                   FiscalPeriod = gl.FiscalPeriod,
                                                   FiscalYear = gl.FiscalYear
                                               }).ToListAsync<IncomeView>();

                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        private GeneralLedgerView MapToView(GeneralLedger inputObject)
        {
            Mapper mapper = new Mapper();
            GeneralLedgerView outObject = mapper.Map<GeneralLedgerView>(inputObject);

            return outObject;
        }
        public IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            try
            {
                var query = (from e in _dbContext.GeneralLedger
                             where (e.LedgerType == "AA" && e.DocType == "PV" && e.FiscalYear == fiscalYear)
                             group e by e.AccountId into pg

                             join f in _dbContext.ChartOfAccount
                                 on pg.Key equals f.AccountId

                             select new
                             {
                                 AccountId = pg.Key,
                                 FiscalPeriod = pg.Select(g => g.FiscalPeriod).FirstOrDefault(),
                                 FiscalYear = pg.Select(g => g.FiscalYear).FirstOrDefault(),
                                 Amount = pg.Select(g => g.Amount).Sum(),
                                 Description = f.Description,
                                 Ledgers = pg.ToList()
                             });

                IList<AccountSummaryView> list = new List<AccountSummaryView>();

                foreach (var item in query)
                {
                    AccountSummaryView view = new AccountSummaryView();
                    view.AccountId = item.AccountId;

                    view.FiscalPeriod = item.FiscalPeriod;
                    view.FiscalYear = item.FiscalYear;
                    view.Description = item.Description;
                    view.Amount = item.Amount;

                    foreach (var ledger in item.Ledgers.OrderByDescending(i => i.Gldate))
                    {
                        GeneralLedgerView glView = MapToView(ledger);
                        view.ledgers.Add(glView);
                    }

                    list.Add(view);
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
     
        public async Task<bool> UpdateBalanceByAccountId(long? accountId, int? fiscalYear, int? fiscalPeriod, string docType)
        {

            try
            {
                SqlParameter param1 = new SqlParameter("@AccountId", accountId);
                SqlParameter param2 = new SqlParameter("@FiscalPeriod", fiscalPeriod);
                SqlParameter param3 = new SqlParameter("@FiscalYear", fiscalYear);
                SqlParameter param4 = new SqlParameter("@DocType", docType);
                //params Object[] parameters;


                var result = await _dbContext.Database.ExecuteSqlCommandAsync("usp_RollupGeneralLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear, @DocType", param1, param2, param3, param4);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<GeneralLedger> GetEntityByDocNumber(long? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.GeneralLedger
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<GeneralLedger>();


                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<GeneralLedger> FindEntityByDocNumber(long? docNumber)
        {
            try
            {
                var query = await (from a in _dbContext.GeneralLedger
                                   where a.DocNumber == docNumber
                                   select a).FirstOrDefaultAsync<GeneralLedger>();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<GeneralLedger> FindEntityByView(GeneralLedgerView view)
        {
            try
            {
                var query = await (from e in _dbContext.GeneralLedger
                                   where e.AccountId == view.AccountId
                                   && e.Amount == view.Amount
                                   && e.Gldate == view.GLDate
                                   && e.DocNumber == view.DocNumber
                                   && e.CheckNumber == view.CheckNumber
                                   select e
                            ).FirstOrDefaultAsync<GeneralLedger>();
                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }


    }
}
