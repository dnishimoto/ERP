CREATE view personalExpenseView
as
select coa.accountid,
coa.location,
coa.busUnit,
coa.objectNumber,
coa.supcode,
coa.Subsidiary,
coa.subsub,
coa.Account,
coa.Description,
coa.CompanyNumber,
bud.BudgetAmount,
bud.BudgetHours,
bud_range.StartDate,
bud_range.EndDate
 from chartofaccts coa
join budget bud
	on coa.Accountid=bud.AccountId
join budgetrange bud_range
	on bud.AccountId=bud_range.AccountId
	and bud_range.IsActive=1

where coa.objectnumber between 501 and 599


