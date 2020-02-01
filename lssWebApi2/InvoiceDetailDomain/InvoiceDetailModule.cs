using lssWebApi2.AbstractFactory;
using lssWebApi2.InvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.ItemMasterDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.InvoiceDetailDomain
{
    public class InvoiceDetailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentInvoiceDetail InvoiceDetail;
        public FluentItemMaster ItemMaster;
        public FluentInvoice Invoice;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentSupplier Supplier;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;

        public InvoiceDetailModule()
        {
            unitOfWork = new UnitOfWork();
            InvoiceDetail = new FluentInvoiceDetail(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }

    }
}
