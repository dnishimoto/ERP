USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[InvoiceAndDetailView]    Script Date: 6/25/2018 6:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[InvoiceAndDetailView]
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
invoicesDetail.UnitPrice,
invoicesDetail.Quantity,
invoicesDetail.Amount DetailAmount,
invoicesDetail.DiscountPercent,
invoicesDetail.DiscountAmount,
invoicesDetail.ExtendedDescription

from invoice invoices
left join invoiceDetail invoicesDetail on invoices.invoiceId=invoicesDetail.invoiceId
inner join itemmaster itemmaster on invoicesdetail.itemid=itemmaster.itemid
GO
