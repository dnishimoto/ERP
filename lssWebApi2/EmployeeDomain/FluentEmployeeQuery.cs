using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EmployeeDomain
{
    public class FluentEmployeeQuery : MapperAbstract<Employee,EmployeeView>, IFluentEmployeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentEmployeeQuery() { }
        public FluentEmployeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IEnumerable<EmployeeView>> GetEntitiesByWorkOrderId(long workOrderId)
        {
            IList<Employee> list = await _unitOfWork.employeeRepository.GetEmployeeByWorkOrderId(workOrderId);
            IList<EmployeeView> views = new List<EmployeeView>();
            
            foreach(var item in list)
            {
                   views.Add( await MapToView(item));
            }
         
            return views;
        }

        public override async Task<Employee> MapToEntity(EmployeeView inputObject)
        {
     
            Employee outObject = mapper.Map<Employee>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Employee>> MapToEntity(IList<EmployeeView> inputObjects)
        {
            IList<Employee> list = new List<Employee>();
  
            foreach (var item in inputObjects)
            {
                Employee outObject = mapper.Map<Employee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<EmployeeView> MapToView(Employee inputObject)
        {
            EmployeeView outObject = mapper.Map<EmployeeView>(inputObject);

                
                Task<AddressBook> addressBookTask = _unitOfWork.addressBookRepository.GetEntityById(outObject.AddressId);
                Task<Udc> jobTitleXrefTask = _unitOfWork.udcRepository.GetEntityById(outObject.JobTitleXrefId);
                Task<Udc> employeeStatusXrefTask = _unitOfWork.udcRepository.GetEntityById(outObject.EmploymentStatusXrefId);

                Task.WaitAll(addressBookTask, jobTitleXrefTask, employeeStatusXrefTask);

                outObject.EmployeeName = addressBookTask.Result.Name;
                outObject.EmployeeTitle = jobTitleXrefTask.Result.Value;
                outObject.EmployeeStatus = employeeStatusXrefTask.Result.Value;
                outObject.JobCode = jobTitleXrefTask.Result.KeyCode;

                await Task.Yield();
        
            return outObject;
        }

        public async Task<List<EmployeeView>> GetViewsBySupervisorId(long supervisorId)
        {
            List<Employee> list = (await _unitOfWork.employeeRepository.GetEntitiesBySupervisorId(supervisorId)).ToList<Employee>();
            List<EmployeeView> views = new List<EmployeeView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }



        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfEmployee.EmployeeNumber.ToString());
        }
        public override async Task<EmployeeView> GetViewById(long ? employeeId)
        {
            Employee detailItem = await _unitOfWork.employeeRepository.GetEntityById(employeeId);
            EmployeeView view = await MapToView(detailItem);

            return view;
        }
        public async Task<EmployeeView> GetViewByNumber(long employeeNumber)
        {
            Employee detailItem = await _unitOfWork.employeeRepository.GetEntityByNumber(employeeNumber);

            return await MapToView(detailItem);
        }

        public override async Task<Employee> GetEntityById(long ? employeeId)
        {
            return await _unitOfWork.employeeRepository.GetEntityById(employeeId);

        }
        public async Task<Employee> GetEntityByNumber(long employeeNumber)
        {
            return await _unitOfWork.employeeRepository.GetEntityByNumber(employeeNumber);
        }

    }
}
