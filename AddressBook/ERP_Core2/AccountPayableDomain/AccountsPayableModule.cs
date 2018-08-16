using ERP_Core2.AbstractFactory;
using MillenniumERP.PurchaseOrderDomain;
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
        UnitOfWork unitOfWork = new UnitOfWork();
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
