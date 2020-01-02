using lssWebApi2.AbstractFactory;
using lssWebApi2.BudgetNoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.BudgetDomain;

namespace lssWebApi2.BudgetNoteDomain
{
    public class BudgetNoteModule : AbstractModule
    {
        public FluentBudgetNote BudgetNote = new FluentBudgetNote();
        public FluentBudget Budget = new FluentBudget();
    }
}
