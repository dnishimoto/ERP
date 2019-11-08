using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.AddressBookDomain
{
public class FluentEmployeeQuery:IFluentEmployeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentEmployeeQuery() { }
        public FluentEmployeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<Employee> MapToEntity(EmployeeView inputObject)
        {
            Mapper mapper = new Mapper();
            Employee outObject = mapper.Map<Employee>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<Employee>> MapToEntity(List<EmployeeView> inputObjects)
        {
            List<Employee> list = new List<Employee>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                Employee outObject = mapper.Map<Employee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<EmployeeView> MapToView(Employee inputObject)
        {
            Mapper mapper = new Mapper();
            EmployeeView outObject = mapper.Map<EmployeeView>(inputObject);
            await Task.Yield();

            Task<AddressBook> addressBookTask = _unitOfWork.addressBookRepository.GetEntityById(outObject.AddressId);
            Task<Udc> jobTitleXrefTask = _unitOfWork.udcRepository.GetEntityById(outObject.JobTitleXrefId);
            Task<Udc> employeeStatusXrefTask = _unitOfWork.udcRepository.GetEntityById(outObject.EmploymentStatusXrefId);

            Task.WaitAll(addressBookTask, jobTitleXrefTask, employeeStatusXrefTask);

            outObject.EmployeeName = addressBookTask.Result.Name;
            outObject.EmployeeTitle = jobTitleXrefTask.Result.Value;
            outObject.EmployeeStatus = employeeStatusXrefTask.Result.Value;
            outObject.JobCode = jobTitleXrefTask.Result.KeyCode;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.employeeRepository.GetNextNumber(TypeOfAddressBook.EmployeeNumber.ToString());
        }
 public async Task<EmployeeView> GetViewById(long employeeId)
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

public async Task<Employee> GetEntityById(long employeeId)
        {
            return await _unitOfWork.employeeRepository.GetEntityById(employeeId);

        }
 public async Task<Employee> GetEntityByNumber(long employeeNumber)
        {
            return await _unitOfWork.employeeRepository.GetEntityByNumber(employeeNumber);
        }
        public async Task<List<EmployeeView>> GetViewsBySupervisorId(long supervisorId)
        {
            List<Employee> list = await _unitOfWork.employeeRepository.GetEntitiesBySupervisorId(supervisorId);

            List<EmployeeView> listViews = new List<EmployeeView>();
            foreach (var item in list) { listViews.Add(await MapToView(item)); }
            return listViews;
        }
}
}
