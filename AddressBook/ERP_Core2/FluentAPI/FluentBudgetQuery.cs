﻿using ERP_Core2.AbstractFactory;
using ERP_Core2.BudgetDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentBudgetQuery : AbstractModule, IBudgetQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentBudgetQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public BudgetActualsView GetBudgetActualsView(BudgetRangeView budgetRangeView)
        {
            Task<BudgetActualsView> budgetActualsViewTask = Task.Run(async () => await _unitOfWork.budgetRepository.GetActualsView(budgetRangeView));
            Task.WaitAll(budgetActualsViewTask);
            return budgetActualsViewTask.Result;
        }
        public BudgetView GetBudgetView(long budgetId)
        {
            Task<BudgetView> budgetViewTask = Task.Run(async () => await _unitOfWork.budgetRepository.GetBudgetView(budgetId));
            Task.WaitAll(budgetViewTask);
            return budgetViewTask.Result;
        }
    }
}
