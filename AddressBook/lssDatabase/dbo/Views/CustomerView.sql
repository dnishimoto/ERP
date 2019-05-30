


CREATE VIEW [dbo].[CustomerView] AS
SELECT Customer.CustomerId, Customer.AddressId, AddressBook.Name, 
LocationAddress.LocationId, LocationAddress.[Address Line 1], 
LocationAddress.[Address Line 2], LocationAddress.City, LocationAddress.State, 
                  LocationAddress.Zipcode, LocationAddress.Country, LocationAddress.TypeXRefId, 
phones.Phonenumber,
phones.Phonetype,
emails.Email,
locationType_udc.value locationType
FROM     Customer INNER JOIN
                  LocationAddress ON Customer.AddressId = LocationAddress.AddressId LEFT OUTER JOIN
                  AddressBook ON Customer.AddressId = AddressBook.AddressId LEFT OUTER JOIN
                  phones ON phones.addressid = Customer.AddressId LEFT OUTER JOIN
                  emails ON emails.addressid = Customer.AddressId   join
		  udc locationType_udc on locationType_udc.xrefid=LocationAddress.TypeXRefId


