using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.UDCDomain
{
    public class FluentUdcQuery : MapperAbstract<Udc,UdcView>,IFluentUdcQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentUdcQuery() { }
        public FluentUdcQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<Udc> MapToEntity(UdcView inputObject)
        {

            Udc outObject = mapper.Map<Udc>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Udc>> MapToEntity(IList<UdcView> inputObjects)
        {
            IList<Udc> list = new List<Udc>();

            foreach (var item in inputObjects)
            {
                Udc outObject = mapper.Map<Udc>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<UdcView> MapToView(Udc inputObject)
        {

            UdcView outObject = mapper.Map<UdcView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.udcRepository.GetNextNumber(TypeOfUdc.UdcNumber.ToString());
        }
        public override async Task<UdcView> GetViewById(long ? udcId)
        {
            Udc detailItem = await _unitOfWork.udcRepository.GetEntityById(udcId);

            return await MapToView(detailItem);
        }
        public async Task<UdcView> GetViewByNumber(long udcNumber)
        {
            Udc detailItem = await _unitOfWork.udcRepository.GetEntityByNumber(udcNumber);

            return await MapToView(detailItem);
        }

        public override async Task<Udc> GetEntityById(long ? udcId)
        {
            return await _unitOfWork.udcRepository.GetEntityById(udcId);

        }
        public async Task<Udc> GetEntityByNumber(long udcNumber)
        {
            return await _unitOfWork.udcRepository.GetEntityByNumber(udcNumber);
        }
        public async Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode)
        {
            return await _unitOfWork.udcRepository.GetUDCValuesByProductCode(productCode);
        }
        public async Task<Udc> GetUdc(string productCode, string keyCode)
        {
            return await _unitOfWork.udcRepository.GetUdc(productCode, keyCode);
        }
    }
}
