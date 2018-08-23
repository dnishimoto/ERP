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
        IAccountsPayable CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        IAccountsPayable CreateSupplierInvoiceDetail(SupplierInvoiceView supplierInvoiceView);
        IAccountsPayable CreatePurchaseOrder(PurchaseOrderView purchaseOrderView);
        IAccountsPayable CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView);
        IAccountsPayable CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView);
        IAccountsPayable CreatePackingSlip(PackingSlipView packingSlipView);
        IAccountsPayable CreatePackingSlipDetails(PackingSlipView packingSlipView);
        IAccountsPayable CreateInventoryByPackingSlip(PackingSlipView packingSlipView);
        IAccountsPayable Apply();
    }
}
