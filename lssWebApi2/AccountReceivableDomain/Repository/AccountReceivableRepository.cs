using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AccountsReceivableDomain
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
            this.GLDate = acctRec.Gldate;
            this.InvoiceId = acctRec.InvoiceId;
            this.InvoiceNumber = acctRec.Invoice.InvoiceNumber;
            this.CreateDate = acctRec.CreateDate;
            this.DocNumber = acctRec.DocNumber;
            this.Remarks = acctRec.Remarks;
            this.PaymentTerms = acctRec.PaymentTerms;
            this.CustomerId = acctRec.CustomerId;
            this.CustomerName = acctRec.Customer.Address.Name;
            this.PurchaseOrderId = acctRec.PurchaseOrderId ?? 0;
            this.Description = acctRec.Description;
            this.AcctRecDocTypeXRefId = acctRec.AcctRecDocTypeXrefId;
            this.DocType = acctRec.AcctRecDocTypeXref.Value;
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
    public class AccountReceivableFlatView
    {
        public Decimal? OpenAmount { get; set; }
        public DateTime? GLDate { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDescription { get; set; }
        public long? DocNumber { get; set; }
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

    }
    public class AccountReceivableRepository : Repository<AcctRec>
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AccountReceivableRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public List<AccountReceivableFlatView> GetOpenAcctRec()
        {
            try { 
            List<AccountReceivableFlatView> list = (from ar in _dbContext.AcctRec
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
                                                    join coa in _dbContext.ChartOfAccts
                                                        on ar.AccountId equals coa.AccountId
                                                    where ar.OpenAmount > 0
                                                    select new AccountReceivableFlatView
                                                    {
                                                        OpenAmount = ar.OpenAmount,
                                                        GLDate = ar.Gldate,
                                                        InvoiceId = ar.InvoiceId,
                                                        InvoiceNumber = inv.InvoiceNumber,
                                                        InvoiceDescription = inv.Description,
                                                        DocNumber = ar.DocNumber,
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
                                                    }).ToList<AccountReceivableFlatView>();
            return list;
        }
             catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
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
        public async Task<CreateProcessStatus> UpdateReceivableByCashLedger(GeneralLedgerView ledgerView)
        {

            try
            {
                List<AcctRec> list = await GetObjectsQueryable(e => e.DocNumber == ledgerView.DocNumber).ToListAsync<AcctRec>();
                AcctRec acctRec = list[0];


                if (acctRec != null)
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
                    acctRec.DebitAmount = cash;
                    acctRec.OpenAmount = acctRec.Amount - acctRec.DebitAmount;
                    decimal discountAmount = acctRec.Amount * acctRec.DiscountPercent ?? 0;
                    //Check for Discount Dates
                    if (
                        (acctRec.DiscountDueDate <= ledgerView.GLDate)
                        &&
((acctRec.DebitAmount + discountAmount) == acctRec.Amount)
                        )
                    {
                        acctRec.OpenAmount = acctRec.Amount - (acctRec.DebitAmount + discountAmount);

                    }
                    UpdateObject(acctRec);
                    return CreateProcessStatus.Update;
                }
                return CreateProcessStatus.Failed;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<AccountReceiveableView> GetAccountReceivableViewByInvoiceId(long? invoiceId)
        {
            try
            {
                var query = await (from a in _dbContext.AcctRec
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
                Invoice invoice = await (from e in _dbContext.Invoice
                                         where e.InvoiceNumber == invoiceView.InvoiceNumber
                                         select e).FirstOrDefaultAsync<Invoice>();

                if (invoice != null)
                {
                    long? invoiceId = invoice.InvoiceId;

                    var query = await (from a in _dbContext.AcctRec
                                       where a.InvoiceId == invoice.InvoiceId
                                       select a).FirstOrDefaultAsync<AcctRec>();

                    if (query == null)
                    {
                        Udc udc = await base.GetUdc("ACCTRECDOCTYPE", "INV");

                        NextNumber nextNumber = await base.GetNextNumber("DocNumber");

                        ChartOfAccts chartOfAcct = await base.GetChartofAccount("1000", "1200", "120", "");

                        AcctRec acctRec = new AcctRec();
                        acctRec.InvoiceId = invoice.InvoiceId;
                        acctRec.DiscountDueDate = invoice.DiscountDueDate;
                        acctRec.Gldate = DateTime.Now.Date;
                        acctRec.CreateDate = DateTime.Now.Date;
                        acctRec.DocNumber = nextNumber.NextNumberValue;
                        acctRec.Remarks = invoice.Description;
                        acctRec.PaymentTerms = invoice.PaymentTerms;
                        acctRec.CustomerId = invoice.CustomerId;
                        //PurchaseOrderId 
                        acctRec.Description = invoice.Description;
                        acctRec.AcctRecDocTypeXrefId = udc.XrefId;
                        acctRec.AccountId = chartOfAcct.AccountId;
                        acctRec.Amount = invoice.Amount;
                        acctRec.OpenAmount = invoice.Amount;
                        acctRec.DebitAmount = 0;
                        acctRec.CreditAmount = invoice.Amount;

                        AddObject(acctRec);
                        return CreateProcessStatus.Insert;
                    }

                }

                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> UpdateAcct(AcctRec acctRec)
        {
            try
            {
                var query = await GetObjectAsync(acctRec.AcctRecId);

                AcctRec acctRecBase = query;



                UpdateObject(acctRecBase);
                return CreateProcessStatus.Update;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public CreateProcessStatus DeleteAcctRec(AcctRec acctRec)
        {
            try
            {
                DeleteObject(acctRec);
                return CreateProcessStatus.Delete;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
