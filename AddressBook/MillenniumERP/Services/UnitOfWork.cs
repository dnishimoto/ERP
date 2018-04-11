using MillenniumERP.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{
    public class UnitOfWork
    {
        public AddressBookRepository addressBookRepository = null;
        DbContext db = new Entities();
        public UnitOfWork()
        {
            addressBookRepository=new AddressBookRepository(db);
    
        }
        public void CommitChanges()
        {
            db.SaveChanges();
        }
    }
}
