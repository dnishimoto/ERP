using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.CustomerLedgerDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.AccountsReceivableDomain
{
   
    public class AccountsReceivable : AbstractModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<bool> CustomerCashPayment(GeneralLedgerView ledgerView)
        {
            try
            {
                long generalLedgerId = await unitOfWork.generalLedgerRepository.CreateLedgerFromView(ledgerView);

                ledgerView.GeneralLedgerId = generalLedgerId;

                CustomerLedgerView customerLedgerView = new CustomerLedgerView(ledgerView);

                //Get the AcctRecId
                AcctRec acctRec = await unitOfWork.accountReceiveableRepository.GetAcctRecByDocNumber(ledgerView.DocNumber);
                if (acctRec != null)
                {
                    customerLedgerView.AcctRecId = acctRec.AcctRecId;
                    customerLedgerView.InvoiceId = acctRec.InvoiceId;
                    customerLedgerView.CustomerId = acctRec.CustomerId;
                    customerLedgerView.GeneralLedgerId = ledgerView.GeneralLedgerId;

                    long customerLedgerId = await unitOfWork.customerLedgerRepository.CreateLedgerFromView(customerLedgerView);
                }

                //Update receivable (today) (check for discount rules)
                bool result2 = await unitOfWork.accountReceiveableRepository.UpdateReceivableByCashLedger(ledgerView);
                if (result2) { unitOfWork.CommitChanges(); }
                bool result3 = await unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerView.AccountId, ledgerView.FiscalYear, ledgerView.FiscalPeriod);

                return result3;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
