

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ServiceInformationInvoiceDomain;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{ 

public interface IFluentServiceInformationInvoice
    {
        IFluentServiceInformationInvoiceQuery Query();
        IFluentServiceInformationInvoice Apply();
        IFluentServiceInformationInvoice AddServiceInformationInvoice(ServiceInformationInvoice serviceInformationInvoice);
        IFluentServiceInformationInvoice UpdateServiceInformationInvoice(ServiceInformationInvoice serviceInformationInvoice);
        IFluentServiceInformationInvoice DeleteServiceInformationInvoice(ServiceInformationInvoice serviceInformationInvoice);
     	IFluentServiceInformationInvoice UpdateServiceInformationInvoices(List<ServiceInformationInvoice> newObjects);
        IFluentServiceInformationInvoice AddServiceInformationInvoices(List<ServiceInformationInvoice> newObjects);
        IFluentServiceInformationInvoice DeleteServiceInformationInvoices(List<ServiceInformationInvoice> deleteObjects);
    }
}
