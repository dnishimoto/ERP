using lssWebApi2.AbstractFactory;
using lssWebApi2.PurchaseOrderDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.PurchaseOrderDetailDomain
{
    public class PurchaseOrderDetailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentItemMaster ItemMaster;
        public FluentSupplier Supplier;
        public FluentAddressBook AddressBook;

        public PurchaseOrderDetailModule()
        {
            unitOfWork = new UnitOfWork();
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
