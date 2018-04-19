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
        DbContext db = new Entities();
        public AddressBookRepository addressBookRepository => new AddressBookRepository(db);
        public ChartOfAccountRepository chartOfAccountRepository => new ChartOfAccountRepository(db);
        public BudgetSnapShotRepository budgetSnapShotRepository => new BudgetSnapShotRepository(db);
        public ScheduleEventRepository scheduleEventRepository => new ScheduleEventRepository(db);

      
        public UnitOfWork()
        {
            //addressBookRepository = new AddressBookRepository(db);
            //chartOfAccountRepository = new ChartOfAccountRepository(db);
        }
        
        public void CommitChanges()
        {
            db.SaveChanges();
        }
    }
}
