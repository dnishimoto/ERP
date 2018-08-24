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
   
    public class AccountsPayableModule : AbstractModule, IAccountsPayable
    {
        private CreateProcessStatus processStatus;
      
        public enum CreateProcessStatus
        {
            Inserted,
            Created,
            AlreadyExists,
            Updated,
            Deleted,
            Failed
        }
        UnitOfWork unitOfWork = new UnitOfWork();

     
        public IAccountsPayable CreatePackingSlip(PackingSlipView packingSlipView)
        {
            Task<CreateProcessStatus> resultTask =  Task.Run(()=>unitOfWork.packingSlipRepository.CreatePackingSlipByView(packingSlipView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        }
        public IAccountsPayable CreatePackingSlipDetails(PackingSlipView packingSlipView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.packingSlipRepository.CreatePackingSlipDetailsByView(packingSlipView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        }
        public IAccountsPayable CreateInventoryByPackingSlip(PackingSlipView packingSlipView)
        {
            Task<PackingSlipView> lookupViewTask = Task.Run(()=>unitOfWork.packingSlipRepository.GetPackingSlipViewBySlipDocument(packingSlipView.SlipDocument));
            Task.WaitAll(lookupViewTask);
            Task<CreateProcessStatus> resultTask = unitOfWork.inventoryRepository.CreateInventoryByPackingSlipView(lookupViewTask.Result);
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        } 

        public IAccountsPayable CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.accountPayableRepository.CreateAcctPayByPurchaseOrderView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        }
        public IAccountsPayable CreatePurchaseOrder(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        }
        public IAccountsPayable CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.purchaseOrderRepository.CreatePurchaseOrderDetailsByView(purchaseOrderView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IAccountsPayable;
        }
     
        public IAccountsPayable CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as IAccountsPayable;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
            
        }
        public IAccountsPayable CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceDetailsByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as IAccountsPayable;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            
        }
        public IAccountsPayable Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted)
            {
                unitOfWork.CommitChanges();
             }
            return this as IAccountsPayable;

        }
  

        
       
    }
}
