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

namespace ERP_Core2.AccountPayableDomain
{
    public interface IAccountsPayable
    {
        IAccountsPayable CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        IAccountsPayable CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView);
        IAccountsPayable Apply();
    }


    public class AccountsPayableModule : AbstractModule, IAccountsPayable
    {
        private CreateProcessStatus processStatus;
        public enum CreateProcessStatus
        {
            Inserted,
            Created,
            AlreadyExists,
            Failed
        }
        UnitOfWork unitOfWork = new UnitOfWork();
        public IAccountsPayable CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            return this as IAccountsPayable;
        }
        public IAccountsPayable CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceDetailsByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            return this as IAccountsPayable;
        }
        public IAccountsPayable Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted)
            {
                unitOfWork.CommitChanges();

            }
            return this as IAccountsPayable;
        }
        public async Task<bool> CreatePackingSlipByView(PackingSlipView packingSlipView)
        {
            try
            {
                CreateProcessStatus result2 = await unitOfWork.packingSlipRepository.CreatePackingSlipByView(packingSlipView);
                if (result2 == CreateProcessStatus.AlreadyExists || result2 == CreateProcessStatus.Created)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateInventoryByPackingSlipView(PackingSlipView packingSlipView)
        {

            bool result3 = await unitOfWork.inventoryRepository.CreateInventoryByPackingSlipView(packingSlipView);
            if (result3)
            {
                unitOfWork.CommitChanges();
            }
            return true;
        }
        public async Task<PackingSlipView> GetPackingSlipViewBySlipDocument(string slipDocument)
        {
            PackingSlipView lookupView = await unitOfWork.packingSlipRepository.GetPackingSlipViewBySlipDocument(slipDocument);
            return lookupView;
        }
        public async Task<bool> CreateAcctPayByPurchaseOrderNumber(string poNumber)
        {
            try
            {
                PurchaseOrderView lookupView = await unitOfWork.purchaseOrderRepository.GetPurchaseOrderViewByOrderNumber(poNumber);
                bool result = await unitOfWork.accountPayableRepository.CreateAcctPayByPurchaseOrderView(lookupView);
                if (result)
                {
                    unitOfWork.CommitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<bool> CreateAccountsPaybyPOView(PurchaseOrderView purchaseOrderView)
        {
            try
            {
                CreateProcessStatus result2 = await unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView);


                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
