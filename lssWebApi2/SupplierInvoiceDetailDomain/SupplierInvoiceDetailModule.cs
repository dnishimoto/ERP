using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierInvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{
    public class SupplierInvoiceDetailModule : AbstractModule
    {
        public FluentSupplierInvoiceDetail SupplierInvoiceDetail = new FluentSupplierInvoiceDetail();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentSupplierInvoice SupplierInvoice = new FluentSupplierInvoice();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
    }
}
