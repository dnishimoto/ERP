USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[CustomerLedger]    Script Date: 7/30/2018 6:36:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerLedger](
	[CustomerLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[AcctRecId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[GLDate] [date] NULL,
	[AccountId] [bigint] NOT NULL,
	[GeneralLedgerId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[Comment] [varchar](255) NULL,
	[AddressId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[DocType] [varchar](50) NOT NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[FiscalYear] [int] NOT NULL,
	[FiscalPeriod] [int] NOT NULL,
 CONSTRAINT [PK_CustomerLedger] PRIMARY KEY CLUSTERED 
(
	[CustomerLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_AcctRec] FOREIGN KEY([AcctRecId])
REFERENCES [dbo].[AcctRec] ([AcctRecId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_AcctRec]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_Customer]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_GeneralLedger] FOREIGN KEY([GeneralLedgerId])
REFERENCES [dbo].[GeneralLedger] ([GeneralLedgerId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_GeneralLedger]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_Invoice]
GO
