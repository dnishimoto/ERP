using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{


    public class SupplierView
    {
        public SupplierView() { }
        public SupplierView(Supplier supplier)
        {
            this.SupplierId = supplier.SupplierId;
            this.SupplierName = supplier.Address.Name;
            this.CompanyName = supplier.Address.CompanyName;
            this.SupplierIdentification = supplier.Identification;

        }

        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public string SupplierIdentification { get; set; }


 
    }
 

    public class SupplierRepository : Repository<Supplier>
    {
        private ApplicationViewFactory applicationViewFactory;
      
        ListensoftwareDBContext _dbContext;
        public SupplierRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<SupplierView> GetSupplierViewBySupplierId(long supplierId)
        {
            Supplier supplier = await GetObjectAsync(supplierId);
            return applicationViewFactory.MapSupplierView(supplier);
        }
        public async Task<SupplierView> CreateSupplierByAddressBook(AddressBook addressBook, LocationAddress locationAddress, Emails email)
        {
            SupplierView supplierView = null;
            try
            {
                Udc udc = await base.GetUdc("AB_Type", "Supplier");
                if (udc != null)
                {
                    addressBook.PeopleXrefId = udc.XrefId;
                }

                Udc udcLocationAddressType = await base.GetUdc("LOCATIONADDRESS_TYPE", "BillTo");
                if (udcLocationAddressType != null)
                {
                    locationAddress.TypeXrefId = udcLocationAddressType.XrefId;
                }

                AddressBook query = await (from e in _dbContext.AddressBook
                                           join f in _dbContext.Emails
                                               on e.AddressId equals f.AddressId
                                           where f.Email == email.Email
                                           && f.LoginEmail==true
                                           select e).FirstOrDefaultAsync<AddressBook>();

                if (query == null)
                {
                    _dbContext.Set<AddressBook>().Add(addressBook);
                    _dbContext.SaveChanges();
                    locationAddress.AddressId = addressBook.AddressId;
                    _dbContext.Set<LocationAddress>().Add(locationAddress);
                    _dbContext.SaveChanges();
                    email.AddressId = addressBook.AddressId;
                    _dbContext.Set<Emails>().Add(email);
                    _dbContext.SaveChanges();


                }
                else
                {
                    addressBook.AddressId = query.AddressId;
                }
                Supplier supplier = await (from e in _dbContext.Supplier
                                           where e.AddressId == addressBook.AddressId
                                           select e).FirstOrDefaultAsync<Supplier>();
                if (supplier == null)
                {
                    Supplier newSupplier = new Supplier();
                    newSupplier.AddressId = addressBook.AddressId;
                    newSupplier.Identification = email.Email;

                    _dbContext.Set<Supplier>().Add(newSupplier);
                    _dbContext.SaveChanges();

                    supplierView = applicationViewFactory.MapSupplierView(newSupplier);
                }
                else
                {
                    supplierView = applicationViewFactory.MapSupplierView(supplier);
                }
                return supplierView;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }


    }
}
