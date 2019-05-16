using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;
using ERP_Core2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;

namespace ERP_Core2.InvoiceDomain
{


    public class InvoiceModule 
    {
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentAccountsReceivable AccountsReceivable = new FluentAccountsReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();

        public bool PostInvoiceAndDetailToAcctRec(InvoiceView invoiceView)
        {
            try
            {
                Invoice
                    .CreateInvoice(invoiceView)
                    .Apply()
                    .MergeWithInvoiceNumber(ref invoiceView);
                InvoiceDetail
                    .CreateInvoiceDetails(invoiceView)
                    .Apply();
                AccountsReceivable
                    .CreateAcctRecFromInvoice(invoiceView)
                    .Apply();
                GeneralLedger
                    .CreateGeneralLedger(invoiceView)
                    .Apply()
                    .UpdateLedgerBalances();
                return true;
            }
            catch (Exception ex) { throw new Exception("PostInvoiceAndDetailToAcctRec", ex); }
        }

   }
}
