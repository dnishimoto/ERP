using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.InvoiceDomain
{
    public interface IFluentInvoice
    {
        //IFluentInvoice CreateInvoice(InvoiceView invoiceView);
        IFluentInvoice Apply();
        IFluentInvoice AddInvoice(List<Invoice> newObjects);
        IFluentInvoice UpdateInvoice(IList<Invoice> newObjects);
        IFluentInvoice AddInvoice(Invoice newObject);
        IFluentInvoice UpdateInvoice(Invoice updateObject);
        IFluentInvoice DeleteInvoice(Invoice deleteObject);
        IFluentInvoice DeleteInvoice(List<Invoice> deleteObjects);
        IFluentInvoiceQuery Query();
    }
}
