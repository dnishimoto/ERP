using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{
    public class FluentChartOfAccountQuery : MapperAbstract<ChartOfAccount,ChartOfAccountView>, IFluentChartOfAccountQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentChartOfAccountQuery() { }
        public FluentChartOfAccountQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<ChartOfAccount> MapToEntity(ChartOfAccountView inputObject)
        {
           
            ChartOfAccount outObject = mapper.Map<ChartOfAccount>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ChartOfAccount>> MapToEntity(IList<ChartOfAccountView> inputObjects)
        {
            
            IList<ChartOfAccount> list = new List<ChartOfAccount>();
  
            foreach (var item in inputObjects)
            {
                ChartOfAccount outObject = mapper.Map<ChartOfAccount>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<ChartOfAccountView> GetViewById(long? carrierId)
        {
            ChartOfAccount detailItem = await _unitOfWork.chartOfAccountRepository.GetEntityById(carrierId);

            return await MapToView(detailItem);
        }
        public override async Task<ChartOfAccountView> MapToView(ChartOfAccount inputObject)
        {
     
            ChartOfAccountView outObject = mapper.Map<ChartOfAccountView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<IList<ChartOfAccountView>> GetViewsByIds(long[] accountIds)
        {
            try
            {
                IList<ChartOfAccount> list = await _unitOfWork.chartOfAccountRepository.GetEntitiesByIds(accountIds);

                IList<ChartOfAccountView> retList = new List<ChartOfAccountView>();
                foreach (var item in list)
                {
                    retList.Add(await MapToView(item));
                }
                return retList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<ChartOfAccountView>> GetViewsByAccount(string companyNumber, string busUnit, string objectNumber, string subsidiary)
        {
            IList<ChartOfAccount> list = await _unitOfWork.chartOfAccountRepository.GetEntitiesByAccount(companyNumber, busUnit, objectNumber, subsidiary);

            IList<ChartOfAccountView> retList = new List<ChartOfAccountView>();
            foreach (var item in list)
            {
                retList.Add(await MapToView(item));
            }
            return retList;
        }

        public async Task<Company> GetCompany()
        {
            return await _unitOfWork.companyRepository.GetCompany();
        }

        public async Task<ChartOfAccount> GetEntity(string company, string busUnit, string objectNumber, string subsidiary)
        {
            ChartOfAccount coa =  await _unitOfWork.chartOfAccountRepository.GetChartofAccount(company, busUnit, objectNumber, subsidiary);
          
            return coa;
        }
        public override async Task<ChartOfAccount> GetEntityById(long ? accountId)
        {
            return await _unitOfWork.chartOfAccountRepository.GetEntityById(accountId);

        }
        public async Task<ChartOfAccount> GetEntityByNumber(long purchaseOrderNumber)
        {
            return await _unitOfWork.chartOfAccountRepository.GetEntityByNumber(purchaseOrderNumber);
        }
    }
}
