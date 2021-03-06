USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[AcctRec]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcctRec](
	[AcctRecId] [bigint] IDENTITY(1,1) NOT NULL,
	[OpenAmount] [money] NULL,
	[DiscountDueDate] [date] NULL,
	[GLDate] [date] NULL,
	[InvoiceId] [bigint] NOT NULL,
	[CreateDate] [date] NULL,
	[DocNumber] [bigint] NULL,
	[Remarks] [varchar](255) NULL,
	[PaymentTerms] [varchar](50) NULL,
	[CustomerId] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[Description] [varchar](255) NULL,
	[AcctRecDocTypeXRefId] [bigint] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[PaymentDueDate] [date] NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [money] NULL,
 CONSTRAINT [PK__AcctRec__4B67207728200668] PRIMARY KEY CLUSTERED 
(
	[AcctRecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_ChartOfAccts]
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
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_UDC] FOREIGN KEY([AcctRecDocTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_UDC]
GO
