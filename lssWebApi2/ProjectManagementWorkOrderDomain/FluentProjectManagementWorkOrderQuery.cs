using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using System.Linq;
using System;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{
public class FluentProjectManagementWorkOrderQuery:MapperAbstract<ProjectManagementWorkOrder,ProjectManagementWorkOrderView>,IFluentProjectManagementWorkOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentProjectManagementWorkOrderQuery() { }
        public FluentProjectManagementWorkOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IQueryable<ProjectManagementWorkOrder>> GetEntitiesByProjectId(long ? projectId)
        {
            try
            {
                IQueryable<ProjectManagementWorkOrder> query = await _unitOfWork.projectManagementWorkOrderRepository.GetEntitiesByProjectId(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetWorkOrdersByProjectId", ex);
            }

        }

       
        public override async Task<ProjectManagementWorkOrder> MapToEntity(ProjectManagementWorkOrderView inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementWorkOrder outObject = mapper.Map<ProjectManagementWorkOrder>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<ProjectManagementWorkOrder>> MapToEntity(List<ProjectManagementWorkOrderView> inputObjects)
        {
            List<ProjectManagementWorkOrder> list = new List<ProjectManagementWorkOrder>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                ProjectManagementWorkOrder outObject = mapper.Map<ProjectManagementWorkOrder>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ProjectManagementWorkOrderView> MapToView(ProjectManagementWorkOrder inputObject)
        {
            Mapper mapper = new Mapper();
            ProjectManagementWorkOrderView outObject = mapper.Map<ProjectManagementWorkOrderView>(inputObject);

          
            Task<ProjectManagementProject> projectTask =  _unitOfWork.projectManagementProjectRepository.GetEntityById(inputObject.ProjectId);
            Task<ChartOfAccount> accountTask =  _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
            Task.WaitAll(projectTask, accountTask);

            outObject.AccountNumber = accountTask.Result.Account;
            outObject.AccountDescription = accountTask.Result.Description;
            outObject.SupCode = accountTask.Result.SupCode;
            outObject.ThirdAccount = accountTask.Result.ThirdAccount;
            outObject.ProjectName = projectTask.Result.ProjectName;

            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.projectManagementWorkOrderRepository.GetNextNumber(TypeOfProjectManagementWorkOrder.WorkOrderNumber.ToString());
        }
 public override async Task<ProjectManagementWorkOrderView> GetViewById(long ? projectManagementWorkOrderId)
        {
            ProjectManagementWorkOrder detailItem = await _unitOfWork.projectManagementWorkOrderRepository.GetEntityById(projectManagementWorkOrderId);

            return await MapToView(detailItem);
        }
 public async Task<ProjectManagementWorkOrderView> GetViewByNumber(long projectManagementWorkOrderNumber)
        {
            ProjectManagementWorkOrder detailItem = await _unitOfWork.projectManagementWorkOrderRepository.GetEntityByNumber(projectManagementWorkOrderNumber);

            return await MapToView(detailItem);
        }

public override async Task<ProjectManagementWorkOrder> GetEntityById(long ? projectManagementWorkOrderId)
        {
            return await _unitOfWork.projectManagementWorkOrderRepository.GetEntityById(projectManagementWorkOrderId);

        }
 public async Task<ProjectManagementWorkOrder> GetEntityByNumber(long projectManagementWorkOrderNumber)
        {
            return await _unitOfWork.projectManagementWorkOrderRepository.GetEntityByNumber(projectManagementWorkOrderNumber);
        }
}
}
