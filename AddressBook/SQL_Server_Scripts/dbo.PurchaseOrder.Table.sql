USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 7/30/2018 6:36:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PurchaseOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](20) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[GrossAmount] [money] NULL,
	[Remark] [varchar](max) NULL,
	[GLDate] [datetime] NULL,
	[AccountId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[ContractId] [bigint] NULL,
	[POQuoteId] [bigint] NULL,
	[Description] [varchar](1000) NULL,
	[PONumber] [varchar](50) NULL,
	[TakenBy] [nchar](10) NULL,
	[BuyerId] [bigint] NULL,
	[RequestedDate] [date] NULL,
	[PromisedDeliveredDate] [date] NULL,
	[Tax] [money] NULL,
	[TaxCode] [varchar](10) NULL,
	[TransactionDate] [datetime] NULL,
	[AmountReceived] [money] NULL,
	[AmountPaid] [money] NULL,
	[ShippedToName] [varchar](255) NULL,
	[ShippedToAddress1] [varchar](100) NULL,
	[ShippedToAddress2] [varchar](100) NULL,
	[ShippedToCity] [varchar](50) NULL,
	[ShippedToZipcode] [varchar](20) NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_ChartOfAccts]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
