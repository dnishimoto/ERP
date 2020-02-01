using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{
    public class ServiceInformationInvoiceModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentServiceInformationInvoice ServiceInformationInvoice;
        public FluentInvoice Invoice;
        public FluentServiceInformation ServiceInformation;

        public ServiceInformationInvoiceModule()
        {
            unitOfWork = new UnitOfWork();
            ServiceInformationInvoice = new FluentServiceInformationInvoice(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            ServiceInformation = new FluentServiceInformation(unitOfWork);
        }
    }
}
