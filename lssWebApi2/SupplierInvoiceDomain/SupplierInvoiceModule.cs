using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.SupplierInvoiceDomain
{
    public class SupplierInvoiceModule : AbstractModule
    {
        public FluentSupplierInvoice SupplierInvoice = new FluentSupplierInvoice();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentInvoice Invoice = new FluentInvoice();
    }
}
