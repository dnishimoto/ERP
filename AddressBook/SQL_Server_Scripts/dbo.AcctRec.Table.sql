USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[AcctRec]    Script Date: 6/17/2018 8:38:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcctRec](
	[AcctRecId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](10) NULL,
	[OpenAmount] [money] NULL,
	[DiscountDueDate] [date] NULL,
	[GLDate] [date] NULL,
	[InvoiceId] [bigint] NOT NULL,
	[CreateDate] [date] NULL,
	[DocNumber] [bigint] NULL,
	[Remarks] [varchar](255) NULL,
	[NetTerms] [varchar](10) NULL,
	[ItemId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[AcctRecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_Customer]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_Invoices]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_ItemMaster]
GO
