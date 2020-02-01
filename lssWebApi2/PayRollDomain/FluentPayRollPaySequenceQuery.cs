using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class FluentPayRollPaySequenceQuery : IFluentPayRollPaySequenceQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollPaySequenceQuery() { }
        public FluentPayRollPaySequenceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<PayRollPaySequence> MapToEntity(PayRollPaySequenceView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollPaySequence outObject = mapper.Map<PayRollPaySequence>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<IList<PayRollPaySequence>> MapToEntity(IList<PayRollPaySequenceView> inputObjects)
        {
            IList<PayRollPaySequence> list = new List<PayRollPaySequence>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollPaySequence outObject = mapper.Map<PayRollPaySequence>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<PayRollPaySequenceView> MapToView(PayRollPaySequence inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollPaySequenceView outObject = mapper.Map<PayRollPaySequenceView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfPayRoll.PayRollPaySequenceNumber.ToString());
        }
        public async Task<PayRollPaySequenceView> GetViewById(long payRollPaySequenceId)
        {
            PayRollPaySequence detailItem = await _unitOfWork.payRollPaySequenceRepository.GetEntityById(payRollPaySequenceId);

            return await MapToView(detailItem);
        }
        public async Task<PayRollPaySequenceView> GetViewByNumber(long payRollPaySequenceNumber)
        {
            PayRollPaySequence detailItem = await _unitOfWork.payRollPaySequenceRepository.GetEntityByNumber(payRollPaySequenceNumber);

            return await MapToView(detailItem);
        }

        public async Task<PayRollPaySequence> GetEntityById(long payRollPaySequenceId)
        {
            return await _unitOfWork.payRollPaySequenceRepository.GetEntityById(payRollPaySequenceId);

        }
        public async Task<PayRollPaySequence> GetEntityByNumber(long payRollPaySequenceNumber)
        {
            return await _unitOfWork.payRollPaySequenceRepository.GetEntityByNumber(payRollPaySequenceNumber);
        }
        public long GetMaxPaySequenceByGroupCode(long payRollGroupCode)
        {
            return _unitOfWork.payRollPaySequenceRepository.GetMaxPaySequenceByGroupCode(payRollGroupCode);
        }
        public async Task<PayRollPaySequenceView> GetCurrentPaySequenceByGroupCode(long payRollGroupCode)
        {
            var results = await _unitOfWork.payRollPaySequenceRepository.GetCurrentPaySequenceByGroupCode(payRollGroupCode);

            return await MapToView(results);
        }
        public async Task<PayRollPaySequenceView> GetByDateRangeAndCode(
                    DateTime payRollBeginDate, DateTime payRollEndDate, int payRollGroupCode)
        {
            return await MapToView(await _unitOfWork.payRollPaySequenceRepository.GetByDateRangeAndCode(payRollBeginDate, payRollEndDate, payRollGroupCode));
        }
    }
}
