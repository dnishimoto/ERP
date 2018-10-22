using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IInventorySupplier
    {
        IInventorySupplier CreateSupplierAddressBook(AddressBook addressBook, Emails email);
        IInventorySupplier CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress);
        IInventorySupplier CreateSupplierEmail(long addressId, Emails email);
        IInventorySupplier CreateSupplier(Supplier supplier);
        IInventorySupplier Apply();
        IInventorySupplierQuery Query();
    }
}
