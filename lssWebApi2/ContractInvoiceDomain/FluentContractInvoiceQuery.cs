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

namespace lssWebApi2.ContractInvoiceDomain
{
public class FluentContractInvoiceQuery:MapperAbstract<ContractInvoice,ContractInvoiceView>,IFluentContractInvoiceQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentContractInvoiceQuery() { }
        public FluentContractInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ContractInvoice> MapToEntity(ContractInvoiceView inputObject)
        {
            ContractInvoice outObject = mapper.Map<ContractInvoice>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<ContractInvoice>> MapToEntity(IList<ContractInvoiceView> inputObjects)
        {
            IList<ContractInvoice> list = new List<ContractInvoice>();
            foreach (var item in inputObjects)
            {
                ContractInvoice outObject = mapper.Map<ContractInvoice>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<ContractInvoiceView>> GetViewsByPage(Expression<Func<ContractInvoice, bool>> predicate, Expression<Func<ContractInvoice, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.contractInvoiceRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<ContractInvoice> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<ContractInvoiceView> container = new PageListViewContainer<ContractInvoiceView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                ContractInvoiceView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<ContractInvoiceView> MapToView(ContractInvoice inputObject)
        {
            ContractInvoiceView outObject = mapper.Map<ContractInvoiceView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfContractInvoice.ContractInvoiceNumber.ToString());
        }
 public override async Task<ContractInvoiceView> GetViewById(long ? contractInvoiceId)
        {
            ContractInvoice detailItem = await _unitOfWork.contractInvoiceRepository.GetEntityById(contractInvoiceId);

            return await MapToView(detailItem);
        }
 public async Task<ContractInvoiceView> GetViewByNumber(long contractInvoiceNumber)
        {
            ContractInvoice detailItem = await _unitOfWork.contractInvoiceRepository.GetEntityByNumber(contractInvoiceNumber);

            return await MapToView(detailItem);
        }

public override async Task<ContractInvoice> GetEntityById(long ? contractInvoiceId)
        {
            return await _unitOfWork.contractInvoiceRepository.GetEntityById(contractInvoiceId);

        }
 public async Task<ContractInvoice> GetEntityByNumber(long contractInvoiceNumber)
        {
            return await _unitOfWork.contractInvoiceRepository.GetEntityByNumber(contractInvoiceNumber);
        }
}
}
