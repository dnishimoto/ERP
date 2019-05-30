Create view IncomeView
as
select coa.Description,
coa.Account,
coa.AccountId,
gl.GeneralLedgerId,
gl.DocType,
gl.LedgerType,
gl.AddressId,
ab.Name,
gl.Amount,
gl.GLDate,
gl.FiscalPeriod,
gl.FiscalYear
 from chartOfAccts coa
join GeneralLedger gl
	on coa.AccountId=gl.AccountId
join AddressBook ab
	on gl.AddressId=ab.AddressId
where busUnit='1200' and objectNumber='300'