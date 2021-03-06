USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ShipmentsDetail]    Script Date: 7/30/2018 6:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentsDetail](
	[ShipmentDetailId] [bigint] NOT NULL,
	[ShipmentId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[SalesOrderDetailId] [bigint] NOT NULL,
 CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED 
(
	[ShipmentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShipmentsDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[ShipmentsDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_ItemMaster]
GO
ALTER TABLE [dbo].[ShipmentsDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY([ShipmentId])
REFERENCES [dbo].[Shipments] ([ShipmentId])
GO
ALTER TABLE [dbo].[ShipmentsDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_Shipments]
GO
