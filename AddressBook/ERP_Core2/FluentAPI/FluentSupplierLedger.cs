using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using MillenniumERP.Services;
using MillenniumERP.SupplierInvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentSupplierLedger : AbstractErrorHandling, ISupplierLedger
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
}
