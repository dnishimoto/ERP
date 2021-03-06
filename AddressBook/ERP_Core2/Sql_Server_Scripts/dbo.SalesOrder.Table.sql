USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 7/30/2018 6:36:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[SalesOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderType] [varchar](10) NULL,
	[CustomerId] [bigint] NOT NULL,
	[DeliveredToLocationId] [bigint] NULL,
	[ShippedToLocationId] [bigint] NULL,
	[InvoiceId] [bigint] NULL,
	[TakenBy] [nchar](10) NULL,
	[UnitOfMeasure] [nchar](10) NULL,
	[FreightAmount] [decimal](18, 4) NULL,
	[CarrierId] [bigint] NULL,
	[BuyerId] [bigint] NULL,
	[PaymentInstrument] [nchar](10) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[TransactionDate] [date] NULL,
	[ScheduledPickupDate] [datetime] NULL,
	[ActualPickupDate] [datetime] NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_Customer]
GO
