using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.TaxRatesByCodeDomain
{
    public class FluentTaxRatesByCodeQuery : IFluentTaxRatesByCodeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentTaxRatesByCodeQuery() { }
        public FluentTaxRatesByCodeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<TaxRatesByCode> MapToEntity(TaxRatesByCodeView inputObject)
        {
            Mapper mapper = new Mapper();
            TaxRatesByCode outObject = mapper.Map<TaxRatesByCode>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<List<TaxRatesByCode>> MapToEntity(List<TaxRatesByCodeView> inputObjects)
        {
            List<TaxRatesByCode> list = new List<TaxRatesByCode>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                TaxRatesByCode outObject = mapper.Map<TaxRatesByCode>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<TaxRatesByCodeView> MapToView(TaxRatesByCode inputObject)
        {
            Mapper mapper = new Mapper();
            TaxRatesByCodeView outObject = mapper.Map<TaxRatesByCodeView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetNextNumber("TaxRatesByCodeNumber");
        }
        public async Task<TaxRatesByCodeView> GetViewById(long TaxRatesByCodeId)
        {
            TaxRatesByCode detailItem = await _unitOfWork.taxRatesByCodeRepository.GetEntityById(TaxRatesByCodeId);

            return await MapToView(detailItem);
        }
        public async Task<TaxRatesByCodeView> GetViewByNumber(long taxRatesByCodeNumber)
        {
            TaxRatesByCode detailItem = await _unitOfWork.taxRatesByCodeRepository.GetEntityByNumber(taxRatesByCodeNumber);

            return await MapToView(detailItem);
        }

        public async Task<TaxRatesByCode> GetEntityById(long taxRatesByCodeId)
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetEntityById(taxRatesByCodeId);

        }
        public async Task<TaxRatesByCode> GetEntityByNumber(long taxRatesByCodeNumber)
        {
            return await _unitOfWork.taxRatesByCodeRepository.GetEntityByNumber(taxRatesByCodeNumber);
        }


    }
}