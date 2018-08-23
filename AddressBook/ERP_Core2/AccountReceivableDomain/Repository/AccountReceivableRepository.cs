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
using MillenniumERP.InvoicesDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace MillenniumERP.AccountsReceivableDomain
{
    public class AccountReceiveableView
    {
        public AccountReceiveableView() { }
        public AccountReceiveableView(AcctRec acctRec)
        {
            this.AcctRecId = acctRec.AcctRecId;
            this.OpenAmount = acctRec.OpenAmount;
            this.DiscountDueDate = acctRec.DiscountDueDate;
            this.PaymentDueDate = acctRec.PaymentDueDate;
            this.GLDate = acctRec.GLDate;
            this.InvoiceId = acctRec.InvoiceId;
            this.InvoiceNumber = acctRec.Invoice.InvoiceNumber;
            this.CreateDate = acctRec.CreateDate;
            this.DocNumber = acctRec.DocNumber;
            this.Remarks = acctRec.Remarks;
            this.PaymentTerms = acctRec.PaymentTerms;
            this.CustomerId = acctRec.CustomerId;
            this.CustomerName = acctRec.Customer.AddressBook.Name;
            this.PurchaseOrderId = acctRec.PurchaseOrderId??0;
            this.Description = acctRec.Description;
            this.AcctRecDocTypeXRefId = acctRec.AcctRecDocTypeXRefId;
            this.DocType = acctRec.UDC.Value;
            this.Amount = acctRec.Amount;
            this.AccountId = acctRec.AccountId;
        }
        public long? AcctRecId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? OpenAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? GLDate { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? DocNumber { get; set; }
        public string Remarks { get; set; }
        public string PaymentTerms { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? PurchaseOrderId { get; set; }
        public string Description { get; set; }
        public long? AcctRecDocTypeXRefId { get; set; }
        public string DocType { get; set; }
        public long? AccountId { get; set; }
    }
    public class AccountReceivableRepository: Repository<AcctRec>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AccountReceivableRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<AcctRec> GetAcctRecByDocNumber(long docNumber)
        {
            try
            {
                List<AcctRec> list = await GetObjectsQueryable(e => e.DocNumber == docNumber).ToListAsync<AcctRec>();
                AcctRec acctRec = list[0];

                return acctRec;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateReceivableByCashLedger(GeneralLedgerView ledgerView)
        {
           
            try
            {
                List<AcctRec> list = await GetObjectsQueryable(e => e.DocNumber == ledgerView.DocNumber).ToListAsync<AcctRec>();
                AcctRec acctRec = list[0];


                if (acctRec != null)
                {

                    //Find the General Ledger Cash Amount by Doc Number
                    var query = await (from e in _dbContext.GeneralLedgers
                                       where e.DocNumber == ledgerView.DocNumber
                                       && e.DocType == "PV"
                                       && e.LedgerType == "AA"
                                       && e.AccountId== ledgerView.AccountId
                                       group e by e.DocNumber
                                       into g

                                       select new { AmountPaid = g.Sum(e => e.Amount) }
                                       ).FirstOrDefaultAsync();


                    decimal? cash = query?.AmountPaid??0;
                    acctRec.DebitAmount = cash;
                    acctRec.OpenAmount = acctRec.Amount - acctRec.DebitAmount;
                    decimal discountAmount = acctRec.Amount * acctRec.DiscountPercent ?? 0;
                    //Check for Discount Dates
                    if (
                        (acctRec.DiscountDueDate <= ledgerView.GLDate)
                        &&
((acctRec.DebitAmount + discountAmount)==acctRec.Amount)
                        )
                    {
                        acctRec.OpenAmount = acctRec.Amount - (acctRec.DebitAmount + discountAmount);

                    }
                    UpdateObject(acctRec);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountReceiveableView> GetAccountReceivableViewByInvoiceId(long ? invoiceId)
        {
            try
            {
                var query = await (from a in _dbContext.AcctRecs
                                   where a.InvoiceId == invoiceId
                                   select a).FirstOrDefaultAsync<AcctRec>();

                if (query != null)
                {
                    AccountReceiveableView view = applicationViewFactory.MapAccountReceivableView(query);
                    return view;
                }
                return null;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreateAcctRecFromInvoice(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoice = await (from e in _dbContext.Invoices
                                         where e.InvoiceNumber == invoiceView.InvoiceNumber
                                         select e).FirstOrDefaultAsync<Invoice>();

                if (invoice != null)
                {
                    long? invoiceId = invoice.InvoiceId;

                    var query = await (from a in _dbContext.AcctRecs
                                       where a.InvoiceId == invoice.InvoiceId
                                       select a).FirstOrDefaultAsync<AcctRec>();

                    if (query == null)
                    {
                        UDC udc = await base.GetUdc("ACCTRECDOCTYPE", "INV");

                        NextNumber nextNumber = await base.GetNextNumber("DocNumber");

                        ChartOfAcct chartOfAcct = await base.GetChartofAccount("1000", "1200", "120", "");

                        AcctRec acctRec = new AcctRec();
                        acctRec.InvoiceId = invoice.InvoiceId;
                        acctRec.DiscountDueDate = invoice.DiscountDueDate;
                        acctRec.GLDate = DateTime.Now.Date;
                        acctRec.CreateDate = DateTime.Now.Date;
                        acctRec.DocNumber = nextNumber.NextNumberValue;
                        acctRec.Remarks = invoice.Description;
                        acctRec.PaymentTerms = invoice.PaymentTerms;
                        acctRec.CustomerId = invoice.CustomerId;
                        //PurchaseOrderId 
                        acctRec.Description = invoice.Description;
                        acctRec.AcctRecDocTypeXRefId = udc.XRefId;
                        acctRec.AccountId = chartOfAcct.AccountId;
                        acctRec.Amount = invoice.Amount;
                        acctRec.OpenAmount = invoice.Amount;
                        acctRec.DebitAmount = 0;
                        acctRec.CreditAmount = invoice.Amount;

                        AddObject(acctRec);
                        return CreateProcessStatus.Inserted;
                    }

                }

                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateAcct(AcctRec acctRec)
        {
            try
            {
                var query = await GetObjectAsync(acctRec.AcctRecId);

                AcctRec acctRecBase = query;

                
                
                UpdateObject(acctRecBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
   
            }
        public bool DeleteAcctRec(AcctRec acctRec)
        {
            try
            {
                DeleteObject(acctRec);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
      
        }
    }
}
