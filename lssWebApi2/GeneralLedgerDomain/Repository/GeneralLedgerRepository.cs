using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountsReceivableDomain;
using System.Data.SqlClient;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ERP_Core2.GeneralLedgerDomain
{
    public class GeneralLedgerView
    {
        public GeneralLedgerView() { }
        public GeneralLedgerView(GeneralLedger generalLedger)
        {
            this.GeneralLedgerId = generalLedger.GeneralLedgerId;
            this.DocNumber = generalLedger.DocNumber;
            this.DocType = generalLedger.DocType;
            this.Amount = generalLedger.Amount;
            this.LedgerType = generalLedger.LedgerType;
            this.GLDate = generalLedger.Gldate;
            this.AccountId = generalLedger.AccountId;
            this.CreatedDate = generalLedger.CreatedDate;
            this.AddressId = generalLedger.AddressId;
            this.Comment = generalLedger.Comment;
            this.DebitAmount = generalLedger.DebitAmount;
            this.CreditAmount = generalLedger.CreditAmount;
            this.FiscalPeriod = generalLedger.FiscalPeriod??0;
            this.FiscalYear = generalLedger.FiscalYear??0;
            this.CheckNumber = generalLedger.CheckNumber;
            this.PurchaseOrderNumber = generalLedger.PurchaseOrderNumber;
            this.Units = generalLedger.Units;
        }

        public long GeneralLedgerId { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public long? InvoiceId { get; set; }
        public long? AcctPayId { get; set; }
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
        public string PurchaseOrderNumber { get; set; }
        public decimal? Units { get; set; }
    }
    public class AccountSummaryView {

        private List<GeneralLedgerView> _ledgers = new List<GeneralLedgerView>();

        public AccountSummaryView() {
           // if (ledgers == null)
            //{
            //    ledgers = new List<GeneralLedger>();
           // }
        }
      
        public long AccountId { get; set; }
        public int? FiscalPeriod { get; set; }
        public int? FiscalYear { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }

        public List<GeneralLedgerView> ledgers { get { return _ledgers; }  }
     
    }

    public class GeneralLedgerRepository : Repository<GeneralLedger>
    {
        public ListensoftwareDBContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public GeneralLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            try
            {
                var query = (from e in _dbContext.GeneralLedger
                             where (e.LedgerType == "AA" && e.DocType == "PV" && e.FiscalYear == fiscalYear)
                             group e by e.AccountId into pg

                             join f in _dbContext.ChartOfAccts
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

                    foreach (var ledger in item.Ledgers.OrderByDescending(i=>i.Gldate))
                   {
                        GeneralLedgerView glView = applicationViewFactory.MapGeneralLedgerView(ledger);
                        view.ledgers.Add(glView);
                    }

                    list.Add(view);
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
 
        }
        public async Task<CreateProcessStatus> CreateLedgerFromView(GeneralLedgerView view)
        {
            try
            {
                GeneralLedger ledger = new GeneralLedger();

                var query = await (from e in _dbContext.GeneralLedger
                             where e.AccountId == view.AccountId
                             && e.Amount == view.Amount
                             && e.Gldate == view.GLDate
                             && e.DocNumber == view.DocNumber
                             && e.CheckNumber==view.CheckNumber
                             select e
                             ).FirstOrDefaultAsync<GeneralLedger>();

                if (query == null)
                {

                    applicationViewFactory.MapGeneralLedgerEntity(ref ledger, view);

                    AddObject(ledger);

                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;
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


                var result = await _dbContext.Database.ExecuteSqlCommandAsync("usp_RollupGeneralLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear", param1, param2, param3);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<GeneralLedgerView> GetLedgerByDocNumber(long ? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.GeneralLedger
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<GeneralLedger>();

                GeneralLedgerView view = applicationViewFactory.MapGeneralLedgerView(query);
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreateLedgerFromReceiveable(AccountReceiveableView accountReceivableView)
        {
            try
            {
                var query = await (from a in _dbContext.GeneralLedger
                                   where a.DocNumber == accountReceivableView.DocNumber
                                   select a).FirstOrDefaultAsync<GeneralLedger>();

                if (query == null)
                {
                    long addressId = await base.GetAddressIdByCustomerId(accountReceivableView.CustomerId);
                    //Revenue Account
                    ChartOfAccts chartOfAcct = await base.GetChartofAccount("1000", "1200", "250", "");

                    GeneralLedger ledger = new GeneralLedger();
                    ledger.DocNumber = accountReceivableView.DocNumber??0;
                    ledger.DocType = "OV";
                    ledger.Amount = accountReceivableView.Amount??0;
                    ledger.LedgerType = "AA";
                    ledger.Gldate = DateTime.Now.Date;
                    ledger.FiscalPeriod = DateTime.Now.Date.Month;
                    ledger.FiscalYear  = DateTime.Now.Date.Year;
                    ledger.AccountId = chartOfAcct.AccountId;
                    ledger.CreatedDate = DateTime.Now.Date;
                    ledger.AddressId = addressId;
                    ledger.Comment = accountReceivableView.Remarks;
                    ledger.DebitAmount = 0.0M;
                    ledger.CreditAmount= accountReceivableView.Amount ?? 0;
                    AddObject(ledger);
                    return CreateProcessStatus.Insert;
            
                }
                return CreateProcessStatus.Failed;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

        public async Task<CreateProcessStatus> UpdateGeneralLedger(GeneralLedger generalLedger)
        {
            try
            {
                var query = await GetObjectAsync(generalLedger.GeneralLedgerId);

                GeneralLedger generalLedgerBase = query;



                UpdateObject(generalLedgerBase);
                return CreateProcessStatus.Update;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public CreateProcessStatus DeleteGeneralLedger(GeneralLedger generalLedger)
        {
            try
            {
                DeleteObject(generalLedger);
                return CreateProcessStatus.Delete;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
