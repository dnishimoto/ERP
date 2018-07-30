USE [listensoftwareDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetNextNumber]    Script Date: 7/30/2018 6:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetNextNumber]( @NextNumberName as varchar(20))
as
begin
begin transaction
select * into #temp from NextNumber where NextNumberName = @NextNumberName
update NextNumber set nextNumberValue+=1 where NextNumberName=@NextNumberName;
commit transaction
select * from #temp
end
GO
