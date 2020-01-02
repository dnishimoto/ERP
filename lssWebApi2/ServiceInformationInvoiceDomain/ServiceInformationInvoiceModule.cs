using lssWebApi2.AbstractFactory;
using lssWebApi2.ServiceInformationInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ServiceInformationDomain;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{
    public class ServiceInformationInvoiceModule : AbstractModule
    {
        public FluentServiceInformationInvoice ServiceInformationInvoice = new FluentServiceInformationInvoice();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentServiceInformation ServiceInformation= new FluentServiceInformation();
    }
}
