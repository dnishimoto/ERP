using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using lssWebApi2.FluentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class SupplierModule
    {
        public FluentInventorySupplier Supplier = new FluentInventorySupplier();
    }
}
