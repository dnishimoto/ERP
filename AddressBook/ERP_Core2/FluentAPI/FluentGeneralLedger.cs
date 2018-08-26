using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.FluentAPI
{
    public class FluentGeneralLedger : AbstractErrorHandling, IGeneralLedger
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public AccountReceiveableView lastAccountReceivableView;
        public CreateProcessStatus processStatus;

        public FluentGeneralLedger() { }
        public IGeneralLedger UpdateAccountBalances(GeneralLedgerView ledgerView)
        {
            Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerView.AccountId, ledgerView.FiscalYear, ledgerView.FiscalPeriod));
            Task.WaitAll(resultTask);
            return this as IGeneralLedger;
        }
        public IGeneralLedger UpdateLedgerBalances()
        {

            Task<GeneralLedgerView> ledgerTask = Task.Run(() => unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(lastAccountReceivableView.DocNumber, "OV"));
            Task.WaitAll(ledgerTask);
            Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerTask.Result.AccountId, ledgerTask.Result.FiscalYear, ledgerTask.Result.FiscalPeriod));
            Task.WaitAll(resultTask);
            return this as IGeneralLedger;
        }
        public IGeneralLedger CreateGeneralLedger(GeneralLedgerView ledgerView)
        {
            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.generalLedgerRepository.CreateLedgerFromView(ledgerView));
                Task.WaitAll(statusTask);
                processStatus = statusTask.Result;

                return this as IGeneralLedger;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IGeneralLedger CreateGeneralLedger(InvoiceView invoiceView)
        {
            try
            {

                Task<AccountReceiveableView> acctRecViewTask = Task.Run(() => unitOfWork.accountReceiveableRepository.GetAccountReceivableViewByInvoiceId(invoiceView.InvoiceId));
                Task.WaitAll(acctRecViewTask);
                if (acctRecViewTask.Result != null)
                {

                    Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.CreateLedgerFromReceiveable(acctRecViewTask.Result));
                    Task.WaitAll(resultTask);
                    if (resultTask.Result == true)
                    {
                        processStatus = CreateProcessStatus.Inserted;
                        return this as IGeneralLedger;
                    }

                }
                processStatus = CreateProcessStatus.AlreadyExists;
                return this as IGeneralLedger;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }



        public IGeneralLedger Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted || processStatus == CreateProcessStatus.Updated || processStatus == CreateProcessStatus.Deleted)
            { unitOfWork.CommitChanges(); }
            return this as IGeneralLedger;
        }
    }
}
