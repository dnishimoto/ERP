using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.ProjectManagementDomain;
using MillenniumERP.ScheduleEventsDomain;

namespace MillenniumERP.Services
{
    public class UnitOfWork
    {
        DbContext db = new Entities();
        public AddressBookRepository addressBookRepository => new AddressBookRepository(db);
        public ChartOfAccountRepository chartOfAccountRepository => new ChartOfAccountRepository(db);
        public BudgetSnapShotRepository budgetSnapShotRepository => new BudgetSnapShotRepository(db);
        public ScheduleEventRepository scheduleEventRepository => new ScheduleEventRepository(db);
        public ProjectManagementProjectRepository projectManagementProjectRepository => new ProjectManagementProjectRepository(db);
      
        public UnitOfWork()
        {
            /*
            db.Database.Connection.Open();
            if (db.Database.Connection.State == ConnectionState.Open)
            {
                Console.WriteLine(@"INFO: ConnectionString: " + db.Database.Connection.ConnectionString
                    + "\n DataBase: " + db.Database.Connection.Database
                    + "\n DataSource: " + db.Database.Connection.DataSource
                    + "\n ServerVersion: " + db.Database.Connection.ServerVersion
                    + "\n TimeOut: " + db.Database.Connection.ConnectionTimeout);
                db.Database.Connection.Close();

            }
            */

            //addressBookRepository = new AddressBookRepository(db);
            //chartOfAccountRepository = new ChartOfAccountRepository(db);
        }

        public void CommitChanges()
        {
            db.SaveChanges();
        }
    }
}
