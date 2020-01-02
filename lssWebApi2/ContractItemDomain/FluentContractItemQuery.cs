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

namespace lssWebApi2.ContractItemDomain
{
public class FluentContractItemQuery:MapperAbstract<ContractItem,ContractItemView>,IFluentContractItemQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentContractItemQuery() { }
        public FluentContractItemQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ContractItem> MapToEntity(ContractItemView inputObject)
        {
            ContractItem outObject = mapper.Map<ContractItem>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<ContractItem>> MapToEntity(List<ContractItemView> inputObjects)
        {
            List<ContractItem> list = new List<ContractItem>();
            foreach (var item in inputObjects)
            {
                ContractItem outObject = mapper.Map<ContractItem>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<ContractItemView>> GetViewsByPage(Expression<Func<ContractItem, bool>> predicate, Expression<Func<ContractItem, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.contractItemRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<ContractItem> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<ContractItemView> container = new PageListViewContainer<ContractItemView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                ContractItemView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<ContractItemView> MapToView(ContractItem inputObject)
        {
            ContractItemView outObject = mapper.Map<ContractItemView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.contractItemRepository.GetNextNumber(TypeOfContractItem.ContractItemNumber.ToString());
        }
 public override async Task<ContractItemView> GetViewById(long ? contractItemId)
        {
            ContractItem detailItem = await _unitOfWork.contractItemRepository.GetEntityById(contractItemId);

            return await MapToView(detailItem);
        }
 public async Task<ContractItemView> GetViewByNumber(long contractItemNumber)
        {
            ContractItem detailItem = await _unitOfWork.contractItemRepository.GetEntityByNumber(contractItemNumber);

            return await MapToView(detailItem);
        }

public override async Task<ContractItem> GetEntityById(long ? contractItemId)
        {
            return await _unitOfWork.contractItemRepository.GetEntityById(contractItemId);

        }
 public async Task<ContractItem> GetEntityByNumber(long contractItemNumber)
        {
            return await _unitOfWork.contractItemRepository.GetEntityByNumber(contractItemNumber);
        }
}
}
