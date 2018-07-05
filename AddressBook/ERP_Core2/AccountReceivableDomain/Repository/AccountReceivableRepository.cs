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
            this.PurchaseOrderId = acctRec.PurchaseOrderId;
            this.Description = acctRec.Description;
            this.AcctRecDocTypeXRefId = acctRec.AcctRecDocTypeXRefId;
            this.DocType = acctRec.UDC.Value;
        }
        public long? AcctRecId { get; set; }
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
        public async Task<bool> CreateAcctRecFromInvoice(Invoice invoice)
        {
            long? invoiceId = invoice.InvoiceId;
         
            var query = await (from a in _dbContext.AcctRecs
                         where a.InvoiceId==invoice.InvoiceId
                         select a).FirstOrDefaultAsync<AcctRec>();
            UDC udc = await base.GetUdc("ACCTRECDOCTYPE", "INV");

            ChartOfAcct chartOfAcct = await base.GetChartofAccount("1000","1200", "120", "");

            if (query == null)
            {
                AcctRec acctRec = new AcctRec();
                acctRec.InvoiceId = invoice.InvoiceId;
                acctRec.DiscountDueDate = invoice.DiscountDueDate;
                acctRec.GLDate = DateTime.Now.Date;
                acctRec.CreateDate = DateTime.Now.Date;
                //DocNumber 
                acctRec.Remarks = invoice.Description;
                acctRec.PaymentTerms = invoice.PaymentTerms;
                acctRec.CustomerId = invoice.CustomerId;
                //PurchaseOrderId 
                acctRec.Description = invoice.Description;
                acctRec.AcctRecDocTypeXRefId = udc.XRefId;
                acctRec.AccountId = chartOfAcct.AccountId;
                acctRec.Amount = invoice.Amount;

                AddObject(acctRec);
            }
           
            return true;
        }
        public async Task<bool> UpdateAcct(AcctRec acctRec)
        {
            try
            {
                var query = GetObjectAsync((int)acctRec.InvoiceId);

                AcctRec acctRecBase = query.Result;

                
                
                UpdateObject(acctRecBase);
                return true;
            }
            catch (Exception ex)
            {
                
            }
            return false;
            }
        public async Task<bool> DeleteAcctRec(AcctRec acctRec)
        {
            try
            {
                DeleteObject(acctRec);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
