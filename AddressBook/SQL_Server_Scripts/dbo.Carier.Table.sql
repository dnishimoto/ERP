USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Carier]    Script Date: 5/28/2018 10:21:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carier](
	[CarrierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
 CONSTRAINT [PK_Carier] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Carier]  WITH CHECK ADD  CONSTRAINT [FK_Carier_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Carier] CHECK CONSTRAINT [FK_Carier_AddressBook]
GO
