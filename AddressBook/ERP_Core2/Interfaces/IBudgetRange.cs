﻿using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IBudgetRange
    {
        IBudgetRange CreateBudgetRange(BudgetRangeView budgetRange);
        IBudgetRange Apply();
        IBudgetRangeQuery Query();
    }
}
