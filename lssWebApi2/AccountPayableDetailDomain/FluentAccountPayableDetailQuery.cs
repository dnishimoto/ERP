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

namespace lssWebApi2.AccountPayableDetailDomain
{
public class FluentAccountPayableDetailQuery:MapperAbstract<AccountPayableDetail,AccountPayableDetailView>,IFluentAccountPayableDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountPayableDetailQuery() { }
        public FluentAccountPayableDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<AccountPayableDetail> MapToEntity(AccountPayableDetailView inputObject)
        {
            AccountPayableDetail outObject = mapper.Map<AccountPayableDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<AccountPayableDetail>> MapToEntity(IList<AccountPayableDetailView> inputObjects)
        {
            IList<AccountPayableDetail> list = new List<AccountPayableDetail>();
            foreach (var item in inputObjects)
            {
                AccountPayableDetail outObject = mapper.Map<AccountPayableDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<AccountPayableDetailView>> GetViewsByPage(Expression<Func<AccountPayableDetail, bool>> predicate, Expression<Func<AccountPayableDetail, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.accountPayableDetailRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<AccountPayableDetail> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<AccountPayableDetailView> container = new PageListViewContainer<AccountPayableDetailView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                AccountPayableDetailView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<AccountPayableDetailView> MapToView(AccountPayableDetail inputObject)
        {
            AccountPayableDetailView outObject = mapper.Map<AccountPayableDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfAccountPayableDetail.AccountPayableDetailNumber.ToString());
        }
 public override async Task<AccountPayableDetailView> GetViewById(long ? accountPyableDetailId)
        {
            AccountPayableDetail detailItem = await _unitOfWork.accountPayableDetailRepository.GetEntityById(accountPyableDetailId);

            return await MapToView(detailItem);
        }
 public async Task<AccountPayableDetailView> GetViewByNumber(long accountPyableDetailNumber)
        {
            AccountPayableDetail detailItem = await _unitOfWork.accountPayableDetailRepository.GetEntityByNumber(accountPyableDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<AccountPayableDetail> GetEntityById(long ? accountPyableDetailId)
        {
            return await _unitOfWork.accountPayableDetailRepository.GetEntityById(accountPyableDetailId);

        }
 public async Task<AccountPayableDetail> GetEntityByNumber(long accountPyableDetailNumber)
        {
            return await _unitOfWork.accountPayableDetailRepository.GetEntityByNumber(accountPyableDetailNumber);
        }
}
}
