

CREATE View [dbo].[CustomerPrimaryShippedToView]
as
select 
addressBook.name,
shippedToAddresses.[AddressId]
      ,shippedToAddresses.[ShipToAddressLine1]
      ,shippedToAddresses.[ShipToAddressLine2]
      ,shippedToAddresses.[ShipToState]
      ,shippedToAddresses.[ShipToCity]
      ,shippedToAddresses.[ShipToZipcode]
	   from addressBook addressbook
left join shippedToAddresses shippedToAddresses
on addressbook.AddressId=shippedToAddresses.shippedToAddressId
