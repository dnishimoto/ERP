USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ServiceInformation]    Script Date: 7/30/2018 6:36:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInformation](
	[ServiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceDescription] [varchar](255) NULL,
	[Price] [money] NULL,
	[AddOns] [varchar](1000) NULL,
	[ServiceTypeXRefId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[LocationId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[SquareFeetOfStructure] [int] NULL,
	[LocationDescription] [varchar](255) NULL,
	[LocationGPS] [varchar](255) NULL,
	[Comments] [varchar](1000) NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([LocationId])
REFERENCES [dbo].[LocationAddress] ([LocationId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([ServiceTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
