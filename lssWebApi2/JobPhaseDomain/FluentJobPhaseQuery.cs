using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;
using System.Linq.Expressions;
using X.PagedList;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.JobPhaseDomain
{
    public class FluentJobPhaseQuery : MapperAbstract<JobPhase, JobPhaseView>, IFluentJobPhaseQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentJobPhaseQuery() { }
        public FluentJobPhaseQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<JobPhase> MapToEntity(JobPhaseView inputObject)
        {
            JobPhase outObject = mapper.Map<JobPhase>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<JobPhase>> MapToEntity(IList<JobPhaseView> inputObjects)
        {
            IList<JobPhase> list = new List<JobPhase>();
            foreach (var item in inputObjects)
            {
                JobPhase outObject = mapper.Map<JobPhase>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public async Task<IList<JobPhase>> GetEntitiesByJobMasterId(long? jobMasterId)
        {
            return await _unitOfWork.jobPhaseRepository.GetEntitiesByJobMasterId(jobMasterId);
        }


        public async Task<PageListViewContainer<JobPhaseView>> GetViewsByPage(Expression<Func<JobPhase, bool>> predicate, Expression<Func<JobPhase, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.jobPhaseRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<JobPhase> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<JobPhaseView> container = new PageListViewContainer<JobPhaseView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                JobPhaseView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

        public override async Task<JobPhaseView> MapToView(JobPhase inputObject)
        {
            JobPhaseView outObject = mapper.Map<JobPhaseView>(inputObject);
            Task<JobMaster> jobMasterTask = _unitOfWork.jobMasterRepository.GetEntityById(inputObject.JobMasterId);
            Task<Contract> contractTask = _unitOfWork.contractRepository.GetEntityById(inputObject.ContractId);
            Task<JobCostType> jobCostTypeTask = _unitOfWork.jobCostTypeRepository.GetEntityById(inputObject.JobCostTypeId);

            Task.WaitAll(jobMasterTask, contractTask, jobCostTypeTask);

            outObject.JobDescription = jobMasterTask.Result?.JobDescription;
            outObject.ContractTitle = contractTask.Result?.Title;
            outObject.CostCode = jobCostTypeTask.Result?.CostCode;

            await Task.Yield();

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfJobPhase.JobPhaseNumber.ToString());
        }
        public override async Task<JobPhaseView> GetViewById(long? jobPhaseId)
        {
            JobPhase detailItem = await _unitOfWork.jobPhaseRepository.GetEntityById(jobPhaseId);

            return await MapToView(detailItem);
        }
        public async Task<JobPhaseView> GetViewByNumber(long jobPhaseNumber)
        {
            JobPhase detailItem = await _unitOfWork.jobPhaseRepository.GetEntityByNumber(jobPhaseNumber);

            return await MapToView(detailItem);
        }

        public override async Task<JobPhase> GetEntityById(long? jobPhaseId)
        {
            return await _unitOfWork.jobPhaseRepository.GetEntityById(jobPhaseId);

        }
        public async Task<JobPhase> GetEntityByNumber(long jobPhaseNumber)
        {
            return await _unitOfWork.jobPhaseRepository.GetEntityByNumber(jobPhaseNumber);
        }
        public async Task<JobPhase> GetEntityByJobIdAndPhase(long? jobMasterId, string phase)
        {
            return await _unitOfWork.jobPhaseRepository.FindEntityByExpression(e => e.JobMasterId == jobMasterId && e.Phase == phase);
        }
    }
}
