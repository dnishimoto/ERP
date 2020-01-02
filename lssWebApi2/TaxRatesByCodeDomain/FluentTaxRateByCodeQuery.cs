using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.TaxRatesByCodeDomain
{
    public class FluentTaxRatesByCodeQuery : MapperAbstract<TaxRatesByCode,TaxRatesByCodeView>,IFluentTaxRatesByCodeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentTaxRatesByCodeQuery() { }
        public FluentTaxRatesByCodeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<TaxRatesByCode> MapToEntity(TaxRatesByCodeView inputObject)
        {

            TaxRatesByCode outObject = mapper.Map<TaxRatesByCode>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<TaxRatesByCode>> MapToEntity(List<TaxRatesByCodeView> inputObjects)
        {
            List<TaxRatesByCode> list = new List<TaxRatesByCode>();

            foreach (var item in inputObjects)
            {
                TaxRatesByCode outObject = mapper.Map<TaxRatesByCode>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<TaxRatesByCodeView> MapToView(TaxRatesByCode inputObject)
        {

            TaxRatesByCodeView outObject = mapper.Map<TaxRatesByCodeView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetNextNumber(TypeOfNextNumberEnum.TaxRatesByCodeNumber.ToString());
        }
        public override async Task<TaxRatesByCodeView> GetViewById(long ? TaxRatesByCodeId)
        {
            TaxRatesByCode detailItem = await _unitOfWork.taxRatesByCodeRepository.GetEntityById(TaxRatesByCodeId);

            return await MapToView(detailItem);
        }
        public async Task<TaxRatesByCodeView> GetViewByNumber(long taxRatesByCodeNumber)
        {
            TaxRatesByCode detailItem = await _unitOfWork.taxRatesByCodeRepository.GetEntityByNumber(taxRatesByCodeNumber);

            return await MapToView(detailItem);
        }

        public async Task<TaxRatesByCodeView> GetViewByCode(string code)
        {
            TaxRatesByCode detailItem = await _unitOfWork.taxRatesByCodeRepository.GetEntityByCode(code);

            return await MapToView(detailItem);
        }

        public override async Task<TaxRatesByCode> GetEntityById(long ? taxRatesByCodeId)
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetEntityById(taxRatesByCodeId);

        }
        public async Task<TaxRatesByCode> GetEntityByNumber(long taxRatesByCodeNumber)
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetEntityByNumber(taxRatesByCodeNumber);
        }


    }
}