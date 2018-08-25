using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
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
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace MillenniumERP.AccountsReceivableDomain
{
    public interface IAccountsReceivableModule
    {
        IAccountsReceivableModule CreateGeneralLedger(GeneralLedgerView ledgerView);
        IAccountsReceivableModule Apply();
        IAccountsReceivableModule CreateCustomerLedger(GeneralLedgerView ledgerView);
        IAccountsReceivableModule UpdateAccountReceivable(GeneralLedgerView ledgerView);
        IAccountsReceivableModule UpdateAccountBalances(GeneralLedgerView ledgerView);
    }

    public class FluentCustomerCashPayment: AbstractErrorHandling, IAccountsReceivableModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public IAccountsReceivableModule CreateGeneralLedger(GeneralLedgerView ledgerView)
        {
            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.generalLedgerRepository.CreateLedgerFromView(ledgerView));
                Task.WaitAll(statusTask);
                processStatus = statusTask.Result;

                return this as IAccountsReceivableModule;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IAccountsReceivableModule Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted || processStatus == CreateProcessStatus.Updated || processStatus == CreateProcessStatus.Deleted)
            {
                unitOfWork.CommitChanges();
            }
            return this as IAccountsReceivableModule;
        }
        public IAccountsReceivableModule CreateCustomerLedger(GeneralLedgerView ledgerView)
        {

            CustomerLedgerView customerLedgerView = new CustomerLedgerView(ledgerView);

            //Get the AcctRecId
            Task<AcctRec> acctRecTask = Task.Run(() => unitOfWork.accountReceiveableRepository.GetAcctRecByDocNumber(ledgerView.DocNumber));
            Task.WaitAll(acctRecTask);

            if (acctRecTask.Result != null)
            {
                customerLedgerView.AcctRecId = acctRecTask.Result.AcctRecId;
                customerLedgerView.InvoiceId = acctRecTask.Result.InvoiceId;
                customerLedgerView.CustomerId = acctRecTask.Result.CustomerId;
                customerLedgerView.GeneralLedgerId = ledgerView.GeneralLedgerId;

                Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.customerLedgerRepository.CreateLedgerFromView(customerLedgerView));
                Task.WaitAll(statusTask);
                processStatus = statusTask.Result;
            }
            return this as IAccountsReceivableModule;
        }
        public IAccountsReceivableModule UpdateAccountReceivable(GeneralLedgerView ledgerView)
        {
            //Update receivable (today) (check for discount rules)
            Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.accountReceiveableRepository.UpdateReceivableByCashLedger(ledgerView));
            Task.WaitAll(statusTask);
            processStatus = statusTask.Result;
            return this as IAccountsReceivableModule;
        }
        public IAccountsReceivableModule UpdateAccountBalances(GeneralLedgerView ledgerView)
        {
            Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerView.AccountId, ledgerView.FiscalYear, ledgerView.FiscalPeriod));
            Task.WaitAll(resultTask);
            return this as IAccountsReceivableModule;
        }
    }
    public class AccountsReceivableModule : AbstractModule
    {

        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();

    }
}
