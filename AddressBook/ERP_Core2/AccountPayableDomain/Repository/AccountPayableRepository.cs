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
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.PurchaseOrderDomain;
using MillenniumERP.SupplierInvoicesDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace MillenniumERP.AccountsPayableDomain
{
    

    public class AccountPayableView
    {
        public AccountPayableView() { }
        public AccountPayableView(AcctPay acctPay)
        {
            this.AcctPayId = acctPay.AcctPayId;
            this.DocNumber = acctPay.DocNumber;
            this.GrossAmount = acctPay.GrossAmount;
            this.DiscountAmount = acctPay.DiscountAmount;
            this.Remark = acctPay.Remark;
            this.GLDate = acctPay.GLDate;
            this.SupplierId = acctPay.SupplierId;
            this.SupplierName = acctPay.Supplier.AddressBook.Name;
            this.ContractId = acctPay.ContractId;
            this.POQuoteId = acctPay.POQuoteId;
            this.Description = acctPay.Description;
            this.PurchaseOrderId = acctPay.PurchaseOrderId;
            this.Tax = acctPay.Tax;
            this.InvoiceId = acctPay.InvoiceId;
            this.AccountId = acctPay.AccountId;
            this.DocType = acctPay.DocType;
            this.PaymentTerms = acctPay.PaymentTerms;
            this.DiscountPercent = acctPay.DiscountPercent;
            this.AmountPaid = acctPay.AmountPaid;
            this.AmountOpen = acctPay.AmountOpen;
            this.OrderNumber = acctPay.OrderNumber;
            this.DiscountDueDate = acctPay.DiscountDueDate;
        }
        public long AcctPayId { get; set; }
        public long? DocNumber { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Remark { get; set; }
        public DateTime? GLDate { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long? ContractId { get; set; }
        public long? POQuoteId { get; set; }
        public string Description { get; set; }
        public long? PurchaseOrderId { get; set; }
        public decimal? Tax { get; set; }
        public long? InvoiceId { get; set; }
        public long AccountId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? AmountOpen { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? DiscountDueDate { get; set; }
    }



    public class AccountPayableRepository : Repository<AcctPay>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AccountPayableRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        
        public async Task<CreateProcessStatus> CreateAcctPayByPurchaseOrderView(PurchaseOrderView poView)
        {
            try
            {
                //Check if exists
                List<AcctPay> list = await GetObjectsQueryable(e => e.OrderNumber == poView.PONumber).ToListAsync<AcctPay>();

                if (list.Count == 0)
                {
                    NextNumber nextNumber = await base.GetNextNumber("DocNumber");
                    AcctPay acctPay = new AcctPay();
                    acctPay.DocNumber = nextNumber.NextNumberValue;
                    acctPay.GrossAmount = poView.GrossAmount;
                    acctPay.Remark = "";
                    acctPay.GLDate = DateTime.Today.Date;
                    acctPay.SupplierId = poView.SupplierId;
                    acctPay.ContractId = poView.ContractId;
                    acctPay.POQuoteId = poView.POQuoteId;
                    acctPay.Description = poView.Description;
                    acctPay.PurchaseOrderId = poView.PurchaseOrderId;
                    acctPay.Tax = poView.Tax;
                    acctPay.AccountId = poView.AccountId;
                    acctPay.DocType = poView.DocType;
                    acctPay.PaymentTerms = poView.PaymentTerms;
                    acctPay.AmountOpen = poView.GrossAmount;
                    acctPay.OrderNumber = poView.PONumber;
                    acctPay.AmountPaid = 0;
                    AddObject(acctPay);
                    return CreateProcessStatus.Inserted;
                }
                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AcctPay> GetAcctPayableByDocNumber(long docNumber)
        {
            try
            {
                List<AcctPay> list = await GetObjectsQueryable(e => e.DocNumber == docNumber).ToListAsync<AcctPay>();
                AcctPay acctPay = list[0];

                return acctPay;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdatePayableByCashLedger(GeneralLedgerView ledgerView)
        {

            try
            {
                List<AcctPay> list = await GetObjectsQueryable(e => e.DocNumber == ledgerView.DocNumber).ToListAsync<AcctPay>();
                AcctPay acctPay = list[0];


                if (acctPay != null)
                {

                    //Find the General Ledger Cash Amount by Doc Number
                    var query = await (from e in _dbContext.GeneralLedgers
                                       where e.DocNumber == ledgerView.DocNumber
                                       && e.DocType == "PV"
                                       && e.LedgerType == "AA"
                                       && e.AccountId == ledgerView.AccountId
                                       group e by e.DocNumber
                                       into g

                                       select new { AmountPaid = g.Sum(e => e.Amount) }
                                       ).FirstOrDefaultAsync();


                    decimal? cash = query?.AmountPaid ?? 0;
                    acctPay.AmountPaid = cash;
                    acctPay.AmountOpen = acctPay.GrossAmount - acctPay.AmountPaid;
                    decimal discountAmount = acctPay.GrossAmount * acctPay.DiscountPercent ?? 0;
                    //Check for Discount Dates
                    if (
                        (acctPay.DiscountDueDate <= ledgerView.GLDate)
                    && ((acctPay.AmountPaid + discountAmount) == acctPay.AmountOpen)
                    )
                    {
                        acctPay.AmountOpen = acctPay.GrossAmount - (acctPay.AmountPaid + discountAmount);

                    }
                    UpdateObject(acctPay);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountPayableView> GetAccountPayableViewByInvoiceId(long? docNumber)
        {
            try
            {
                var query = await (from a in _dbContext.AcctPays
                                   where a.DocNumber == docNumber
                                   select a).FirstOrDefaultAsync<AcctPay>();

                if (query != null)
                {
                    AccountPayableView view = applicationViewFactory.MapAccountPayableView(query);
                    return view;
                }
                return null;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<bool> UpdateAcct(AcctPay acctPay)
        {
            try
            {
                var query = await GetObjectAsync(acctPay.AcctPayId);

                AcctPay acctRecBase = query;



                UpdateObject(acctRecBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteAcctRec(AcctPay acctPay)
        {
            try
            {
                DeleteObject(acctPay);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
