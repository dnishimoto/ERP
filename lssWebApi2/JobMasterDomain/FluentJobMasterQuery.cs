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

namespace lssWebApi2.JobMasterDomain
{
public class FluentJobMasterQuery:MapperAbstract<JobMaster,JobMasterView>,IFluentJobMasterQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentJobMasterQuery() { }
        public FluentJobMasterQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<JobMaster> MapToEntity(JobMasterView inputObject)
        {
            JobMaster outObject = mapper.Map<JobMaster>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<JobMaster>> MapToEntity(IList<JobMasterView> inputObjects)
        {
            IList<JobMaster> list = new List<JobMaster>();
            foreach (var item in inputObjects)
            {
                JobMaster outObject = mapper.Map<JobMaster>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<JobMasterView>> GetViewsByPage(Expression<Func<JobMaster, bool>> predicate, Expression<Func<JobMaster, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.jobMasterRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<JobMaster> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<JobMasterView> container = new PageListViewContainer<JobMasterView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                JobMasterView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<JobMasterView> MapToView(JobMaster inputObject)
        {
            JobMasterView outObject = mapper.Map<JobMasterView>(inputObject);
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<Contract> contractTask = _unitOfWork.contractRepository.GetEntityById(inputObject.ContractId);
            Task.WaitAll(customerTask, contractTask);

            AddressBook addressBookCustomer = await _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result?.AddressId);

            outObject.CustomerName = addressBookCustomer?.Name;
            outObject.ContractTitle = contractTask.Result?.Title;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfJobMaster.JobMasterNumber.ToString());
        }
 public override async Task<JobMasterView> GetViewById(long ? jobMasterId)
        {
            JobMaster detailItem = await _unitOfWork.jobMasterRepository.GetEntityById(jobMasterId);

            return await MapToView(detailItem);
        }
 public async Task<JobMasterView> GetViewByNumber(long jobMasterNumber)
        {
            JobMaster detailItem = await _unitOfWork.jobMasterRepository.GetEntityByNumber(jobMasterNumber);

            return await MapToView(detailItem);
        }

public override async Task<JobMaster> GetEntityById(long ? jobMasterId)
        {
            return await _unitOfWork.jobMasterRepository.GetEntityById(jobMasterId);

        }
 public async Task<JobMaster> GetEntityByNumber(long jobMasterNumber)
        {
            return await _unitOfWork.jobMasterRepository.GetEntityByNumber(jobMasterNumber);
        }
}
}
