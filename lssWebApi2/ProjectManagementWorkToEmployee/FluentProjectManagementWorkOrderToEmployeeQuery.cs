using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{
public class FluentProjectManagementWorkOrderToEmployeeQuery:MapperAbstract<ProjectManagementWorkOrderToEmployee,ProjectManagementWorkOrderToEmployeeView>,IFluentProjectManagementWorkOrderToEmployeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentProjectManagementWorkOrderToEmployeeQuery() { }
        public FluentProjectManagementWorkOrderToEmployeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ProjectManagementWorkOrderToEmployee> MapToEntity(ProjectManagementWorkOrderToEmployeeView inputObject)
        {
            ProjectManagementWorkOrderToEmployee outObject = mapper.Map<ProjectManagementWorkOrderToEmployee>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<ProjectManagementWorkOrderToEmployee>> MapToEntity(IList<ProjectManagementWorkOrderToEmployeeView> inputObjects)
        {
            IList<ProjectManagementWorkOrderToEmployee> list = new List<ProjectManagementWorkOrderToEmployee>();
            foreach (var item in inputObjects)
            {
                ProjectManagementWorkOrderToEmployee outObject = mapper.Map<ProjectManagementWorkOrderToEmployee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ProjectManagementWorkOrderToEmployeeView> MapToView(ProjectManagementWorkOrderToEmployee inputObject)
        {
            ProjectManagementWorkOrderToEmployeeView outObject = mapper.Map<ProjectManagementWorkOrderToEmployeeView>(inputObject);

            AddressBook addressBook = null;
            Task<Employee> employeeTask = _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<ProjectManagementWorkOrder> workOrderTask = _unitOfWork.projectManagementWorkOrderRepository.GetEntityById(inputObject.WorkOrderId);
            Task.WaitAll(employeeTask, workOrderTask);
            addressBook = await _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result.AddressId);


            outObject.EmployeeName = addressBook.Name;
            outObject.WorkOrderDescription = workOrderTask.Result.Description;
            outObject.WorkOrderLocation = workOrderTask.Result.Location;
            outObject.WorkOrderStartDate = workOrderTask.Result.StartDate;
            outObject.WorkOrderEndDate = workOrderTask.Result.EndDate;
            outObject.WorkOrderInstructions = workOrderTask.Result.Instructions;


    
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfProjectManagementWorkOrderToEmployee.WorkOrderToEmployeeNumber.ToString());
        }
 public override async Task<ProjectManagementWorkOrderToEmployeeView> GetViewById(long ? projectManagementWorkOrderToEmployeeId)
        {
            ProjectManagementWorkOrderToEmployee detailItem = await _unitOfWork.projectManagementWorkOrderToEmployeeRepository.GetEntityById(projectManagementWorkOrderToEmployeeId);

            return await MapToView(detailItem);
        }
 public async Task<ProjectManagementWorkOrderToEmployeeView> GetViewByNumber(long projectManagementWorkOrderToEmployeeNumber)
        {
            ProjectManagementWorkOrderToEmployee detailItem = await _unitOfWork.projectManagementWorkOrderToEmployeeRepository.GetEntityByNumber(projectManagementWorkOrderToEmployeeNumber);

            return await MapToView(detailItem);
        }

public override async Task<ProjectManagementWorkOrderToEmployee> GetEntityById(long ? projectManagementWorkOrderToEmployeeId)
        {
            return await _unitOfWork.projectManagementWorkOrderToEmployeeRepository.GetEntityById(projectManagementWorkOrderToEmployeeId);

        }
 public async Task<ProjectManagementWorkOrderToEmployee> GetEntityByNumber(long projectManagementWorkOrderToEmployeeNumber)
        {
            return await _unitOfWork.projectManagementWorkOrderToEmployeeRepository.GetEntityByNumber(projectManagementWorkOrderToEmployeeNumber);
        }
        public async Task<IList<ProjectManagementWorkOrderToEmployeeView>> GetViewsByWorkOrderId(long? workOrderId)
        {
            IList<ProjectManagementWorkOrderToEmployee> list= await _unitOfWork.projectManagementWorkOrderToEmployeeRepository.FindEntitiesByExpression(e=>e.WorkOrderId==workOrderId);

            IList<ProjectManagementWorkOrderToEmployeeView> views = new List<ProjectManagementWorkOrderToEmployeeView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }
}
}
