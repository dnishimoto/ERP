   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.SupervisorDomain
{
    public class SupervisorView
    {


        public long SupervisorId { get; set; }
        public long AddressId { get; set; }
        public string SupervisorCode { get; set; }
        public long? JobTitleXrefId { get; set; }
        public long? ParentSupervisorId { get; set; }
        public bool? IsActive { get; set; }
        public string Area { get; set; }
        public string DepartmentCode { get; set; }
        public long SupervisorNumber { get; set; }


        public string SupervisorName { get; set; }
        public string ParentSupervisorName{get;set;}
        public long? ParentSupervisorAddressId { get; set; }
        public string ParentSupervisorTitle { get; set; }
        public string ParentSupervisorCode { get; set; }

        public string JobTitle { get; set; }
    }

  

    public class SupervisorRepository: Repository<Supervisor>, ISupervisorRepository
    {
        ListensoftwaredbContext _dbContext;
        public SupervisorRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<Supervisor>GetEntityById(long ? supervisorId)
        {
			try{
            return await _dbContext.FindAsync<Supervisor>(supervisorId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Supervisor> GetEntityByNumber(long supervisorNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Supervisor
                               where detail.SupervisorNumber == supervisorNumber
                               select detail).FirstOrDefaultAsync<Supervisor>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		
  }
}
