using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;

using ERP_Core2.FluentAPI;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{

       
    public class BudgetModule
    {
        public FluentBudget Budget = new FluentBudget();
        public FluentBudgetRange BudgetRange = new FluentBudgetRange();
    }
}
