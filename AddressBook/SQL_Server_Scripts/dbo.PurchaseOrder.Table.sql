USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 6/17/2018 8:38:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PurchaseOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[POType] [varchar](10) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[GrossAmount] [money] NULL,
	[Remark] [varchar](max) NULL,
	[GLDate] [datetime] NULL,
	[AccountId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[ContractId] [bigint] NULL,
	[POQuoteId] [bigint] NULL,
	[Description] [varchar](1000) NULL,
	[PONumber] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[UnitOfMeasure] [nchar](50) NULL,
	[TakenBy] [nchar](10) NULL,
	[ShippedToLocationId] [bigint] NULL,
	[BuyerId] [bigint] NULL,
	[RequestedDate] [date] NULL,
	[PromisedDeliveredDate] [date] NULL,
	[Tax] [decimal](18, 4) NULL,
	[TaxCode] [varchar](10) NULL,
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
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Customer]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
