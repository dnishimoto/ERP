using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace lssWebApi2.AccountReceivableDomain
{
    public class AccountReceivableView
    {
        public long AcctRecId { get; set; }
        public decimal? OpenAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? Gldate { get; set; }
        public long? InvoiceId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? DocNumber { get; set; }
        public string Remarks { get; set; }
        public string PaymentTerms { get; set; }
        public long CustomerId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public string Description { get; set; }
        public long AcctRecDocTypeXrefId { get; set; }
        public long AccountId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string AcctRecDocType { get; set; }
        public decimal? InterestPaid { get; set; }
        public decimal? LateFee { get; set; }
        public long AccountReceivableNumber { get; set; }
        public string CustomerPurchaseOrder { get; set; }
   
        public string InvoiceDocument { get; set; }
        public string CustomerName { get; set; }
        public string DocType { get; set; }
 
    }
    public class AccountReceivableFlatView
    {
        public Decimal? OpenAmount { get; set; }
        public DateTime? GLDate { get; set; }
        public long? AcctRecId { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceDocument { get; set; }
        public string InvoiceDescription { get; set; }
        public long? DocNumber { get; set; }
        public string AcctRecDocType { get; set; }
        public string Remarks { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Account { get; set; }
        public string CoaDescription { get; set; }
        public decimal? GLAmount { get; set; }
        public long AccountReceivableNumber { get; set; }

    }
    public class AccountReceivableRepository : Repository<AccountReceivable>, IAccountReceivableRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AccountReceivableRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<AccountReceivable> GetEntityByPurchaseOrderId(long? purchaseOrderId)
        {
            var query= await (from e in _dbContext.AccountReceivable
                        where e.PurchaseOrderId == purchaseOrderId
                        select e).FirstOrDefaultAsync<AccountReceivable>();
            return query;
        }
        public async Task<AccountReceivable> GetEntityById(long? accountReceivableId)
        {
            try
            {
                return await _dbContext.FindAsync<AccountReceivable>(accountReceivableId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IQueryable<AccountReceivable> GetQueryableByCustomerId(long? customerId)
        {
            IQueryable<AccountReceivable> query =

                          (from e in _dbContext.AccountReceivable
                           where e.CustomerId == customerId
                           && e.OpenAmount > 0
                           select e);
            return query;
        }
        public bool HasLateFee(long? acctRecId)
        {
            try
            {
                bool status = (from e in _dbContext.AccountReceivable
                               where e.AcctRecId == acctRecId
                               select e).Any();

                return status;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
     
       
        public bool IsPaymentLate(long? invoiceId, DateTime asOfDate)
        {
            try
            {
                bool status = (from e in _dbContext.AccountReceivable
                               where e.InvoiceId == invoiceId
                               && DateTime.Parse(e.PaymentDueDate.ToString()).AddDays(15) < asOfDate
                               select e).Any();

                return status;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<AccountReceivableFlatView>> GetOpenAcctRec()
        {
            try
            {
                IList<AccountReceivableFlatView> list = await (from ar in _dbContext.AccountReceivable
                                                        join inv in _dbContext.Invoice
                                                            on ar.InvoiceId equals inv.InvoiceId
                                                        join cust in _dbContext.Customer
                                                            on ar.CustomerId equals cust.CustomerId
                                                        join udcDocType in _dbContext.Udc
                                                            on ar.AcctRecDocTypeXrefId equals udcDocType.XrefId
                                                        join abCust in _dbContext.AddressBook
                                                            on cust.AddressId equals abCust.AddressId
                                                        join la in _dbContext.LocationAddress
                                                            on abCust.AddressId equals la.AddressId
                                                        join coa in _dbContext.ChartOfAccount
                                                            on ar.AccountId equals coa.AccountId
                                                        where ar.OpenAmount > 0
                                                        select new AccountReceivableFlatView
                                                        {
                                                            OpenAmount = ar.OpenAmount,
                                                            GLDate = ar.Gldate,
                                                            AcctRecId = ar.AcctRecId,
                                                            InvoiceId = ar.InvoiceId,
                                                            InvoiceDocument = inv.InvoiceDocument,
                                                            InvoiceDescription = inv.Description,
                                                            DocNumber = ar.DocNumber,
                                                            AcctRecDocType = ar.AcctRecDocType,
                                                            Remarks = ar.Remarks,
                                                            PaymentTerms = ar.PaymentTerms,
                                                            PaymentDueDate = ar.PaymentDueDate,
                                                            DiscountDueDate = ar.DiscountDueDate,
                                                            CustomerId = ar.CustomerId,
                                                            CustomerName = abCust.Name,
                                                            AddressLine1 = la.AddressLine1,
                                                            AddressLine2 = la.AddressLine2,
                                                            City = la.City,
                                                            State = la.State,
                                                            Zipcode = la.Zipcode,
                                                            Account = coa.Account,
                                                            CoaDescription = coa.Description,
                                                            GLAmount = _dbContext.GeneralLedger.Where(e => e.AccountId == ar.AccountId && e.DocNumber == ar.DocNumber).Sum(e => (decimal?)e.Amount)
                                                        }).ToListAsync<AccountReceivableFlatView>();
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountReceivable> GetAcctRecByDocNumber(long docNumber)
        {
            try
            {
                List<AccountReceivable> list = await (from detail in _dbContext.AccountReceivable
                                                      where detail.DocNumber==docNumber
                                                      select detail).ToListAsync<AccountReceivable>();
                AccountReceivable acctRec = list[0];

                return acctRec;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        
        public async Task<AccountReceivable> GetEntityByInvoiceId(long? invoiceId)
        {
            try
            {
                var query = await (from a in _dbContext.AccountReceivable
                                   where a.InvoiceId == invoiceId
                                   select a).FirstOrDefaultAsync<AccountReceivable>();

                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
      
    
       
    }
}
