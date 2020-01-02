using lssWebApi2.AutoMapper;
using lssWebApi2.BudgetNoteDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentBudgetNoteQuery
{
    Task<BudgetNote> MapToEntity(BudgetNoteView inputObject);
    Task<List<BudgetNote>> MapToEntity(List<BudgetNoteView> inputObjects);
    Task<BudgetNoteView> MapToView(BudgetNote inputObject);
    Task<NextNumber> GetNextNumber();
    Task<BudgetNote> GetEntityById(long ? budgetNoteId);
    Task<BudgetNote> GetEntityByNumber(long budgetNoteNumber);
    Task<BudgetNoteView> GetViewById(long ? budgetNoteId);
    Task<BudgetNoteView> GetViewByNumber(long budgetNoteNumber);
}
