

CREATE PROCEDURE [dbo].[usp_GetNextNumber]( @NextNumberName as varchar(255))
as
begin
begin transaction
select * into #temp from NextNumber where NextNumberName = @NextNumberName
update NextNumber set nextNumberValue+=1 where NextNumberName=@NextNumberName;
commit transaction
select * from #temp
end
