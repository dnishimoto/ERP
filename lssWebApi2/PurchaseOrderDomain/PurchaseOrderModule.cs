using lssWebApi2.AbstractFactory;
using lssWebApi2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ContractDomain;
using lssWebApi2.POQuoteDomain;
using lssWebApi2.BuyerDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.PurchaseOrderDomain
{
    public class PurchaseOrderModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentChartOfAccount ChartOfAccount;
        public FluentSupplier Supplier;
        public FluentContract Contract;
        public FluentAddressBook AddressBook;
        public FluentPOQuote POQuote;
        public FluentBuyer Buyer;
        public FluentTaxRatesByCode TaxRatesByCode;

        public PurchaseOrderModule()
        {
            unitOfWork = new UnitOfWork();
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            POQuote = new FluentPOQuote(unitOfWork);
            Buyer = new FluentBuyer(unitOfWork);
            TaxRatesByCode = new FluentTaxRatesByCode(unitOfWork);
        }


    }
}
