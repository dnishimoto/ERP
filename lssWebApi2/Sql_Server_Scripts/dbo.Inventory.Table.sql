USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 7/30/2018 6:36:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[ItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShortDescription] [varchar](100) NULL,
	[LongDescription] [varchar](255) NULL,
	[Remarks] [varchar](2000) NULL,
	[UOM] [varchar](100) NULL,
	[SKU] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[ExtendedPrice] [money] NULL,
	[DistributionAccountId] [bigint] NULL,
	[ReceivingAccountId] [bigint] NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_ItemMaster]
GO
