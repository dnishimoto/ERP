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
using MillenniumERP.AccountsReceivableDomain;
using System.Data.SqlClient;
using MillenniumERP.GeneralLedgerDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;
using ERP_Core2.AccountPayableDomain;

namespace MillenniumERP.CustomerLedgerDomain
{
    public class CustomerLedgerView
    {
        public CustomerLedgerView() { }
        public CustomerLedgerView(GeneralLedgerView ledger)
        {
            this.DocNumber = ledger.DocNumber;
            this.DocType = ledger.DocType;
            this.Amount = ledger.Amount ;
            this.GLDate = ledger.GLDate ;
            this.CreatedDate = ledger.CreatedDate ;
            this.AccountId = ledger.AccountId;
            this.AddressId = ledger.AddressId;
            this.Comment = ledger.Comment;
            this.DebitAmount = ledger.DebitAmount;
            this.CreditAmount = ledger.CreditAmount;
            this.FiscalPeriod = ledger.FiscalPeriod;
            this.FiscalYear = ledger.FiscalYear;
            this.GeneralLedgerId = ledger.GeneralLedgerId;
        }
        public CustomerLedgerView(CustomerLedger CustomerLedger)
        {
            this.CustomerLedgerId = CustomerLedger.CustomerLedgerId;
            this.CustomerId = CustomerLedger.CustomerId;
            this.CustomerName = CustomerLedger.Customer.AddressBook.Name;
            this.InvoiceId = CustomerLedger.InvoiceId;
            this.AcctRecId = CustomerLedger.AcctRecId;
            this.DocNumber = CustomerLedger.DocNumber;
            this.DocType = CustomerLedger.DocType;
            this.Amount = CustomerLedger.Amount??0;
            this.GLDate = CustomerLedger.GLDate??DateTime.Now.Date;
            this.CreatedDate = CustomerLedger.CreatedDate ?? DateTime.Now.Date;
            this.AccountId = CustomerLedger.AccountId;
            this.AddressId = CustomerLedger.AddressId;
            this.Comment = CustomerLedger.Comment;
            this.DebitAmount = CustomerLedger.DebitAmount;
            this.CreditAmount = CustomerLedger.CreditAmount;
            this.FiscalPeriod = CustomerLedger.FiscalPeriod;
            this.FiscalYear = CustomerLedger.FiscalYear;
            this.GeneralLedgerId = CustomerLedger.GeneralLedgerId;
        }
        public long CustomerId { get; set; }
        public long GeneralLedgerId { get; set; }
        public string CustomerName { get; set; }
        public long InvoiceId { get; set; }
        public long AcctRecId { get; set; }
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
    }

    public class CustomerLedgerRepository : Repository<CustomerLedger>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public CustomerLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateLedgerFromView(CustomerLedgerView view)
        {
            try
            {
                CustomerLedger customerLedger = new CustomerLedger();

                var query = await (from e in _dbContext.CustomerLedgers
                             where e.AccountId == view.AccountId
                             && e.Amount == view.Amount
                             && e.GLDate == view.GLDate
                             && e.DocNumber == view.DocNumber
                             && e.Comment == view.Comment
                           
                             select e
                             ).FirstOrDefaultAsync<CustomerLedger>();

                if (query == null)
                {

                    applicationViewFactory.MapCustomerLedgerEntity(ref customerLedger, view);


                    AddObject(customerLedger);

                    return CreateProcessStatus.Inserted;

              
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


                var result = await _dbContext.Database.ExecuteSqlCommandAsync("usp_RollupCustomerLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear", param1, param2, param3);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CustomerLedgerView> GetCustomerLedgerByDocNumber(long ? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.CustomerLedgers
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<CustomerLedger>();

                CustomerLedgerView view = applicationViewFactory.MapCustomerLedgerView(query);
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
      

        public async Task<bool> UpdateCustomerLedger(CustomerLedger CustomerLedger)
        {
            try
            {
                var query = await GetObjectAsync(CustomerLedger.CustomerLedgerId);

                CustomerLedger CustomerLedgerBase = query;



                UpdateObject(CustomerLedgerBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteCustomerLedger(CustomerLedger CustomerLedger)
        {
            try
            {
                DeleteObject(CustomerLedger);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
