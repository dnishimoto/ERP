using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.BudgetNoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.BudgetNoteDomain
{

    public class UnitBudgetNote
    {

        private readonly ITestOutputHelper output;

        public UnitBudgetNote(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            BudgetNoteModule BudgetNoteMod = new BudgetNoteModule();
            Budget budget = await BudgetNoteMod.Budget.Query().GetEntityById(2);

           BudgetNoteView view = new BudgetNoteView()
            {
                   BudgetId=budget.BudgetId,
                   Note= "Test Note"

            };
            NextNumber nnNextNumber = await BudgetNoteMod.BudgetNote.Query().GetNextNumber();

            view.BudgetNoteNumber = nnNextNumber.NextNumberValue;

            BudgetNote budgetNote = await BudgetNoteMod.BudgetNote.Query().MapToEntity(view);

            BudgetNoteMod.BudgetNote.AddBudgetNote(budgetNote).Apply();

            BudgetNote newBudgetNote = await BudgetNoteMod.BudgetNote.Query().GetEntityByNumber(view.BudgetNoteNumber);

            Assert.NotNull(newBudgetNote);

            newBudgetNote.Note = "Test Note Update";

            BudgetNoteMod.BudgetNote.UpdateBudgetNote(newBudgetNote).Apply();

            BudgetNoteView updateView = await BudgetNoteMod.BudgetNote.Query().GetViewById(newBudgetNote.BudgetNoteId);

            Assert.Same(updateView.Note, "Test Note Update");
              BudgetNoteMod.BudgetNote.DeleteBudgetNote(newBudgetNote).Apply();
            BudgetNote lookupBudgetNote= await BudgetNoteMod.BudgetNote.Query().GetEntityById(view.BudgetNoteId);

            Assert.Null(lookupBudgetNote);
        }
       
      

    }
}
