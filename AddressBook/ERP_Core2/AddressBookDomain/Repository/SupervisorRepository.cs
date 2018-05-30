using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using MillenniumERP.Services;

namespace MillenniumERP.AddressBookDomain
{
    public class SupervisorView
    {
        public SupervisorView()
        {
           // supervisor = new Supervisor();
        }
        //public AddressBook addressBook { get; set; }
        //public Supervisor supervisor { get; set; }
        public Supervisor parentsupervisor { get; set; }
        //public UDC titleUDC { get; set; }

        public long? SupervisorId { get; set; }
        public long? SupervisorAddressId { get; set; }
        public string SupervisorName { get; set; }
        public string Title { get; set; }
        public string SupervisorCode { get; set; }

        public long? ParentSupervisorId { get; set; }
        public long? ParentSupervisorAddressId { get; set; }
        public string ParentSupervisorName { get; set; }
        public string ParentSupervisorTitle { get; set; }
        public string ParentSupervisorCode { get; set; }


    }
    public class SupervisorRepository : Repository<Supervisor>
    {
        Entities _dbContext;
        public SupervisorRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public SupervisorView GetSupervisorBySupervisorId(int supervisorId)
        {
            Task<Supervisor> supervisorTask = base.GetObjectAsync(supervisorId);

            SupervisorView view = new SupervisorView(              
            
            );
            view.SupervisorName = supervisorTask.Result.AddressBook.Name;
            view.SupervisorAddressId = supervisorTask.Result.AddressBook.AddressId;
            view.ParentSupervisorId = supervisorTask.Result.ParentSupervisorId;
            if (view.ParentSupervisorId != null)
            {
                Task<Supervisor> parentSupervisorTask = base.GetObjectAsync((int)view.ParentSupervisorId);
                view.ParentSupervisorName = parentSupervisorTask.Result.AddressBook.Name;
                view.ParentSupervisorCode = parentSupervisorTask.Result.SupervisorCode;
                view.ParentSupervisorId = parentSupervisorTask.Result.SupervisorId;
                view.ParentSupervisorTitle = parentSupervisorTask.Result.UDC.Value;
                view.ParentSupervisorAddressId = parentSupervisorTask.Result.AddressBook.AddressId;
            }
            
            view.SupervisorCode = supervisorTask.Result.SupervisorCode;
            view.SupervisorId = supervisorTask.Result.SupervisorId;
            view.Title = supervisorTask.Result.UDC.Value;
            return view;

        }
    }
}
