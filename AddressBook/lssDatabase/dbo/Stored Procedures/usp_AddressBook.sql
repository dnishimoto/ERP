
CREATE PROCEDURE [dbo].[usp_AddressBook]( @Id as BigInt)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	select
	[AddressId] 
	,[Name]
	,[FirstName]
	,[LastName] 
	from [dbo].[AddressBook]
where [AddressId]=@Id


END

