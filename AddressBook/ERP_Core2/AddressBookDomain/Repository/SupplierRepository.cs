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
            this.SupplierIdentification = supplier.Identification;

        }

        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
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
        public SupplierView GetSupplierViewBySupplierId(int supplierId)
        {
            Task<Supplier> supplierTask = GetObjectAsync(supplierId);
            return applicationViewFactory.MapSupplierView(supplierTask.Result);
        }



    }
}
