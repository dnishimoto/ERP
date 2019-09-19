using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
    public class FluentPayRollGroupQuery : IFluentPayRollGroupQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollGroupQuery() { }
        public FluentPayRollGroupQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<PayRollGroup> MapToEntity(PayRollGroupView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollGroup outObject = mapper.Map<PayRollGroup>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<List<PayRollGroup>> MapToEntity(List<PayRollGroupView> inputObjects)
        {
            List<PayRollGroup> list = new List<PayRollGroup>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollGroup outObject = mapper.Map<PayRollGroup>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<PayRollGroupView> MapToView(PayRollGroup inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollGroupView outObject = mapper.Map<PayRollGroupView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.payRollGroupRepository.GetNextNumber(TypeOfNextNumberEnum.PayRollGroupNumber.ToString());
        }
        public async Task<PayRollGroupView> GetViewById(long payRollGroupId)
        {
            PayRollGroup detailItem = await _unitOfWork.payRollGroupRepository.GetEntityById(payRollGroupId);

            return await MapToView(detailItem);
        }
        public async Task<PayRollGroupView> GetViewByNumber(long payRollGroupNumber)
        {
            PayRollGroup detailItem = await _unitOfWork.payRollGroupRepository.GetEntityByNumber(payRollGroupNumber);

            return await MapToView(detailItem);
        }

        public async Task<PayRollGroup> GetEntityById(long payRollGroupId)
        {
            return await _unitOfWork.payRollGroupRepository.GetEntityById(payRollGroupId);

        }
        public async Task<PayRollGroup> GetEntityByNumber(long payRollGroupNumber)
        {
            return await _unitOfWork.payRollGroupRepository.GetEntityByNumber(payRollGroupNumber);
        }
        public async Task<PayRollGroupView> GetViewByCode(int code)
        {
            PayRollGroup detailItem = await _unitOfWork.payRollGroupRepository.GetEntityByCode(code);
            return await MapToView(detailItem);
        }


    }
}
