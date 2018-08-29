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
using ERP_Core2.FluentAPI;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.CustomerLedgerDomain;
using ERP_Core2.EntityFramework;

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

    public interface ISupplier
    {
        ISupplier UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView);
        ISupplier CreateSupplierLedger(GeneralLedgerView generalLedger);
        ISupplier Apply();
    }
    public class FluentSupplier : AbstractErrorHandling, ISupplier
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentSupplier() { }
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();

        public ISupplier CreateSupplierLedger(GeneralLedgerView generalLedgerView)
        {
            Task<GeneralLedgerView> generalLedgerTask = Task.Run(() => unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(generalLedgerView.DocNumber,generalLedgerView.DocType));
            Task.WaitAll(generalLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);
            supplierLedgerView.GeneralLedgerId = generalLedgerTask.Result.GeneralLedgerId;


            Task<CreateProcessStatus> statusResult = Task.Run(() => unitOfWork.supplierLedgerRepository.CreateSupplierLedgerFromView(supplierLedgerView));
            Task.WaitAll(statusResult);
            processStatus = statusResult.Result;
            return this as ISupplier;
        }
        public ISupplier UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView)
        {
            Task<SupplierLedgerView> supplierLedgerTask = Task.Run(() => unitOfWork.supplierLedgerRepository.GetSupplierLedgerByDocNumber(generalLedgerView.DocNumber, generalLedgerView.DocType));
            Task.WaitAll(supplierLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);

            supplierLedgerView.SupplierLedgerId = supplierLedgerTask.Result.SupplierLedgerId;

            Task<CreateProcessStatus> statusResult = Task.Run(() => unitOfWork.supplierLedgerRepository.UpdateSupplierLedger(supplierLedgerView));
            Task.WaitAll(statusResult);
            processStatus = statusResult.Result;
            return this as ISupplier;
        }
        public ISupplier Apply()
        {
            try
            {
                if ((processStatus == CreateProcessStatus.Inserted) || (processStatus == CreateProcessStatus.Updated) || (processStatus == CreateProcessStatus.Deleted))
                {
                    unitOfWork.CommitChanges();
                }
                return this as ISupplier;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
          
        }
    }
    public class AccountsPayableModule : AbstractModule
    {
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();

    }
}
