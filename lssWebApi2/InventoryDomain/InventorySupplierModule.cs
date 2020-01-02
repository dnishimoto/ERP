using lssWebApi2.AddressBookDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.SupplierDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.InventoryDomain
{
    public class InventorySupplierModule
    {
        //public FluentInventorySupplier Supplier = new FluentInventorySupplier();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentLocationAddress LocationAddress = new FluentLocationAddress();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentEmail Email = new FluentEmail();


        public bool CreateSupplier(AddressBook addressBook, EmailEntity emailEntity,LocationAddress locationAddress)
        {
            try
            {
                AddressBook
                    .CreateSupplierAddressBook(addressBook, emailEntity)
                    .Apply();

                Task<AddressBook> addressBookLookupTask = Task.Run(async()=>await AddressBook.Query().GetAddressBookbyEmail(emailEntity.Email));

                locationAddress.AddressId = addressBookLookupTask.Result.AddressId;

                LocationAddress

                    .AddLocationAddress(locationAddress)
                    .Apply();

                Email.CreateSupplierEmail(addressBook.AddressId, emailEntity).Apply();

                Supplier supplier = new Supplier { AddressId = addressBook.AddressId, Identification = emailEntity.Email };

                Supplier
                    .AddSupplier(supplier)
                    .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateSupplier", ex); }
        }


    }
}
