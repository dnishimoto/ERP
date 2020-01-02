using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;

namespace lssWebApi2.ProjectManagementTaskDomain
{
    public class FluentProjectManagementTaskQuery : MapperAbstract<ProjectManagementTask, ProjectManagementTaskView>, IFluentProjectManagementTaskQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentProjectManagementTaskQuery() { }
        public FluentProjectManagementTaskQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IQueryable<ProjectManagementTask>> GetEntitiesByMilestoneId(long? milestoneId)
        {
            try
            {
                return await _unitOfWork.projectManagementTaskRepository.GetEntitiesByMilestoneId(milestoneId);

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IQueryable<ProjectManagementTask>> GetEntitiesByProjectId(long? projectId)
        {
            try
            {
                IQueryable<ProjectManagementTask> query = await _unitOfWork.projectManagementTaskRepository.GetEntitiesByProjectId(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetTasksByProjectId", ex);
            }
        }
        public override async Task<ProjectManagementTask> MapToEntity(ProjectManagementTaskView inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementTask outObject = mapper.Map<ProjectManagementTask>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<ProjectManagementTask>> MapToEntity(List<ProjectManagementTaskView> inputObjects)
        {
            List<ProjectManagementTask> list = new List<ProjectManagementTask>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                ProjectManagementTask outObject = mapper.Map<ProjectManagementTask>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ProjectManagementTaskView> MapToView(ProjectManagementTask inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementTaskView outObject = mapper.Map<ProjectManagementTaskView>(inputObject);

            ProjectManagementProject project = await _unitOfWork.projectManagementProjectRepository.GetEntityById(inputObject.ProjectId);
            ProjectManagementMilestone milestone = await _unitOfWork.projectManagementMilestoneRepository.GetEntityById(1);
            Udc udc = await _unitOfWork.udcRepository.GetEntityById(21);
            ProjectManagementWorkOrder workOrder = await _unitOfWork.projectManagementWorkOrderRepository.GetEntityById(7);
            ChartOfAccount account = await _unitOfWork.chartOfAccountRepository.GetEntityById(4);

            FluentProjectManagementWorkOrderToEmployee WorkToEmployee = new FluentProjectManagementWorkOrderToEmployee();
            IList<ProjectManagementWorkOrderToEmployeeView> views = await WorkToEmployee.Query().GetViewsByWorkOrderId(workOrder.WorkOrderId);

            outObject.ProjectName = project.ProjectName;
            outObject.Status = udc.Value;
            outObject.Instructions = workOrder.Instructions;
            outObject.Account = account.Account;
            outObject.WorkOrderToEmployeeViews = views;

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.projectManagementTaskRepository.GetNextNumber(TypeOfProjectManagementTask.TaskNumber.ToString());
        }
        public override async Task<ProjectManagementTaskView> GetViewById(long? projectManagementTaskId)
        {
            ProjectManagementTask detailItem = await _unitOfWork.projectManagementTaskRepository.GetEntityById(projectManagementTaskId);

            return await MapToView(detailItem);
        }
        public async Task<ProjectManagementTaskView> GetViewByNumber(long projectManagementTaskNumber)
        {
            ProjectManagementTask detailItem = await _unitOfWork.projectManagementTaskRepository.GetEntityByNumber(projectManagementTaskNumber);

            return await MapToView(detailItem);
        }

        public override async Task<ProjectManagementTask> GetEntityById(long? projectManagementTaskId)
        {
            return await _unitOfWork.projectManagementTaskRepository.GetEntityById(projectManagementTaskId);

        }
        public async Task<ProjectManagementTask> GetEntityByNumber(long projectManagementTaskNumber)
        {
            return await _unitOfWork.projectManagementTaskRepository.GetEntityByNumber(projectManagementTaskNumber);
        }
    }
}
