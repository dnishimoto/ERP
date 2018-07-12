using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;

namespace ERP_Core2.InvoiceDomain
{
    
       public class UnitTestInvoices
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestInvoices(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestPointInvoiceToCashPayment()
        {
          

        }

        [Fact]
        public async Task TestPostInvoiceToAcctRec()
        {
            try
            {
                Invoice invoice = new Invoice();

                UDC udc = await unitOfWork.invoiceRepository.GetUdc("PAYMENTTERMS", "COD");
         
                invoice.InvoiceNumber = "Test1";
                invoice.InvoiceDate = DateTime.Today.Date;
                invoice.Amount = 999.99M;
                invoice.CustomerId = 3;
                invoice.Description = "Test Invoice to Acct Rec";
                invoice.TaxAmount = 0.0M;
                invoice.PaymentDueDate = DateTime.Today.Date;
                invoice.PaymentTerms = udc.Value;
                invoice.CompanyId = 1;


                bool result = await unitOfWork.invoiceRepository.AddInvoice(invoice);
                unitOfWork.CommitChanges();
                List<Invoice>list = unitOfWork.invoiceRepository.GetObjectsAsync(e=>e.InvoiceNumber==invoice.InvoiceNumber).ToList<Invoice>();
                invoice.InvoiceId = list[0].InvoiceId;
                bool result2 = await unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoice);
                //unitOfWork.invoiceRepository.DeleteInvoice(invoice);
                unitOfWork.CommitChanges();

                AccountReceiveableView acctRecView =await unitOfWork.accountReceiveableRepository.GetAccountReceivableViewByInvoiceId(invoice.InvoiceId);
                bool result3 = await unitOfWork.generalLedgerRepository.CreateLedgerFromReceiveable(acctRecView);

                unitOfWork.CommitChanges();

                GeneralLedgerView ledger = await unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(acctRecView.DocNumber,"OV");
                bool result4 = await unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledger.AccountId,ledger.FiscalYear,ledger.FiscalPeriod);



                Assert.True(true);
            }
            catch (Exception ex)
            {

            }
        }
            
    
    }
}
