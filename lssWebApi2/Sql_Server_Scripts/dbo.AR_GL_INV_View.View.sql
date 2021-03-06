USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[AR_GL_INV_View]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[AR_GL_INV_View]
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
GO
