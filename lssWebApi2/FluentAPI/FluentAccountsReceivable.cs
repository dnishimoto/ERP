using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountPayableModule;
using ERP_Core2.AccountsReceivableDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.FluentAPI;

namespace ERP_Core2.FluentAPI
{
   
   
    public class FluentAccountReceivable : AbstractErrorHandling, IAccountsReceivable
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        public FluentAccountReceivable() { }

        public IQueryAccountReceivable Query()
        {
            FluentAccountReceivableQuery query = new FluentAccountReceivableQuery(unitOfWork);
            return query as IQueryAccountReceivable;
        }

        public IAccountsReceivable UpdateAccountReceivable(GeneralLedgerView ledgerView)
        {
            //Update receivable (today) (check for discount rules)
            Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.accountReceiveableRepository.UpdateReceivableByCashLedger(ledgerView));
            Task.WaitAll(statusTask);
            processStatus = statusTask.Result;
            return this as IAccountsReceivable;
        }
        public IAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoiceView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsReceivable;
        }
     
        public IAccountsReceivable Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IAccountsReceivable;
        }
    }
}
