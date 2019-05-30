
/****** Script for SelectTopNRows command from SSMS  ******/

CREATE View [dbo].[AccountReceivableView]
as

SELECT [AcctRecId]
      ,[OpenAmount]
      ,[GLDate]
      ,invoice.[InvoiceId]
	  ,invoice.InvoiceNumber
      ,[CreateDate]
      ,[DocNumber]
      ,[Remarks]
      ,customer.[CustomerId]
	  ,customerAddressbook.name CustomerName
      ,[PurchaseOrderId]
      ,acctrec.[Description]
      ,[AcctRecDocTypeXRefId]
	  ,udcDocType.Value docType
  FROM [dbo].[AcctRec] AcctRec join
  udc udcDocType on acctrec.AcctRecDocTypeXRefId=udcDocType.xrefid join
  customer customer on acctrec.customerid=customer.customerid join
  addressBook customerAddressbook on customer.AddressId=customeraddressbook.addressid join
  invoice invoice on acctrec.InvoiceId=invoice.InvoiceId

