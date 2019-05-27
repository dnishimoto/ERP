using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.AddressBookDomain.Repository;
using lssWebApi2.EntityFramework;
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
            this.JobCode = employee.JobTitleXref.KeyCode;


        }

        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeStatus { get; set; }
        public string JobCode { get; set; }
    }
 

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationViewFactory applicationViewFactory;
      
        ListensoftwaredbContext _dbContext;
        public EmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<EmployeeView> GetEmployeeViewByEmployeeId(long employeeId)
        {
            Employee employee = await GetObjectAsync(employeeId);
            return applicationViewFactory.MapEmployeeView(employee);
        }



    }
}
