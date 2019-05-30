
CREATE View [dbo].[SupplierView]
as
select 

AB.companyname SupplierName 
,AB.Name ContactName
,AB.AddressId
,supplier.SupplierId
from addressbook AB 
	join Supplier Supplier
		on AB.AddressId=Supplier.AddressId
