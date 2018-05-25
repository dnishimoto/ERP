USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ShipmentsDetail]    Script Date: 5/24/2018 9:44:51 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
