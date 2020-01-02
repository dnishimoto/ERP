using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.SupplierDomain
{
    public class SupplierModule : AbstractModule
    {
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmail Emails = new FluentEmail();
        public FluentAccountPayable AccountPayable = new FluentAccountPayable();
        public FluentUdc Udc = new FluentUdc();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
        public FluentLocationAddress LocationAddress = new FluentLocationAddress();

        public bool CreateSupplierProfile(AddressBook addressBook, EmailEntity emailEntity, LocationAddress locationAddress)

        {

            try

            {

                AddressBook

                    .CreateSupplierAddressBook(addressBook, emailEntity).Apply();

               Task<AddressBook> addressBookLookupTask = Task.Run(async()=>await AddressBook.Query().GetAddressBookbyEmail(emailEntity.Email));
                Task.WaitAll(addressBookLookupTask);

                LocationAddress

                    .CreateSupplierLocationAddress(addressBookLookupTask.Result.AddressId, locationAddress).Apply();

                Emails
                    .CreateEmailByAddressId(addressBookLookupTask.Result.AddressId, emailEntity).Apply();


                Supplier supplier = new Supplier { AddressId = addressBookLookupTask.Result.AddressId, Identification = emailEntity.Email };



                Supplier

                    .CreateSupplier(supplier)

                    .Apply();

                return true;

            }

            catch (Exception ex) { throw new Exception("CreateSupplier", ex); }

        }
    }
}
