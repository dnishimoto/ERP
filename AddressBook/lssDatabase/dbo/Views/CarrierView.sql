Create View CarrierView
as
SELECT  [CarrierId]
      ,addressBook.[AddressId]
      ,[CarrierTypeXrefId]
	  ,addressBook.CompanyName
  FROM [dbo].[Carrier] carrier
  join [dbo].[AddressBook] addressBook
	on carrier.AddressId=addressBook.AddressId