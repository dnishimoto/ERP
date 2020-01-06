using lssWebApi2.AbstractFactory;
using lssWebApi2.ContractInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.ContractInvoiceDomain
{
    public class ContractInvoiceModule : AbstractModule
    {
        public FluentContractInvoice ContractInvoice = new FluentContractInvoice();
    }
}
