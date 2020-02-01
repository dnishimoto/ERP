using lssWebApi2.AbstractFactory;
using lssWebApi2.BudgetNoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.BudgetDomain;
using lssWebApi2.Services;

namespace lssWebApi2.BudgetNoteDomain
{
    public class BudgetNoteModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentBudgetNote BudgetNote;
        public FluentBudget Budget;
        public BudgetNoteModule()
        {
            unitOfWork = new UnitOfWork();
            BudgetNote = new FluentBudgetNote(unitOfWork);
            Budget = new FluentBudget(unitOfWork);
        }
    }
}
