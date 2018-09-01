using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using MillenniumERP.CustomerLedgerDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentSupplier : AbstractErrorHandling, ISupplier
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentSupplier() { }
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();

        public ISupplier UpdateAccountsPayable(GeneralLedgerView generalLedgerView)
        {
            Task<CreateProcessStatus> statusResultTask = Task.Run(() => unitOfWork.accountPayableRepository.UpdatePayableByLedgerView(generalLedgerView));
            Task.WaitAll(statusResultTask);
            processStatus = statusResultTask.Result;
            return this as ISupplier;
        }
        public ISupplier CreateSupplierLedger(GeneralLedgerView generalLedgerView)
        {
            Task<GeneralLedgerView> generalLedgerTask = Task.Run(() => unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(generalLedgerView.DocNumber, generalLedgerView.DocType));
            Task.WaitAll(generalLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);
            supplierLedgerView.GeneralLedgerId = generalLedgerTask.Result.GeneralLedgerId;


            Task<CreateProcessStatus> statusResultTask = Task.Run(() => unitOfWork.supplierLedgerRepository.CreateSupplierLedgerFromView(supplierLedgerView));
            Task.WaitAll(statusResultTask);
            processStatus = statusResultTask.Result;
            return this as ISupplier;
        }
        public ISupplier UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView)
        {
            Task<SupplierLedgerView> supplierLedgerTask = Task.Run(() => unitOfWork.supplierLedgerRepository.GetSupplierLedgerByDocNumber(generalLedgerView.DocNumber, generalLedgerView.DocType));
            Task.WaitAll(supplierLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);

            supplierLedgerView.SupplierLedgerId = supplierLedgerTask.Result.SupplierLedgerId;

            Task<CreateProcessStatus> statusResultTask = Task.Run(() => unitOfWork.supplierLedgerRepository.UpdateSupplierLedger(supplierLedgerView));
            Task.WaitAll(statusResultTask);
            processStatus = statusResultTask.Result;
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
}
