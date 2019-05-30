



CREATE procedure [dbo].[usp_RollupGeneralLedgerBalance]
(
@AccountId as varchar(20),
@FiscalPeriod as int,
@FiscalYear as int,
@DocType as varchar(20)

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
  accountid=@AccountId
  and  fiscalyear=@FiscalYear 
   and fiscalperiod=@FiscalPeriod
   and docType=@DocType
   group by accountid, fiscalperiod, fiscalyear,ledgertype
  ) accountbalances
  where
   AccountBalance.accountid=accountbalances.accountid
   and
   AccountBalance.AccountId=@AccountId
   and
   AccountBalance.FiscalPeriod=@FiscalPeriod
   and
   AccountBalance.FiscalYear=@FiscalYear
  
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
