USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 7/30/2018 6:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[AssetId] [bigint] IDENTITY(1,1) NOT NULL,
	[AssetCode] [varchar](50) NULL,
	[TagCode] [varchar](50) NULL,
	[ClassCode] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
	[Manufacturer] [varchar](100) NULL,
	[Model] [varchar](50) NULL,
	[SerialNumber] [varchar](50) NULL,
	[AcquiredDate] [date] NULL,
	[OriginalCost] [decimal](18, 2) NULL,
	[ReplacementCost] [decimal](18, 2) NULL,
	[Depreciation] [decimal](18, 2) NULL,
	[Location] [varchar](50) NULL,
	[SubLocation] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[EquipmentStatusXRefId] [bigint] NOT NULL,
	[GenericLocationLevel1] [varchar](50) NULL,
	[GenericLocationLevel2] [varchar](50) NULL,
	[GenericLocationLevel3] [varchar](50) NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_UDC] FOREIGN KEY([EquipmentStatusXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_Assets_UDC]
GO
