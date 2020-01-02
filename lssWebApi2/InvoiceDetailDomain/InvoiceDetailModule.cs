using lssWebApi2.AbstractFactory;
using lssWebApi2.InvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.InvoiceDetailDomain
{
    public class InvoiceDetailModule : AbstractModule
    {
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
    }
}
