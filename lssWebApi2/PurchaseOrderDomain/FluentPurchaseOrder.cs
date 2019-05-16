using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.PurchaseOrderDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentPurchaseOrder : AbstractErrorHandling, IFluentPurchaseOrder
    {
        private CreateProcessStatus processStatus;
        UnitOfWork unitOfWork = new UnitOfWork();

        public IFluentPurchaseOrder Apply()
        {
            if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
            {
                unitOfWork.CommitChanges();
            }
            return this as IFluentPurchaseOrder;

        }
        public IFluentPurchaseOrder CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.accountPayableRepository.CreateAcctPayByPurchaseOrderView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentPurchaseOrder;
        }
        public IFluentPurchaseOrder CreatePurchaseOrder(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentPurchaseOrder;
        }
        public IFluentPurchaseOrder CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderDetailsByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentPurchaseOrder;
        }

    }
}
