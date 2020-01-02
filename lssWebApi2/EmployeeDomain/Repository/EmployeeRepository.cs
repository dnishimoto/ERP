   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.EmployeeDomain
{
    public class EmployeeView
    {
        public long EmployeeId { get; set; }
        public long AddressId { get; set; }
        public long JobTitleXrefId { get; set; }
        public long EmploymentStatusXrefId { get; set; }
        public DateTime? HiredDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string TaxIdentification { get; set; }
        public int? PayRollGroupCode { get; set; }
        public Decimal Salary { get; set; }
        public Decimal HourlyRate { get; set; }
        public Decimal SalaryPerPayPeriod { get; set; }
        public long EmployeeNumber { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeTitle { get; set; } 
        public string EmployeeStatus { get; set; }
        public string JobCode { get; set; }
    }
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public EmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IList<Employee>> GetEmployeeByTaskId(int ? taskId)
        {
            try
            {
                IList<Employee> list = await (from employee in _dbContext.Employee
                                              join task in _dbContext.ProjectManagementTaskToEmployee
                                              on employee.EmployeeId equals task.EmployeeId
                                              where task.TaskId == taskId
                                              select employee
                                ).ToListAsync<Employee>();

             
                return list;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }

        public async Task<IList<Employee>> GetEmployeeByWorkOrderId(long? workOrderId)
        {

            IList<Employee> list = await (from employee in _dbContext.Employee
                                                     join workOrder in _dbContext.ProjectManagementWorkOrderToEmployee
                                                     on employee.EmployeeId equals workOrder.EmployeeId
                                                     where workOrder.WorkOrderId == workOrderId
                                                select employee
                                ).ToListAsync<Employee>();
            
            return list;

        }


        public async Task<Employee>GetEntityById(long ? employeeId)
        {
            try
            {
                return await _dbContext.FindAsync<Employee>(employeeId);
            }

            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
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
		
        public async Task<IList<Employee>> GetEntitiesBySupervisorId(long supervisorId)
        {
            try

            {
                IList<Employee> list = await (from supervisoremployee in _dbContext.SupervisorEmployees

                                  join employee in _dbContext.Employee on

                                  supervisoremployee.EmployeeId equals employee.EmployeeId

                                  where supervisoremployee.SupervisorId == supervisorId

                                  select employee
                    ).ToListAsync<Employee>();
 
                return list;
            }

            catch (Exception ex)

            {

                throw new Exception(GetMyMethodName(), ex);

            }
        }

  }
}
