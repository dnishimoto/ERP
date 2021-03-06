USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 7/30/2018 6:36:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[CarrierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[CarrierTypeXrefId] [bigint] NOT NULL,
 CONSTRAINT [PK_Carier] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD FOREIGN KEY([CarrierTypeXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD  CONSTRAINT [FK_Carier_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Carrier] CHECK CONSTRAINT [FK_Carier_AddressBook]
GO
