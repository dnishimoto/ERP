USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[CompanyForInvoiceView]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CompanyForInvoiceView]
AS
SELECT        dbo.Company.CompanyName, dbo.Company.CompanyCode, dbo.Company.CompanyStreet, dbo.Company.CompanyCity, dbo.Company.CompanyState, dbo.Company.CompanyZipcode, dbo.Invoice.InvoiceId, 
                         dbo.Invoice.InvoiceNumber
FROM            dbo.Company INNER JOIN
                         dbo.Invoice ON dbo.Company.CompanyId = dbo.Invoice.CompanyId
GO
