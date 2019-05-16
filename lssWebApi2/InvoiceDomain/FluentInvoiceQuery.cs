using ERP_Core2.AbstractFactory;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInvoiceQuery : AbstractErrorHandling, IFluentInvoiceQuery
    {
        public UnitOfWork _unitOfWork = null;
        public FluentInvoiceQuery() { }
        public FluentInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate)
        {
            return _unitOfWork.invoiceRepository.GetInvoicesByDate(startInvoiceDate, endInvoiceDate);
        }

    }
}
