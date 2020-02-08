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

namespace lssWebApi2.AccountReceivableDetailDomain
{
public class FluentAccountReceivableDetailQuery:MapperAbstract<AccountReceivableDetail,AccountReceivableDetailView>,IFluentAccountReceivableDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountReceivableDetailQuery() { }
        public FluentAccountReceivableDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<AccountReceivableDetail> MapToEntity(AccountReceivableDetailView inputObject)
        {
            AccountReceivableDetail outObject = mapper.Map<AccountReceivableDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<AccountReceivableDetail>> MapToEntity(IList<AccountReceivableDetailView> inputObjects)
        {
            IList<AccountReceivableDetail> list = new List<AccountReceivableDetail>();
            foreach (var item in inputObjects)
            {
                AccountReceivableDetail outObject = mapper.Map<AccountReceivableDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<AccountReceivableDetailView>> GetViewsByPage(Expression<Func<AccountReceivableDetail, bool>> predicate, Expression<Func<AccountReceivableDetail, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.accountReceivableDetailRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<AccountReceivableDetail> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<AccountReceivableDetailView> container = new PageListViewContainer<AccountReceivableDetailView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                AccountReceivableDetailView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<AccountReceivableDetailView> MapToView(AccountReceivableDetail inputObject)
        {
            AccountReceivableDetailView outObject = mapper.Map<AccountReceivableDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfAccountReceivableDetail.AccountReceivableDetailNumber.ToString());
        }
 public override async Task<AccountReceivableDetailView> GetViewById(long ? accountReceivableDetailId)
        {
            AccountReceivableDetail detailItem = await _unitOfWork.accountReceivableDetailRepository.GetEntityById(accountReceivableDetailId);

            return await MapToView(detailItem);
        }
 public async Task<AccountReceivableDetailView> GetViewByNumber(long accountReceivableDetailNumber)
        {
            AccountReceivableDetail detailItem = await _unitOfWork.accountReceivableDetailRepository.GetEntityByNumber(accountReceivableDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<AccountReceivableDetail> GetEntityById(long ? accountReceivableDetailId)
        {
            return await _unitOfWork.accountReceivableDetailRepository.GetEntityById(accountReceivableDetailId);

        }
 public async Task<AccountReceivableDetail> GetEntityByNumber(long accountReceivableDetailNumber)
        {
            return await _unitOfWork.accountReceivableDetailRepository.GetEntityByNumber(accountReceivableDetailNumber);
        }
}
}
