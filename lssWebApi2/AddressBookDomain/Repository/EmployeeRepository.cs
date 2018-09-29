using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{


    public class EmployeeView
    {
        public EmployeeView() { }
        public EmployeeView(Employee employee)
        {
            this.EmployeeId = employee.EmployeeId;
            this.EmployeeName = employee.Address.Name;
            this.EmployeeTitle = employee.JobTitleXref.Value;
            this.EmployeeStatus = employee.EmploymentStatusXref.Value;
        }

        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeStatus { get; set; }
    }
 

    public class EmployeeRepository : Repository<Employee>
    {
        private ApplicationViewFactory applicationViewFactory;
      
        ListensoftwareDBContext _dbContext;
        public EmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<EmployeeView> GetEmployeeViewByEmployeeId(long employeeId)
        {
            Employee employee = await GetObjectAsync(employeeId);
            return applicationViewFactory.MapEmployeeView(employee);
        }



    }
}
