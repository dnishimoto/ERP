USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 7/30/2018 6:36:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipments](
	[ShipmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShipmentDate] [datetime] NULL,
	[CustomerId] [bigint] NOT NULL,
	[CarrierId] [bigint] NOT NULL,
	[TrackingNumber] [varchar](50) NULL,
	[ActualWeight] [decimal](18, 4) NULL,
	[BillableWeight] [decimal](18, 4) NULL,
	[Duty] [decimal](18, 4) NULL,
	[Tax] [decimal](18, 4) NULL,
	[ShippingCost] [decimal](18, 4) NULL,
	[Amount] [decimal](18, 4) NULL,
	[CodAmount] [decimal](18, 4) NULL,
	[ShippedFromLocationId] [bigint] NOT NULL,
	[ShippedToLocationId] [bigint] NULL,
	[PurchaseOrderId] [bigint] NULL,
	[VendorInvoiceId] [bigint] NULL,
	[VendorShippingCost] [decimal](18, 4) NULL,
	[VendorHandlingCost] [decimal](18, 4) NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [FK_Shipments_Customer]
GO
