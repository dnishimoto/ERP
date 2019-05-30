/****** Script for SelectTopNRows command from SSMS  ******/
Create View AccountSummaryCY
as
SELECT
      gl.[AccountId],
	   gl.[FiscalPeriod],
	   gl.[FiscalYear],
	  coa.Description,
	  sum(Amount) Amount
  FROM [dbo].[GeneralLedger] gl 
	join [dbo].[ChartOfAccts] coa
		on gl.AccountId=coa.AccountId
  where ledgertype='AA' and DocType='PV' and FiscalYear=datepart(Year,getDate())
  group by
        gl.[AccountId],
		 gl. [FiscalPeriod],
	 gl. [FiscalYear],
	  coa.Description