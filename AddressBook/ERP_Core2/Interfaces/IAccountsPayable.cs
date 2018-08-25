using ERP_Core2.AccountPayableDomain;
using MillenniumERP.PackingSlipDomain;
using MillenniumERP.PurchaseOrderDomain;
using MillenniumERP.SupplierInvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IAccountsPayable
    {
        IPackingSlip PackingSlip();
        IPurchaseOrder PurchaseOrder();
        ISupplierLedger SupplierLedger();

       
    }
}
