using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class FluentPayRollCurrentPaySequenceQuery : IFluentPayRollCurrentPaySequenceQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollCurrentPaySequenceQuery() { }
        public FluentPayRollCurrentPaySequenceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<PayRollCurrentPaySequence> MapToEntity(PayRollCurrentPaySequenceView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollCurrentPaySequence outObject = mapper.Map<PayRollCurrentPaySequence>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<List<PayRollCurrentPaySequence>> MapToEntity(List<PayRollCurrentPaySequenceView> inputObjects)
        {
            List<PayRollCurrentPaySequence> list = new List<PayRollCurrentPaySequence>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollCurrentPaySequence outObject = mapper.Map<PayRollCurrentPaySequence>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<PayRollCurrentPaySequenceView> MapToView(PayRollCurrentPaySequence inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollCurrentPaySequenceView outObject = mapper.Map<PayRollCurrentPaySequenceView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.payRollCurrentPaySequenceRepository.GetNextNumber(TypeOfPayRoll.PayRollCurrentPaySequenceNumber.ToString());
        }
        public async Task<PayRollCurrentPaySequenceView> GetViewById(long payRollCurrentPaySequenceId)
        {
            PayRollCurrentPaySequence detailItem = await _unitOfWork.payRollCurrentPaySequenceRepository.GetEntityById(payRollCurrentPaySequenceId);

            return await MapToView(detailItem);
        }
        public async Task<PayRollCurrentPaySequenceView> GetViewByNumber(long payRollCurrentPaySequenceNumber)
        {
            PayRollCurrentPaySequence detailItem = await _unitOfWork.payRollCurrentPaySequenceRepository.GetEntityByNumber(payRollCurrentPaySequenceNumber);

            return await MapToView(detailItem);
        }

        public async Task<PayRollCurrentPaySequence> GetEntityById(long payRollCurrentPaySequenceId)
        {
            return await _unitOfWork.payRollCurrentPaySequenceRepository.GetEntityById(payRollCurrentPaySequenceId);

        }
        public async Task<PayRollCurrentPaySequence> GetEntityByNumber(long payRollCurrentPaySequenceNumber)
        {
            return await _unitOfWork.payRollCurrentPaySequenceRepository.GetEntityByNumber(payRollCurrentPaySequenceNumber);
        }
        public async Task<PayRollCurrentPaySequenceView> GetViewByPayRollCode(long payRollGroupCode)
        {
            return await MapToView(await _unitOfWork.payRollCurrentPaySequenceRepository.GetViewByPayRollCode(payRollGroupCode));
        }
    } 
}
