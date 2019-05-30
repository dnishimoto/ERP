

CREATE View [dbo].[FinancialView]
as
select 
coa.CompanyNumber,
coa.Account,
coa.Level, 
coa.BusUnit,
coa.ObjectNumber,
coa.Subsidiary, 
coa.Description, 
Sum(month.Amount) PeriodAmount,
Sum(AcctBal.amount) AcctBalAmount,
Sum(YTD.amount) YTDAmount,
month.FiscalPeriod,
month.FiscalYear 
from ChartOfAccts COA
outer apply

( select sum(amount) Amount,ab.FiscalPeriod,ab.FiscalYear from AccountBalance AB
	where  COA.accountid=AB.AccountId
	group by AB.AccountId, ab.FiscalPeriod,ab.fiscalyear
)Month
outer apply
(
select sum(amount) Amount from AccountBalance AB
	where  COA.accountid=AB.AccountId

		and cast(cast(ab.fiscalyear as varchar) + right('0'+cast(ab.fiscalPeriod as varchar),2) as int) 
		<=
	cast(cast(Month.FiscalYear as varchar) + right('0'+cast(Month.FiscalPeriod as varchar),2) as int) 

	group by ab.accountid

)AcctBal
outer apply
(
select sum(amount) Amount From AccountBalance AB
where  COA.accountid=AB.AccountId
	and ab.FiscalYear=Month.FiscalYear
	and ab.FiscalPeriod<=Month.FiscalPeriod
)YTD
group by
coa.Account, 
coa.BusUnit,
coa.ObjectNumber,
coa.Subsidiary, 
coa.Description, 
month.FiscalPeriod,
month.FiscalYear,
coa.Level,
coa.CompanyNumber
