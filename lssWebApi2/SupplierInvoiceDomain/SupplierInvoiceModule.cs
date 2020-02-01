using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;

namespace lssWebApi2.SupplierInvoiceDomain
{
    public class SupplierInvoiceModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSupplierInvoice SupplierInvoice;
        public FluentSupplier Supplier;
        public FluentAddressBook AddressBook;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentInvoice Invoice;


        public SupplierInvoiceModule()
        {
            unitOfWork = new UnitOfWork();
            SupplierInvoice = new FluentSupplierInvoice(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
        }
    }
}
