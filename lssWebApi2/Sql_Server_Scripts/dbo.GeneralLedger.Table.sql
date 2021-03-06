USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[GeneralLedger]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralLedger](
	[GeneralLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[DocType] [varchar](10) NOT NULL,
	[Amount] [money] NOT NULL,
	[LedgerType] [varchar](10) NOT NULL,
	[GLDate] [datetime] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Comment] [varchar](255) NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[FiscalYear] [int] NULL,
	[FiscalPeriod] [int] NULL,
	[CheckNumber] [varchar](50) NULL,
	[PurchaseOrderNumber] [varchar](50) NULL,
 CONSTRAINT [PK__generalL__3214EC07AC773B83] PRIMARY KEY CLUSTERED 
(
	[GeneralLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AddressBook]
GO
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_ChartOfAccts]
GO
