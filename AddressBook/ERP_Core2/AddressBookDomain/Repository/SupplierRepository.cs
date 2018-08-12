using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.Services;

namespace MillenniumERP.AddressBookDomain
{
          
      
    public class SupplierView
    {
        public SupplierView() { }
        public SupplierView(Supplier supplier)
        {
            this.SupplierId = supplier.SupplierId;
            this.SupplierName = supplier.AddressBook.Name;
            this.CompanyName = supplier.AddressBook.CompanyName;
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
      
        Entities _dbContext;
        public SupplierRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public SupplierView GetSupplierViewBySupplierId(long supplierId)
        {
            Task<Supplier> supplierTask = GetObjectAsync(supplierId);
            return applicationViewFactory.MapSupplierView(supplierTask.Result);
        }
        public async Task<SupplierView> CreateSupplierByAddressBook(AddressBook addressBook, LocationAddress locationAddress, Email email)
        {
            SupplierView supplierView = null;
            try
            {
                UDC udc = await base.GetUdc("AB_Type", "Supplier");
                if (udc != null)
                {
                    addressBook.PeopleXrefId = udc.XRefId;
                }

                UDC udcLocationAddressType = await base.GetUdc("LOCATIONADDRESS_TYPE", "BillTo");
                if (udcLocationAddressType != null)
                {
                    locationAddress.TypeXRefId = udcLocationAddressType.XRefId;
                }

                AddressBook query = await (from e in _dbContext.AddressBooks
                                           join f in _dbContext.Emails
                                               on e.AddressId equals f.AddressId
                                           where f.Email1 == email.Email1
                                           select e).FirstOrDefaultAsync<AddressBook>();

                if (query == null)
                {
                    _dbContext.Set<AddressBook>().Add(addressBook);
                    _dbContext.SaveChanges();
                    locationAddress.AddressId = addressBook.AddressId;
                    _dbContext.Set<LocationAddress>().Add(locationAddress);
                    _dbContext.SaveChanges();
                    email.AddressId = addressBook.AddressId;
                    _dbContext.Set<Email>().Add(email);
                    _dbContext.SaveChanges();


                }
                else
                {
                    addressBook.AddressId = query.AddressId;
                }
                Supplier supplier = await (from e in _dbContext.Suppliers
                                           where e.AddressId == addressBook.AddressId
                                           select e).FirstOrDefaultAsync<Supplier>();
                if (supplier == null)
                {
                    Supplier newSupplier = new Supplier();
                    newSupplier.AddressId = addressBook.AddressId;
                    newSupplier.Identification = email.Email1;

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
