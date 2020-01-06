

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ContractInvoiceDomain;

namespace lssWebApi2.ContractInvoiceDomain
{ 

public interface IFluentContractInvoice
    {
        IFluentContractInvoiceQuery Query();
        IFluentContractInvoice Apply();
        IFluentContractInvoice AddContractInvoice(ContractInvoice contractInvoice);
        IFluentContractInvoice UpdateContractInvoice(ContractInvoice contractInvoice);
        IFluentContractInvoice DeleteContractInvoice(ContractInvoice contractInvoice);
     	IFluentContractInvoice UpdateContractInvoices(IList<ContractInvoice> newObjects);
        IFluentContractInvoice AddContractInvoices(List<ContractInvoice> newObjects);
        IFluentContractInvoice DeleteContractInvoices(List<ContractInvoice> deleteObjects);
    }
}
