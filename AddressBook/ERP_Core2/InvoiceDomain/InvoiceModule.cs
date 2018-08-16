using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.InvoiceDomain
{
    public class InvoiceModule : AbstractModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<bool> PostInvoiceAndDetailToAcctRec(InvoiceView invoiceView)
        {

            try
            {
                Invoice invoice = new Invoice();

                unitOfWork.invoiceRepository.applicationViewFactory.MapInvoiceEntity(ref invoice, invoiceView);

                bool result = await unitOfWork.invoiceRepository.AddInvoice(invoice);
                if (result) { unitOfWork.CommitChanges(); }
                List<Invoice> list = unitOfWork.invoiceRepository.GetObjectsQueryable(e => e.InvoiceNumber == invoice.InvoiceNumber).ToList<Invoice>();
                invoice.InvoiceId = list[0].InvoiceId;

                //Assign the InvoiceId
                for (int i = 0; i < invoiceView.InvoiceDetailViews.Count; i++)
                {
                    invoiceView.InvoiceDetailViews[i].InvoiceId = invoice.InvoiceId;
                    InvoiceDetail invoiceDetail = new InvoiceDetail();
                    unitOfWork.invoiceRepository.applicationViewFactory.MapInvoiceDetailEntity(ref invoiceDetail, invoiceView.InvoiceDetailViews[i]);

                    bool result2 = await unitOfWork.invoiceDetailRepository.AddInvoiceDetail(invoiceDetail);
                    if (result2) { unitOfWork.CommitChanges(); }

                }

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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
