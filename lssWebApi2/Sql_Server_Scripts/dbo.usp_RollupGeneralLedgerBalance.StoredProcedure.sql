USE [listensoftwareDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_RollupGeneralLedgerBalance]    Script Date: 7/30/2018 6:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_RollupGeneralLedgerBalance]
(
@AccountId as varchar(20),
@FiscalPeriod as int,
@FiscalYear as int

)
as

declare @Count as int;

select @Count=count(*) from accountbalance
where accountid=@AccountId and fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod

begin transaction
if @Count>0 
begin
  update  AccountBalance     
  set amount=accountbalances.amount

  from 
  (
  select accountid, fiscalperiod, fiscalyear, ledgertype, Sum(amount) Amount
  from generalledger gl 
  where
   fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod
   group by accountid, fiscalperiod, fiscalyear,ledgertype
  ) accountbalances
  where
   AccountBalance.accountid=accountbalances.accountid
   and
   AccountBalance.AccountId=@AccountId
  
 end
else
begin
insert into AccountBalance
(accountid
,AccountBalanceType
,FiscalYear
,FiscalPeriod
,Amount
)
select accountId, 
ledgertype,fiscalyear, 
fiscalperiod,
sum(Amount) Amount from generalledger
group by fiscalyear, accountid, fiscalperiod,ledgertype
having accountid=@AccountId and fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod
end

commit transaction
GO
