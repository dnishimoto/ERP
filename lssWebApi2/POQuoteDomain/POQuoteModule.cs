using lssWebApi2.AbstractFactory;
using lssWebApi2.POQuoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.SupplierDomain;

namespace lssWebApi2.POQuoteDomain
{
    public class POQuoteModule : AbstractModule
    {
        public FluentPOQuote POQuote = new FluentPOQuote();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
