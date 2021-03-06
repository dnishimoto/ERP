USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 7/30/2018 6:36:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[Amount] [decimal](18, 4) NULL,
	[PurchaseOrderLineId] [bigint] NULL,
	[SalesOrderDetailId] [bigint] NULL,
	[ItemId] [bigint] NOT NULL,
	[DiscountPercent] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[ShipmentDetailId] [bigint] NULL,
	[ExtendedDescription] [varchar](255) NULL,
 CONSTRAINT [PK_InvoiceLineDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesDetail_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoicesDetail_Invoices]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoicesDetail_ItemMaster]
GO
