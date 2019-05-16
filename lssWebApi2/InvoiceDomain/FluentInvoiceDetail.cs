using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountPayableModule;

namespace ERP_Core2.FluentAPI
{
    public class FluentInvoiceDetail : AbstractErrorHandling, IFluentInvoiceDetail
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;
        public FluentInvoiceDetail() { }


        public IFluentInvoiceDetail CreateInvoiceDetails(InvoiceView invoiceView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.invoiceDetailRepository.CreateInvoiceDetailsByView(invoiceView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentInvoiceDetail;
        }

        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            return query as IQuery;
        }
        public IFluentInvoiceDetail Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentInvoiceDetail;
        }
    }
}
