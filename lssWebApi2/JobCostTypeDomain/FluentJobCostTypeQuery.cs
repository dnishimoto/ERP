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

namespace lssWebApi2.JobCostTypeDomain
{
public class FluentJobCostTypeQuery:MapperAbstract<JobCostType,JobCostTypeView>,IFluentJobCostTypeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentJobCostTypeQuery() { }
        public FluentJobCostTypeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<JobCostType> MapToEntity(JobCostTypeView inputObject)
        {
            JobCostType outObject = mapper.Map<JobCostType>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<JobCostType>> MapToEntity(IList<JobCostTypeView> inputObjects)
        {
            IList<JobCostType> list = new List<JobCostType>();
            foreach (var item in inputObjects)
            {
                JobCostType outObject = mapper.Map<JobCostType>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<JobCostTypeView>> GetViewsByPage(Expression<Func<JobCostType, bool>> predicate, Expression<Func<JobCostType, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.jobCostTypeRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<JobCostType> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<JobCostTypeView> container = new PageListViewContainer<JobCostTypeView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                JobCostTypeView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<JobCostTypeView> MapToView(JobCostType inputObject)
        {
            JobCostTypeView outObject = mapper.Map<JobCostTypeView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfJobCostType.JobCostTypeNumber.ToString());
        }
 public override async Task<JobCostTypeView> GetViewById(long ? jobCostTypeId)
        {
            JobCostType detailItem = await _unitOfWork.jobCostTypeRepository.GetEntityById(jobCostTypeId);

            return await MapToView(detailItem);
        }
 public async Task<JobCostTypeView> GetViewByNumber(long jobCostTypeNumber)
        {
            JobCostType detailItem = await _unitOfWork.jobCostTypeRepository.GetEntityByNumber(jobCostTypeNumber);

            return await MapToView(detailItem);
        }

public override async Task<JobCostType> GetEntityById(long ? jobCostTypeId)
        {
            return await _unitOfWork.jobCostTypeRepository.GetEntityById(jobCostTypeId);

        }
 public async Task<JobCostType> GetEntityByNumber(long jobCostTypeNumber)
        {
            return await _unitOfWork.jobCostTypeRepository.GetEntityByNumber(jobCostTypeNumber);
        }
}
}
