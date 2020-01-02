using lssWebApi2.AbstractFactory;
using lssWebApi2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ContractDomain;
using lssWebApi2.POQuoteDomain;
using lssWebApi2.BuyerDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.ChartOfAccountsDomain;

namespace lssWebApi2.PurchaseOrderDomain
{
    public class PurchaseOrderModule : AbstractModule
    {
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentContract Contract = new FluentContract();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentPOQuote POQuote = new FluentPOQuote();
        public FluentBuyer Buyer = new FluentBuyer();

       
    }
}
