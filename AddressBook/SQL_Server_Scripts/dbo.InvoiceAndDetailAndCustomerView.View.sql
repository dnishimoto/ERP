USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[InvoiceAndDetailAndCustomerView]    Script Date: 7/30/2018 6:36:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE View [dbo].[InvoiceAndDetailAndCustomerView]
as
select 
invoices.InvoiceId,
invoices.InvoiceNumber,
invoices.InvoiceDate,
invoices.Amount,
invoices.Description,
invoices.TaxAmount,
invoices.PaymentDueDate,
invoices.PaymentTerms,
itemmaster.description ItemDescription,
itemmaster.itemnumber,
invoicesDetail.UnitPrice,
invoicesDetail.Quantity,
invoicesDetail.Amount DetailAmount,
invoicesDetail.DiscountPercent,
invoicesDetail.DiscountAmount,
invoicesDetail.ExtendedDescription DetailExtendedDescription,
invoices.companyid,
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

from invoice invoices
left join invoiceDetail invoicesDetail on invoices.invoiceId=invoicesDetail.invoiceId
inner join itemmaster itemmaster on invoicesdetail.itemid=itemmaster.itemid
outer apply
(
SELECT addressBook.Name AS CustomerName, Company.CompanyName, Company.CompanyCode, Company.CompanyStreet, Company.CompanyCity, Company.CompanyState, Company.CompanyZipcode, Invoice.InvoiceId, 
                  Invoice.InvoiceNumber,customerLocationAddress.[Address Line 1],customerLocationAddress.[Address Line 2],customerLocationAddress.City CustomerCity,
				  customerLocationAddress.State customerState,customerLocationAddress.Zipcode CustomerZipcode, locationTypeudc.Value  CustomerLocationType
FROM     Company INNER JOIN
                  Invoice ON Company.CompanyId = Invoice.CompanyId INNER JOIN
                  Customer AS customer ON customer.CustomerId = Invoice.CustomerId INNER JOIN
                  AddressBook AS addressBook ON customer.AddressId = addressBook.AddressId  left Join
				  LocationAddress customerLocationAddress on customerLocationAddress.AddressId=customer.AddressId  left join
				  udc locationTypeUdc on locationTypeUdc.xrefid=customerLocationAddress.TypeXRefId
	  
	where invoice.invoiceid=invoices.InvoiceId
) InvoiceHeader
GO
