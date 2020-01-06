

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.BudgetNoteDomain;

namespace lssWebApi2.BudgetNoteDomain
{ 

public interface IFluentBudgetNote
    {
        IFluentBudgetNoteQuery Query();
        IFluentBudgetNote Apply();
        IFluentBudgetNote AddBudgetNote(BudgetNote budgetNote);
        IFluentBudgetNote UpdateBudgetNote(BudgetNote budgetNote);
        IFluentBudgetNote DeleteBudgetNote(BudgetNote budgetNote);
     	IFluentBudgetNote UpdateBudgetNotes(IList<BudgetNote> newObjects);
        IFluentBudgetNote AddBudgetNotes(List<BudgetNote> newObjects);
        IFluentBudgetNote DeleteBudgetNotes(List<BudgetNote> deleteObjects);
    }
}
