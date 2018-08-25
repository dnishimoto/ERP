using ERP_Core2.AbstractFactory;
using MillenniumERP.AccountsPayableDomain;
using MillenniumERP.PurchaseOrderDomain;
using MillenniumERP.PackingSlipDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MillenniumERP.PurchaseOrderDomain.PurchaseOrderRepository;
using MillenniumERP.SupplierInvoicesDomain;
using ERP_Core2.Interfaces;

namespace ERP_Core2.AccountPayableDomain
{
    public enum CreateProcessStatus
    {
        Inserted,
        Created,
        AlreadyExists,
        Updated,
        Deleted,
        Failed
    }

    public interface IPackingSlip
    {
        IPackingSlip CreatePackingSlip(PackingSlipView packingSlipView);

        IPackingSlip CreatePackingSlipDetails(PackingSlipView packingSlipView);
        IPackingSlip CreateInventoryByPackingSlip(PackingSlipView packingSlipView);
        IPackingSlip Apply();
    }

    public class FluentPackingSlip : AbstractErrorHandling, IPackingSlip
    {
         private CreateProcessStatus processStatus;
         UnitOfWork unitOfWork = new UnitOfWork();
        public IPackingSlip Apply()
        {
            if ((processStatus == CreateProcessStatus.Inserted) || (processStatus == CreateProcessStatus.Updated) || (processStatus == CreateProcessStatus.Deleted))
            {
                unitOfWork.CommitChanges();
            }
            return this as IPackingSlip;

        }

        public IPackingSlip CreatePackingSlip(PackingSlipView packingSlipView)
    {
        Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.packingSlipRepository.CreatePackingSlipByView(packingSlipView));
        Task.WaitAll(resultTask);
        processStatus = resultTask.Result;
        return this as IPackingSlip;
    }
    public IPackingSlip CreatePackingSlipDetails(PackingSlipView packingSlipView)
    {
        Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.packingSlipRepository.CreatePackingSlipDetailsByView(packingSlipView));
        Task.WaitAll(resultTask);
        processStatus = resultTask.Result;
        return this as IPackingSlip;
    }
    public IPackingSlip CreateInventoryByPackingSlip(PackingSlipView packingSlipView)
    {
        Task<PackingSlipView> lookupViewTask = Task.Run(() => unitOfWork.packingSlipRepository.GetPackingSlipViewBySlipDocument(packingSlipView.SlipDocument));
        Task.WaitAll(lookupViewTask);
        Task<CreateProcessStatus> resultTask = unitOfWork.inventoryRepository.CreateInventoryByPackingSlipView(lookupViewTask.Result);
        Task.WaitAll(resultTask);
        processStatus = resultTask.Result;
        return this as IPackingSlip;
    }
}

    public interface IPurchaseOrder
    {
        IPurchaseOrder CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder CreatePurchaseOrder(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder Apply();
    }

    public class FluentPurchaseOrder: AbstractErrorHandling,IPurchaseOrder
    {
        private CreateProcessStatus processStatus;
        UnitOfWork unitOfWork = new UnitOfWork();

        public IPurchaseOrder Apply()
        {
            if ((processStatus == CreateProcessStatus.Inserted) || (processStatus == CreateProcessStatus.Updated) || (processStatus == CreateProcessStatus.Deleted))
            {
                unitOfWork.CommitChanges();
            }
            return this as IPurchaseOrder;

        }
        public IPurchaseOrder CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.accountPayableRepository.CreateAcctPayByPurchaseOrderView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPurchaseOrder;
        }
        public IPurchaseOrder CreatePurchaseOrder(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPurchaseOrder;
        }
        public IPurchaseOrder CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderDetailsByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPurchaseOrder;
        }

    }
    public interface ISupplierLedger
    {
        ISupplierLedger CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        ISupplierLedger CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView);
        ISupplierLedger Apply();
    }

    public class FluentSupplierLedger: AbstractErrorHandling,ISupplierLedger
    {
        private CreateProcessStatus processStatus;
        UnitOfWork unitOfWork = new UnitOfWork();

        public ISupplierLedger Apply()
        {
            if ((processStatus == CreateProcessStatus.Inserted) || (processStatus == CreateProcessStatus.Updated) || (processStatus == CreateProcessStatus.Deleted))
            {
                unitOfWork.CommitChanges();
            }
            return this as ISupplierLedger;

        }
        public ISupplierLedger CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as ISupplierLedger;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public ISupplierLedger CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceDetailsByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as ISupplierLedger;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }


        }
    }
    public class AccountsPayableModule : AbstractModule, IAccountsPayable
    {

        public IPackingSlip PackingSlip()
        {
            return new FluentPackingSlip() as IPackingSlip;
        }

        public IPurchaseOrder PurchaseOrder()
        {
            return new FluentPurchaseOrder() as IPurchaseOrder;
        }
        public ISupplierLedger SupplierLedger()
        {
            return new FluentSupplierLedger() as ISupplierLedger;
        }


    
            
       
    }
}
