USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[LocationAddress]    Script Date: 7/30/2018 6:36:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationAddress](
	[LocationId] [bigint] IDENTITY(1,1) NOT NULL,
	[Address Line 1] [varchar](255) NULL,
	[Address Line 2] [varchar](255) NULL,
	[City] [varchar](50) NULL,
	[Zipcode] [varchar](20) NULL,
	[TypeXRefId] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[State] [nchar](2) NULL,
	[Country] [varchar](50) NULL,
 CONSTRAINT [PK_LocationAddress] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocationAddress]  WITH CHECK ADD  CONSTRAINT [FK_LocationAddress_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[LocationAddress] CHECK CONSTRAINT [FK_LocationAddress_AddressBook]
GO
ALTER TABLE [dbo].[LocationAddress]  WITH CHECK ADD  CONSTRAINT [FK_LocationAddress_UDCType] FOREIGN KEY([TypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[LocationAddress] CHECK CONSTRAINT [FK_LocationAddress_UDCType]
GO
