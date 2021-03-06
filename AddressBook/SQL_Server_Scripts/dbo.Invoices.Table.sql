USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 5/28/2018 10:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[InvoiceDate] [date] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CustomerId] [bigint] NOT NULL,
	[Description] [varchar](2000) NULL,
	[TaxAmount] [decimal](18, 4) NULL,
	[PaymentDueDate] [date] NULL,
	[PaymentTerms] [varchar](10) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Customer]
GO
