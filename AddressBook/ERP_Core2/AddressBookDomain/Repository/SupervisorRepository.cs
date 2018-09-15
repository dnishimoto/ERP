using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using ERP_Core2.Services;

namespace ERP_Core2.AddressBookDomain
{
    public class SupervisorView 
    {
        
       public SupervisorView()
        {
           
        }
        public SupervisorView(Supervisor supervisor,Supervisor parentSupervisor)
        {
            this.SupervisorName = supervisor.AddressBook.Name;
            this.SupervisorAddressId = supervisor.AddressBook.AddressId;
            this.ParentSupervisorId = supervisor.ParentSupervisorId;
            if (this.ParentSupervisorId != null)
            {
                this.ParentSupervisorName = parentSupervisor.AddressBook.Name;
                this.ParentSupervisorCode = parentSupervisor.SupervisorCode;
                this.ParentSupervisorId = parentSupervisor.SupervisorId;
                this.ParentSupervisorTitle = parentSupervisor.UDC.Value;
                this.ParentSupervisorAddressId = parentSupervisor.AddressBook.AddressId;
            }

            this.SupervisorCode = supervisor.SupervisorCode;
            this.SupervisorId = supervisor.SupervisorId;
            this.Title = supervisor.UDC.Value;
        }

         public Supervisor parentsupervisor { get; set; }
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
        private ApplicationViewFactory applicationViewFactory;
      
        Entities _dbContext;
        public SupervisorRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
    
        public List<EmployeeView> GetEmployeesBySupervisorId(long supervisorId)
        {
            try
            {
                var resultList = (from supervisoremployee in _dbContext.SupervisorEmployees
                                  join employee in _dbContext.Employees on
                                  supervisoremployee.EmployeeId equals employee.EmployeeId
                                  where supervisoremployee.SupervisorId == supervisorId

                                  select employee

                    );
                List<EmployeeView> list = new List<EmployeeView>();
                foreach (var item in resultList)
                {
                    list.Add(applicationViewFactory.MapEmployeeView(item));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<SupervisorView> GetSupervisorBySupervisorId(long supervisorId)
        {
            try
            {
                Supervisor supervisor = await base.GetObjectAsync(supervisorId);
                Supervisor parentSupervisor = null;
                long parentSupervisorId = supervisor.ParentSupervisorId ?? 0;
                if (parentSupervisorId !=0)
                { parentSupervisor = await base.GetObjectAsync(parentSupervisorId); }

                SupervisorView view = applicationViewFactory.MapSupervisorView(supervisor, parentSupervisor);


                return view;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
