






CREATE View [dbo].[InvoiceAndDetailAndCustomerView]
as
select 
invoice.InvoiceId,
invoice.InvoiceNumber,
invoice.InvoiceDate,
invoice.Amount,
invoice.Description,
invoice.TaxAmount,
invoice.PaymentDueDate,
invoice.PaymentTerms,
itemmaster.description ItemDescription,
itemmaster.itemnumber,
invoicesDetail.UnitPrice,
invoicesDetail.Quantity,
invoicesDetail.Amount DetailAmount,
invoicesDetail.DiscountPercent,
invoicesDetail.DiscountAmount,
invoicesDetail.ExtendedDescription DetailExtendedDescription,
invoice.companyid,
InvoiceHeader.CompanyName,
InvoiceHeader.CompanyCode,
InvoiceHeader.CompanyStreet,
InvoiceHeader.CompanyCity,
InvoiceHeader.CompanyState,
InvoiceHeader.CompanyZipcode,
InvoiceHeader.CustomerName,

InvoiceHeader.[Address Line 1] customerAddress1,
InvoiceHeader.[Address Line 2] customerAddress2,
InvoiceHeader.CustomerCity,
InvoiceHeader.CustomerState,
InvoiceHeader.CustomerZipcode,
InvoiceHeader.CustomerLocationType

from invoice invoice
left join invoiceDetail invoicesDetail on invoice.invoiceId=invoicesDetail.invoiceId
inner join itemmaster itemmaster on invoicesdetail.itemid=itemmaster.itemid
outer apply
(
SELECT 
				addressBook.Name AS CustomerName, 
				Company.CompanyName, Company.CompanyCode, 
				Company.CompanyStreet, 
				Company.CompanyCity, 
				Company.CompanyState, 
				Company.CompanyZipcode, 
				  customerLocationAddress.[Address Line 1],
				  customerLocationAddress.[Address Line 2],
				  customerLocationAddress.City CustomerCity,
				  customerLocationAddress.State customerState,
				  customerLocationAddress.Zipcode CustomerZipcode, 
				  locationTypeudc.Value  CustomerLocationType
FROM     Company company INNER JOIN
                 
                  Customer AS customer 
						ON customer.CustomerId = Invoice.CustomerId 
					JOIN AddressBook AS addressBook 
						ON customer.AddressId = addressBook.AddressId  
					left Join  LocationAddress customerLocationAddress 
						on customerLocationAddress.AddressId=customer.AddressId  
					left join  udc locationTypeUdc 
						on locationTypeUdc.xrefid=customerLocationAddress.TypeXRefId
	  
	where company.CompanyId = Invoice.CompanyId 

) InvoiceHeader


