using lssWebApi2.AbstractFactory;
using lssWebApi2.POQuoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.POQuoteDomain
{
    public class POQuoteModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPOQuote POQuote;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentCustomer Customer;
        public FluentSupplier Supplier;
        public FluentAddressBook AddressBook;

        public POQuoteModule()
        {
            unitOfWork = new UnitOfWork();
            POQuote = new FluentPOQuote(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
