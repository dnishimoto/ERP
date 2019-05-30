Create View AR_GL_INV_View
as
select AR.OpenAmount,
AR.DiscountDueDate,
AR.InvoiceId,
AR.DiscountPercent,
AR.DiscountAmount,
INV.InvoiceDate,
INV.InvoiceNumber,
GL.DocNumber,
AR.PaymentTerms,
AR.Amount,
GL.DocType GLDocType,
GL.LedgerType,
GL.GLDate,
COA.Account,
AB.Name CustomerName,
AB.CompanyName

 from acctrec AR
join generalledger GL
	on ar.docNumber=GL.docNumber and GL.DocType='OV'
join ChartOfAccts COA 
	on COA.AccountId= GL.accountid
join AddressBook AB
	on GL.AddressId=AB.AddressId
join Invoice INV
	on AR.InvoiceId=INV.InvoiceId