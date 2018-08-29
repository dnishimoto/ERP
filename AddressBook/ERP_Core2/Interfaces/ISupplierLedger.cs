using MillenniumERP.SupplierInvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ISupplierLedger
    {
        ISupplierLedger CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        ISupplierLedger CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView);
        ISupplierLedger Apply();
    }
}
