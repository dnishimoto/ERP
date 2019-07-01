using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.InventoryDomain
{
    public interface IFluentInventorySupplier
    {
        IFluentInventorySupplier CreateSupplierAddressBook(AddressBook addressBook, Emails email);
        IFluentInventorySupplier CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress);
        IFluentInventorySupplier CreateSupplierEmail(long addressId, Emails email);
        IFluentInventorySupplier CreateSupplier(Supplier supplier);
        IFluentInventorySupplier Apply();
        IFluentInventorySupplierQuery Query();
    }
}
