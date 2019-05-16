using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using ERP_Core2.SupplierInvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentSupplierLedger : AbstractErrorHandling, IFluentSupplierLedger
    {
        private CreateProcessStatus processStatus;
        UnitOfWork unitOfWork = new UnitOfWork();

        public IFluentSupplierLedger Apply()
        {
            if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
            {
                unitOfWork.CommitChanges();
            }
            return this as IFluentSupplierLedger;

        }
        public IFluentSupplierLedger CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as IFluentSupplierLedger;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public IFluentSupplierLedger CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.supplierInvoiceRepository.CreateSupplierInvoiceDetailsByView(supplierInvoiceView));
                Task.WaitAll(resultTask);
                processStatus = resultTask.Result;
                return this as IFluentSupplierLedger;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }


        }
    }
}
