using lssWebApi2.AbstractFactory;
using lssWebApi2.FluentAPI;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using lssWebApi2.CustomerDomain;

namespace lssWebApi2.InvoiceDomain
{


    public class InvoiceModule 
    {
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentAddressBook AddressBook = new FluentAddressBook();

        public bool PostInvoiceAndDetailToAcctRec(InvoiceView invoiceView)
        {
            try
            {
                Invoice
                    .CreateInvoiceByView(invoiceView)
                    .Apply()
                    .MergeWithInvoiceNumber(ref invoiceView);

                InvoiceDetail
                    .CreateInvoiceDetailsByInvoiceView(invoiceView)
                    .Apply();
                AccountsReceivable
                    .CreateAcctRecByInvoiceView(invoiceView)
                    .Apply();

                GeneralLedger
                    .CreateGeneralLedgerByInvoiceView(invoiceView)
                    .Apply()
                    .UpdateLedgerBalances();
                return true;
            }
            catch (Exception ex) { throw new Exception("PostInvoiceAndDetailToAcctRec", ex); }
        }

   }
}
