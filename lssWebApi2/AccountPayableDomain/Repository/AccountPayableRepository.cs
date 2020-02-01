   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AccountPayableDomain
{
    public class AccountPayableView
    {

        public long AcctPayId { get; set; }
        public long? DocNumber { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Remark { get; set; }
        public DateTime? Gldate { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public long? ContractId { get; set; }
        public long? PoquoteId { get; set; }
        public string Description { get; set; }
        public long? PurchaseOrderId { get; set; }
        public decimal? Tax { get; set; }
        public long AccountId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? AmountOpen { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public long AccountPayableNumber { get; set; }

     

    }
    public class AccountPayableRepository: Repository<AccountPayable>, IAccountPayableRepository
    {
        ListensoftwaredbContext _dbContext;
        public AccountPayableRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<AccountPayable> GetEntityByPurchaseOrderView(PurchaseOrderView poView)
        {
            List<AccountPayable> list = await (from detail in _dbContext.AccountPayable
                                               where detail.OrderNumber == poView.Ponumber
                                               select detail).ToListAsync<AccountPayable>();

            AccountPayable acctPay = null;
            if (list.Count == 0)
            {
               
                acctPay = new AccountPayable();
              
                acctPay.GrossAmount = poView.Amount;
                acctPay.Remark = "";
                acctPay.Gldate = DateTime.Today.Date;
                acctPay.SupplierId = poView.SupplierId;
                acctPay.ContractId = poView.ContractId;
                acctPay.PoquoteId = poView.PoquoteId;
                acctPay.Description = poView.Description;
                acctPay.PurchaseOrderId = poView.PurchaseOrderId;
                acctPay.Tax = poView.Tax;
                acctPay.AccountId = poView.AccountId;
                acctPay.DocType = poView.DocType;
                acctPay.PaymentTerms = poView.PaymentTerms;
                acctPay.AmountOpen = poView.Amount;
                acctPay.OrderNumber = poView.Ponumber;
                acctPay.AmountPaid = 0;

            }
            return (acctPay);


        }

        public async Task<AccountPayable> GetEntityByGeneralLedger(GeneralLedgerView ledgerView)
        {
            List<AccountPayable> list = await (from detail in _dbContext.AccountPayable
                                               where detail.DocNumber==ledgerView.DocNumber
                                               select detail).ToListAsync<AccountPayable>();

            AccountPayable acctPay = list[0];


            if (acctPay != null)
            {

                //Find the General Ledger Cash Amount by Doc Number
                var query = await (from e in _dbContext.GeneralLedger
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
             
            }
            await Task.Yield();
            return acctPay;

        }
        public async Task<AccountPayable> GetAcctPayableByDocNumber(long docNumber)
        {
            try
            {
                List<AccountPayable> list = await (from detail in _dbContext.AccountPayable
                                                   where detail.DocNumber==docNumber
                                                   select detail).ToListAsync<AccountPayable>();
                AccountPayable acctPay = list[0];

                return acctPay;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountPayable> GetEntityByInvoiceId(long? docNumber)
        {
            try
            {
                var query = await (from a in _dbContext.AccountPayable
                                   where a.DocNumber == docNumber
                                   select a).FirstOrDefaultAsync<AccountPayable>();

                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<AccountPayable> GetAcctPayByPONumber(string poNumber)
        {
            try
            {

                AccountPayable acctPay = await (from e in _dbContext.AccountPayable
                                                where e.OrderNumber == poNumber
                                         select e).FirstOrDefaultAsync<AccountPayable>();

                return acctPay;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountPayable>GetEntityById(long ? accountPayableId)
        {
			try{
            return await _dbContext.FindAsync<AccountPayable>(accountPayableId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<AccountPayable> GetEntityByNumber(long accountPayableNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.AccountPayable
                               where detail.AccountPayableNumber == accountPayableNumber
                               select detail).FirstOrDefaultAsync<AccountPayable>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		

		
  }
}
