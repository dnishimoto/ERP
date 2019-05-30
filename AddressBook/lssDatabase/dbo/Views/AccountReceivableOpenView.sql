
CREATE View [dbo].[AccountReceivableOpenView]
as
select 
ar.OpenAmount
,ar.GLDate
,ar.InvoiceId
,inv.invoiceNumber
,inv.description InvoiceDescription
,ar.DocNumber
,ar.Remarks
,ar.PaymentTerms
,ar.DiscountDueDate
,ar.CustomerId
,abCust.Name
,la.[Address Line 1]
,la.[Address Line 2]
,la.City
,la.State
,la.Zipcode
,coa.Account
,coa.Description CoaDescription
,(select sum(amount) from GeneralLedger gl 
where gl.AccountId=ar.AccountId and gl.DocNumber=ar.DocNumber) GLAmount
,(select max(checknumber) from GeneralLedger gl
where gl.AccountId=ar.AccountId and gl.DocNumber=ar.DocNumber) LastCheckNumber
from acctrec ar
join Invoice inv
	on ar.invoiceid=inv.invoiceid
join Customer cust
	on ar.CustomerId=cust.CustomerId
join udc udcDocType
	on ar.AcctRecDocTypeXRefId=udcDocType.XRefId
join AddressBook abCust
	on cust.AddressId=abCust.AddressId
join LocationAddress la
	on abCust.AddressId=la.AddressId
join ChartOfAccts coa
	on ar.AccountId=coa.AccountId
where ar.OpenAmount>0
