using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.SupplierInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDomain
{
    public interface IAccountPayableModule
    {
        bool CreateByPurchaseOrderView(PurchaseOrderView purchaseOrderView);
        bool CreatePackingSlip(PackingSlipView packingSlipView);
        bool CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView);
        bool CreateAccountPayable(GeneralLedgerView ledgerView);
 
    }
}
