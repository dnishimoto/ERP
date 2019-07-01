using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
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

        public bool CreateSupplier(AddressBook addressBook, Emails email,LocationAddress locationAddress)
        {
            try
            {
                Supplier
                    .CreateSupplierAddressBook(addressBook, email)
                    .Apply();
                AddressBook ab = Supplier.Query().GetAddressBookbyEmail(email);
                Supplier
                    .CreateSupplierLocationAddress(ab.AddressId, locationAddress)
                    .Apply();
                Supplier.CreateSupplierEmail(ab.AddressId, email).Apply();

                Supplier supplier = new Supplier { AddressId = ab.AddressId, Identification = email.Email };

                Supplier
                    .CreateSupplier(supplier)
                    .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateSupplier", ex); }
        }


    }
}
