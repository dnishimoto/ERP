   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ERP_Core2.AddressBookDomain
{
 public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public EmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<Employee>GetEntityById(long employeeId)
        {
            return await _dbContext.FindAsync<Employee>(employeeId);
        }
         public async Task<Employee> GetEntityByNumber(long employeeNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Employee
                               where detail.EmployeeNumber == employeeNumber
                               select detail).FirstOrDefaultAsync<Employee>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		 public async Task<List<Employee>> GetObjectsQueryable(Expression<Func<Employee, bool>> predicate,string include)
       {
            try
            {
                var resultList = base.GetObjectsQueryable(predicate, include);

                List <Employee> list = new List<Employee>();
                foreach (var item in resultList)
                {
                    list.Add(item);
                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

		
  }
}
