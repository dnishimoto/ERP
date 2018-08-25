using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
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
    public class FluentAccountsReceivable : AbstractErrorHandling, IAccountsReceivable
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        public FluentAccountsReceivable() { }
        public IAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoiceView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsReceivable;
        }
        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            return query as IQuery;
        }
        public IAccountsReceivable Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted || processStatus == CreateProcessStatus.Updated || processStatus == CreateProcessStatus.Deleted)
            { unitOfWork.CommitChanges(); }
            return this as IAccountsReceivable;
        }
    }
}
