using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using System.Collections;
using ERP_Core2.AccountsReceivableDomain;
using System.Data.SqlClient;
using ERP_Core2.GeneralLedgerDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;
using ERP_Core2.AccountPayableDomain;

namespace ERP_Core2.CustomerLedgerDomain
{
    public class SupplierLedgerView
    {
        public SupplierLedgerView() { }

        public SupplierLedgerView(GeneralLedgerView ledger)
        {
            this.SupplierId = ledger.SupplierId??0;
            this.InvoiceId = ledger.InvoiceId??0;
            this.AcctPayId = ledger.AcctPayId??0;
            this.DocNumber = ledger.DocNumber;
            this.DocType = ledger.DocType;
            this.Amount = ledger.Amount;
            this.GLDate = ledger.GLDate;
            this.CreatedDate = ledger.CreatedDate;
            this.AccountId = ledger.AccountId;
            this.AddressId = ledger.AddressId;
            this.Comment = ledger.Comment;
            this.DebitAmount = ledger.DebitAmount;
            this.CreditAmount = ledger.CreditAmount;
            this.FiscalPeriod = ledger.FiscalPeriod;
            this.FiscalYear = ledger.FiscalYear;
            this.GeneralLedgerId = ledger.GeneralLedgerId;
        }
        public SupplierLedgerView(SupplierLedger SupplierLedger)
        {
            this.SupplierLedgerId = SupplierLedger.SupplierLedgerId;
            this.SupplierId = SupplierLedger.SupplierId;
            this.SupplierName = SupplierLedger.Supplier.AddressBook.Name;
            this.InvoiceId = SupplierLedger.InvoiceId;
            this.AcctPayId = SupplierLedger.AcctPayId;
            this.DocNumber = SupplierLedger.DocNumber;
            this.DocType = SupplierLedger.DocType;
            this.Amount = SupplierLedger.Amount ?? 0;
            this.GLDate = SupplierLedger.GLDate ?? DateTime.Now.Date;
            this.CreatedDate = SupplierLedger.CreatedDate ?? DateTime.Now.Date;
            this.AccountId = SupplierLedger.AccountId;
            this.AddressId = SupplierLedger.AddressId;
            this.Comment = SupplierLedger.Comment;
            this.DebitAmount = SupplierLedger.DebitAmount;
            this.CreditAmount = SupplierLedger.CreditAmount;
            this.FiscalPeriod = SupplierLedger.FiscalPeriod;
            this.FiscalYear = SupplierLedger.FiscalYear;
            this.GeneralLedgerId = SupplierLedger.GeneralLedgerId;
        }
        public long SupplierId { get; set; }
        public long GeneralLedgerId { get; set; }
        public string SupplierName { get; set; }
        public long InvoiceId { get; set; }
        public long AcctPayId { get; set; }
        public long SupplierLedgerId { get; set; }
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

    public class SupplierLedgerRepository : Repository<SupplierLedger>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public SupplierLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public async Task<CreateProcessStatus> CreateSupplierLedgerFromView(SupplierLedgerView view)
        {
            try
            {
                SupplierLedger supplierLedger = new SupplierLedger();

                var query = await (from e in _dbContext.SupplierLedgers
                                   where e.AccountId == view.AccountId
                                   && e.Amount == view.Amount
                                   && e.GLDate == view.GLDate
                                   && e.DocNumber == view.DocNumber
                                   select e
                             ).FirstOrDefaultAsync<SupplierLedger>();

                if (query == null)
                {

                    applicationViewFactory.MapSupplierLedgerEntity(ref supplierLedger, view);


                    AddObject(supplierLedger);

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


                var result = await _dbContext.Database.ExecuteSqlCommandAsync("usp_RollupCustomerLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear", param1, param2, param3);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<SupplierLedgerView> GetSupplierLedgerByDocNumber(long? docNumber, string docType)
        {
            try
            {
                var query = await (from a in _dbContext.SupplierLedgers
                                   where a.DocNumber == docNumber
                                   && a.DocType == docType
                                   select a).FirstOrDefaultAsync<SupplierLedger>();

                SupplierLedgerView view = applicationViewFactory.MapSupplierLedgerView(query);
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }


        public async Task<CreateProcessStatus> UpdateSupplierLedger(SupplierLedgerView supplierLedgerView)
        {
            try
            {
                var query = await GetObjectAsync(supplierLedgerView.SupplierLedgerId);

                SupplierLedger supplierLedgerBase = query;

                if (query != null)
                {
                    applicationViewFactory.MapSupplierLedgerEntity(ref supplierLedgerBase, supplierLedgerView);
                    UpdateObject(supplierLedgerBase);
                    return CreateProcessStatus.Update;
                }
                return CreateProcessStatus.Failed;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteSupplierLedger(SupplierLedger SupplierLedger)
        {
            try
            {
                DeleteObject(SupplierLedger);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
