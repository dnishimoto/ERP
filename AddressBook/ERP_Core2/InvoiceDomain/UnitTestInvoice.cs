﻿using System.Linq;
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
using MillenniumERP.InvoicesDomain;
using MillenniumERP.InvoiceDetailsDomain;

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
        public async Task TestPostInvoiceAndDetailToAcctRec()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                //NextNumber nextNumber = await unitOfWork.invoiceRepository.GetNextNumber("InvoiceNumber");

                InvoiceView invoiceView = new InvoiceView();

                invoiceView.InvoiceNumber = "Inv-03";
                invoiceView.InvoiceDate = DateTime.Today.Date;
                invoiceView.Amount = 1500.0M;
                invoiceView.CustomerId = 9;
                invoiceView.Description = "VNW Fixed Asset project";
                invoiceView.PaymentTerms = "Net 30";
                invoiceView.TaxAmount = 0;
                invoiceView.CompanyId = 1;

                Invoice invoice = new Invoice();

                unitOfWork.invoiceRepository.applicationViewFactory.MapInvoiceEntity(ref invoice, invoiceView);

                bool result = await unitOfWork.invoiceRepository.AddInvoice(invoice);
                if (result) { unitOfWork.CommitChanges(); }
                List<Invoice> list = unitOfWork.invoiceRepository.GetObjectsAsync(e => e.InvoiceNumber == invoice.InvoiceNumber).ToList<Invoice>();
                invoice.InvoiceId = list[0].InvoiceId;


                InvoiceDetailView invoiceDetailView = new InvoiceDetailView();
                invoiceDetailView.Amount = 1500M;
                invoiceDetailView.InvoiceId = invoice.InvoiceId;


                invoiceDetailView.UnitOfMeasure = "Project";
                invoiceDetailView.Quantity = 1;
                invoiceDetailView.UnitPrice = 1500M;
                invoiceDetailView.Amount = 1500M;
                invoiceDetailView.DiscountPercent = 0;
                invoiceDetailView.DiscountAmount = 0;
                invoiceDetailView.ItemId = 4;

                InvoiceDetail invoiceDetail = new InvoiceDetail();
                unitOfWork.invoiceRepository.applicationViewFactory.MapInvoiceDetailEntity(ref invoiceDetail, invoiceDetailView);

                bool result2 = await unitOfWork.invoiceDetailRepository.AddInvoiceDetail(invoiceDetail);
                if (result2) { unitOfWork.CommitChanges(); }

                bool result3 = await unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoice);
                //unitOfWork.invoiceRepository.DeleteInvoice(invoice);
                if (result3)
                {
                    unitOfWork.CommitChanges();
                }

                AccountReceiveableView acctRecView = await unitOfWork.accountReceiveableRepository.GetAccountReceivableViewByInvoiceId(invoice.InvoiceId);

                if (acctRecView != null)
                {
                    bool result4 = await unitOfWork.generalLedgerRepository.CreateLedgerFromReceiveable(acctRecView);
                    if (result4)
                    {
                        unitOfWork.CommitChanges();
                    }
                    GeneralLedgerView ledger = await unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(acctRecView.DocNumber, "OV");
                    bool result5 = await unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledger.AccountId, ledger.FiscalYear, ledger.FiscalPeriod);
                }

                Assert.True(true);
            }
            catch (Exception ex)
            {

            }

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
                List<Invoice> list = unitOfWork.invoiceRepository.GetObjectsAsync(e => e.InvoiceNumber == invoice.InvoiceNumber).ToList<Invoice>();
                invoice.InvoiceId = list[0].InvoiceId;
                bool result2 = await unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoice);
                //unitOfWork.invoiceRepository.DeleteInvoice(invoice);
                if (result2) { unitOfWork.CommitChanges(); }
                

                AccountReceiveableView acctRecView = await unitOfWork.accountReceiveableRepository.GetAccountReceivableViewByInvoiceId(invoice.InvoiceId);

                if (acctRecView != null)
                {
                    bool result3 = await unitOfWork.generalLedgerRepository.CreateLedgerFromReceiveable(acctRecView);

                    if (result3) { unitOfWork.CommitChanges(); }


                    GeneralLedgerView ledger = await unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(acctRecView.DocNumber, "OV");
                    bool result4 = await unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledger.AccountId, ledger.FiscalYear, ledger.FiscalPeriod);
                }


                Assert.True(true);
            }
            catch (Exception ex)
            {

            }
        }
            
    
    }
}
