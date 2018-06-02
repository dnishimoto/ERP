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
          
      
    public class EmployeeView
    {
        public EmployeeView() { }
        public EmployeeView(Employee employee)
        {
            this.EmployeeId = employee.EmployeeId;
            this.EmployeeName = employee.AddressBook.Name;
            this.EmployeeTitle = employee.UDC1.Value;
            this.EmployeeStatus = employee.UDC.Value;
        }

        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeStatus { get; set; }
    }
 

    public class EmployeeRepository : Repository<Employee>
    {
        private ApplicationViewFactory applicationViewFactory;
      
        Entities _dbContext;
        public EmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public EmployeeView GetEmployeeViewByEmployeeId(int employeeId)
        {
            Task<Employee> employeeTask = GetObjectAsync(employeeId);
            return applicationViewFactory.MapEmployeeView(employeeTask.Result);
        }



    }
}
