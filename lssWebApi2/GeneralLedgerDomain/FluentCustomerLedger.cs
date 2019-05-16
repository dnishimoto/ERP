using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.Services;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomerLedger : AbstractErrorHandling, IFluentCustomerLedger
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public IFluentCustomerLedger Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            {
                unitOfWork.CommitChanges();
            }
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger CreateCustomerLedger(GeneralLedgerView ledgerView)
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
            return this as IFluentCustomerLedger;
        }
    }
}
