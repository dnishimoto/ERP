USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[BudgetView]    Script Date: 7/30/2018 6:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[BudgetView]
as
select 
budgetRange.RangeId,
budgetRange.StartDate,
budgetRange.EndDate,
budgetRange.AccountId,
budgetRange.SupervisorCode,
budgetRange.LastUpdated,
chartOfAccounts.Location,
chartOfAccounts.CompanyNumber,
chartOfAccounts.BusUnit,
chartOfAccounts.ObjectNumber,
chartOfAccounts.Description,
Budget.budgetHours,
Budget.budgetAmount,
Budget.actualHours,
Budget.ActualAmount,
Budget.ProjectedHours,
Budget.ProjectedAmount
 from budgetrange budgetRange
inner join chartofaccts chartOfAccounts on budgetRange.accountid=chartOfAccounts.accountid
inner join budget budget on budgetRange.rangeid=budget.rangeid
GO
