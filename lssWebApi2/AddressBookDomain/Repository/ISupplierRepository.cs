using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain.Repository
{
    public interface ISupplierRepository
    {
        AddressBook GetAddressBookByEmail(Emails email);
        Task<SupplierView> GetSupplierViewBySupplierId(long supplierId);
        Task<CreateProcessStatus> CreateSupplierAddressBook(AddressBook addressBook, Emails email);
        Task<CreateProcessStatus> CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress);
        Task<CreateProcessStatus> CreateSupplierEmail(long addressId, Emails email);
        Task<CreateProcessStatus> CreateSupplier(Supplier supplier);
        Task<SupplierView> GetSupplierViewByEmail(Emails email);
        Task<SupplierView> CreateSupplierByAddressBook(AddressBook addressBook, LocationAddress locationAddress, Emails email);
    }
}
