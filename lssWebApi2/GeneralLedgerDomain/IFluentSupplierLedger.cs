using ERP_Core2.SupplierInvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentSupplierLedger
    {
        IFluentSupplierLedger CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        IFluentSupplierLedger CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView);
        IFluentSupplierLedger Apply();
    }
}
