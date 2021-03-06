USE [listensoftwareDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddressBook]    Script Date: 7/30/2018 6:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddressBook]( @Id as BigInt)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	select
	[Id] 
	,[Name]
	,[FirstName]
	,[LastName] 
	,[Company] 
	,[CellPhone]
	,[MailingCity]
	,[MailingState]
	,[MailingAddress]
	,[MailingZipcode]
	,[BillingCity] 
	,[BillingState] 
	,[BillingZipcode]
	,[BillingAddress]
from [dbo].[AddressBook]
where [Id]=@Id


END

GO
