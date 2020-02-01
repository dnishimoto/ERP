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

namespace lssWebApi2.NextNumberDomain
{
public class FluentNextNumberQuery:MapperAbstract<NextNumber,NextNumberView>,IFluentNextNumberQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentNextNumberQuery() { }
        public FluentNextNumberQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<NextNumber> MapToEntity(NextNumberView inputObject)
        {
            NextNumber outObject = mapper.Map<NextNumber>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<NextNumber>> MapToEntity(IList<NextNumberView> inputObjects)
        {
            IList<NextNumber> list = new List<NextNumber>();
            foreach (var item in inputObjects)
            {
                NextNumber outObject = mapper.Map<NextNumber>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<NextNumberView>> GetViewsByPage(Expression<Func<NextNumber, bool>> predicate, Expression<Func<NextNumber, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.nextNumberRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<NextNumber> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<NextNumberView> container = new PageListViewContainer<NextNumberView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                NextNumberView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<NextNumberView> MapToView(NextNumber inputObject)
        {
            NextNumberView outObject = mapper.Map<NextNumberView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfNextNumber.NextNumberValue.ToString());
        }
 public override async Task<NextNumberView> GetViewById(long ? nextNumberId)
        {
            NextNumber detailItem = await _unitOfWork.nextNumberRepository.GetEntityById(nextNumberId);

            return await MapToView(detailItem);
        }
 public async Task<NextNumberView> GetViewByNumber(long nextNumberNumber)
        {
            NextNumber detailItem = await _unitOfWork.nextNumberRepository.GetEntityByNumber(nextNumberNumber);

            return await MapToView(detailItem);
        }

public override async Task<NextNumber> GetEntityById(long ? nextNumberId)
        {
            return await _unitOfWork.nextNumberRepository.GetEntityById(nextNumberId);

        }
 public async Task<NextNumber> GetEntityByNumber(long nextNumberNumber)
        {
            return await _unitOfWork.nextNumberRepository.GetEntityByNumber(nextNumberNumber);
        }
}
}
