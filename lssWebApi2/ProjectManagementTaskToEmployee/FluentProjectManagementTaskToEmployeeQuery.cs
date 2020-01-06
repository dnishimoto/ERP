using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{
    public class FluentProjectManagementTaskToEmployeeQuery : MapperAbstract<ProjectManagementTaskToEmployee, ProjectManagementTaskToEmployeeView>, IFluentProjectManagementTaskToEmployeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentProjectManagementTaskToEmployeeQuery() { }
        public FluentProjectManagementTaskToEmployeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<ProjectManagementTaskToEmployee> MapToEntity(ProjectManagementTaskToEmployeeView inputObject)
        {
            ProjectManagementTaskToEmployee outObject = mapper.Map<ProjectManagementTaskToEmployee>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ProjectManagementTaskToEmployee>> MapToEntity(IList<ProjectManagementTaskToEmployeeView> inputObjects)
        {
            IList<ProjectManagementTaskToEmployee> list = new List<ProjectManagementTaskToEmployee>();
            foreach (var item in inputObjects)
            {
                ProjectManagementTaskToEmployee outObject = mapper.Map<ProjectManagementTaskToEmployee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ProjectManagementTaskToEmployeeView> MapToView(ProjectManagementTaskToEmployee inputObject)
        {
            ProjectManagementTaskToEmployeeView outObject = mapper.Map<ProjectManagementTaskToEmployeeView>(inputObject);


            Task<Employee> employeeTask = _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<ProjectManagementTask> taskTask = _unitOfWork.projectManagementTaskRepository.GetEntityById(inputObject.TaskId);
            Task.WaitAll(employeeTask, taskTask);

            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result.AddressId);
            ProjectManagementMilestone milestone = await _unitOfWork.projectManagementMilestoneRepository.GetEntityById(taskTask.Result.MileStoneId);
            ProjectManagementProject project = await _unitOfWork.projectManagementProjectRepository.GetEntityById(milestone.ProjectId);

            outObject.EmployeeName = addressBook.Name;
            outObject.TaskName = taskTask.Result.TaskName;
            outObject.TaskDescription = taskTask.Result.Description;
            outObject.MilestoneName = milestone.MilestoneName;
            outObject.ProjectName = project.ProjectName;

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.projectManagementTaskToEmployeeRepository.GetNextNumber(TypeOfProjectManagementTaskToEmployee.TaskToEmployeeNumber.ToString());
        }
        public override async Task<ProjectManagementTaskToEmployeeView> GetViewById(long? projectManagementTaskToEmployeeId)
        {
            ProjectManagementTaskToEmployee detailItem = await _unitOfWork.projectManagementTaskToEmployeeRepository.GetEntityById(projectManagementTaskToEmployeeId);

            return await MapToView(detailItem);
        }
        public async Task<ProjectManagementTaskToEmployeeView> GetViewByNumber(long projectManagementTaskToEmployeeNumber)
        {
            ProjectManagementTaskToEmployee detailItem = await _unitOfWork.projectManagementTaskToEmployeeRepository.GetEntityByNumber(projectManagementTaskToEmployeeNumber);

            return await MapToView(detailItem);
        }

        public override async Task<ProjectManagementTaskToEmployee> GetEntityById(long? projectManagementTaskToEmployeeId)
        {
            return await _unitOfWork.projectManagementTaskToEmployeeRepository.GetEntityById(projectManagementTaskToEmployeeId);

        }
        public async Task<ProjectManagementTaskToEmployee> GetEntityByNumber(long projectManagementTaskToEmployeeNumber)
        {
            return await _unitOfWork.projectManagementTaskToEmployeeRepository.GetEntityByNumber(projectManagementTaskToEmployeeNumber);
        }
    }
}
