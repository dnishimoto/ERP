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

namespace lssWebApi2.JobCostLedgerDomain
{
public class FluentJobCostLedgerQuery:MapperAbstract<JobCostLedger,JobCostLedgerView>,IFluentJobCostLedgerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentJobCostLedgerQuery() { }
        public FluentJobCostLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<JobCostLedger> MapToEntity(JobCostLedgerView inputObject)
        {
            JobCostLedger outObject = mapper.Map<JobCostLedger>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<JobCostLedger>> MapToEntity(IList<JobCostLedgerView> inputObjects)
        {
            IList<JobCostLedger> list = new List<JobCostLedger>();
            foreach (var item in inputObjects)
            {
                JobCostLedger outObject = mapper.Map<JobCostLedger>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<JobCostLedgerView>> GetViewsByPage(Expression<Func<JobCostLedger, bool>> predicate, Expression<Func<JobCostLedger, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.jobCostLedgerRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<JobCostLedger> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<JobCostLedgerView> container = new PageListViewContainer<JobCostLedgerView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                JobCostLedgerView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<JobCostLedgerView> MapToView(JobCostLedger inputObject)
        {
            JobCostLedgerView outObject = mapper.Map<JobCostLedgerView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfJobCostLedger.JobCostLedgerNumber.ToString());
        }
 public override async Task<JobCostLedgerView> GetViewById(long ? jobCostLedgerId)
        {
            JobCostLedger detailItem = await _unitOfWork.jobCostLedgerRepository.GetEntityById(jobCostLedgerId);

            return await MapToView(detailItem);
        }
 public async Task<JobCostLedgerView> GetViewByNumber(long jobCostLedgerNumber)
        {
            JobCostLedger detailItem = await _unitOfWork.jobCostLedgerRepository.GetEntityByNumber(jobCostLedgerNumber);

            return await MapToView(detailItem);
        }

public override async Task<JobCostLedger> GetEntityById(long ? jobCostLedgerId)
        {
            return await _unitOfWork.jobCostLedgerRepository.GetEntityById(jobCostLedgerId);

        }
 public async Task<JobCostLedger> GetEntityByNumber(long jobCostLedgerNumber)
        {
            return await _unitOfWork.jobCostLedgerRepository.GetEntityByNumber(jobCostLedgerNumber);
        }
}
}
