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

namespace ERP_Core2.AccountPayableDomain
{
    public class AccountsPayableModule : AbstractModule
    {
        public enum PurchaseOrderStatus
        {
            Created,
            AlreadyExists
        }
        public enum PackingSlipStatus
        {
            Created,
            AlreadyExists
        }
        UnitOfWork unitOfWork = new UnitOfWork();
        public async Task<bool> CreatePackingSlipByView(PackingSlipView packingSlipView)
        {
            try
            {
                PackingSlipStatus result2 = await unitOfWork.packingSlipRepository.CreatePackingSlipByView(packingSlipView);
                if (result2 == PackingSlipStatus.AlreadyExists || result2 == PackingSlipStatus.Created)
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
        public async Task<bool> CreateAccountsPaybyPOView(PurchaseOrderView purchaseOrderView)
        {
            try
            {
                PurchaseOrderStatus result2 = await unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView);

                if (result2 == PurchaseOrderStatus.AlreadyExists || result2 == PurchaseOrderStatus.Created)
                {
                    PurchaseOrderView lookupView = await unitOfWork.purchaseOrderRepository.GetPurchaseOrderViewByOrderNumber(purchaseOrderView.PONumber);
                    bool result3 = await unitOfWork.accountPayableRepository.CreateAcctPayByPurchaseOrderView(lookupView);
                    if (result3)
                    {
                        unitOfWork.CommitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
