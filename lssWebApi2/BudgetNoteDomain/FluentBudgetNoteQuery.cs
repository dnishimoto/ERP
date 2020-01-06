using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetNoteDomain
{
    public class FluentBudgetNoteQuery : MapperAbstract<BudgetNote, BudgetNoteView>, IFluentBudgetNoteQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentBudgetNoteQuery() { }
        public FluentBudgetNoteQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<BudgetNote> MapToEntity(BudgetNoteView inputObject)
        {
            Mapper mapper = new Mapper();
            BudgetNote outObject = mapper.Map<BudgetNote>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<BudgetNote>> MapToEntity(IList<BudgetNoteView> inputObjects)
        {
            IList<BudgetNote> list = new List<BudgetNote>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                BudgetNote outObject = mapper.Map<BudgetNote>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<BudgetNoteView> MapToView(BudgetNote inputObject)
        {
            Mapper mapper = new Mapper();
            BudgetNoteView outObject = mapper.Map<BudgetNoteView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.budgetNoteRepository.GetNextNumber(TypeOfBudgetNote.BudgetNoteNumber.ToString());
        }
        public override async Task<BudgetNoteView> GetViewById(long? budgetNoteId)
        {
            BudgetNote detailItem = await _unitOfWork.budgetNoteRepository.GetEntityById(budgetNoteId);

            return await MapToView(detailItem);
        }
        public async Task<BudgetNoteView> GetViewByNumber(long budgetNoteNumber)
        {
            BudgetNote detailItem = await _unitOfWork.budgetNoteRepository.GetEntityByNumber(budgetNoteNumber);

            return await MapToView(detailItem);
        }

        public override async Task<BudgetNote> GetEntityById(long? budgetNoteId)
        {
            return await _unitOfWork.budgetNoteRepository.GetEntityById(budgetNoteId);
        }
        public async Task<BudgetNote> GetEntityByNumber(long budgetNoteNumber)
        {
            return await _unitOfWork.budgetNoteRepository.GetEntityByNumber(budgetNoteNumber);
        }
    }
}
