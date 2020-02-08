





CREATE VIEW [dbo].[CustomerView] AS
SELECT Customer.CustomerId, Customer.AddressId, AddressBook.Name, 
LocationAddress.LocationAddressId, LocationAddress.[Address Line 1], 
LocationAddress.[Address Line 2], LocationAddress.City, LocationAddress.State, 
                  LocationAddress.Zipcode, LocationAddress.Country, LocationAddress.TypeXRefId, 
PhoneEntity.phonenumber,
PhoneEntity.phonetype,
EmailEntity.email,
locationType_udc.value locationType
FROM     Customer LEFT JOIN
                  LocationAddress ON Customer.AddressId = LocationAddress.AddressId LEFT OUTER JOIN
                  AddressBook ON Customer.AddressId = AddressBook.AddressId LEFT OUTER JOIN
                  phoneEntity ON phoneEntity.addressid = Customer.AddressId LEFT OUTER JOIN
                  emailEntity ON emailEntity.addressid = Customer.AddressId   LEFT OUTER join
		  udc locationType_udc on locationType_udc.xrefid=LocationAddress.TypeXRefId


