using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.InvoiceDetailsDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.Interfaces;

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
        public void TestPostInvoiceAndDetailToAcctRec()
        {
            try
            {
                //NextNumber nextNumber = await unitOfWork.invoiceRepository.GetNextNumber("InvoiceNumber");

                InvoiceView invoiceView = new InvoiceView();

                invoiceView.InvoiceNumber = "Inv-03";
                invoiceView.InvoiceDate = DateTime.Parse("8/10/2018");
                invoiceView.Amount = 1500.0M;
                invoiceView.CustomerId = 9;
                invoiceView.Description = "VNW Fixed Asset project";
                invoiceView.PaymentTerms = "Net 30";
                invoiceView.TaxAmount = 0;
                invoiceView.CompanyId = 1;

                InvoiceDetailView invoiceDetailView = new InvoiceDetailView();
                invoiceDetailView.Amount = 1500M;
                //invoiceDetailView.InvoiceId = invoice.InvoiceId;


                invoiceDetailView.UnitOfMeasure = "Project";
                invoiceDetailView.Quantity = 1;
                invoiceDetailView.UnitPrice = 1500M;
                invoiceDetailView.Amount = 1500M;
                invoiceDetailView.DiscountPercent = 0;
                invoiceDetailView.DiscountAmount = 0;
                invoiceDetailView.ItemId = 4;

              
                invoiceView.InvoiceDetailViews.Add(invoiceDetailView);

                InvoiceModule invoiceModule = new InvoiceModule();

               invoiceModule
                          .Invoice.CreateInvoice(invoiceView).Apply()
                          .MergeWithInvoiceNumber(ref invoiceView);
                invoiceModule
                      .InvoiceDetail.CreateInvoiceDetails(invoiceView).Apply();
                invoiceModule
                      .AccountsReceivable.CreateAcctRecFromInvoice(invoiceView).Apply();
                invoiceModule
                       .GeneralLedger.CreateGeneralLedger(invoiceView).Apply().UpdateLedgerBalances();

                //bool result=await invoiceModule.PostInvoiceAndDetailToAcctRec(invoiceView);

                Assert.True(true);
            }
            catch (Exception ex)
            {
                throw new Exception("TestPostInvoiceAndDetailToAcctRec", ex);
            }

        }
                 
    
    }
}
