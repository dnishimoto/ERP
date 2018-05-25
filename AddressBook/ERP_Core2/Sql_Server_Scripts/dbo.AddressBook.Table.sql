USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[AddressBook]    Script Date: 5/24/2018 9:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressBook](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Type] [varchar](20) NULL,
	[PeopleXrefId] [bigint] NULL,
	[ProductKey] [varchar](100) NULL,
	[PrimaryShippedToAddressId] [bigint] NULL,
	[PrimaryEmailId] [bigint] NULL,
	[PrimaryPhoneId] [bigint] NULL,
	[MailingAddressId] [bigint] NULL,
	[BillingAddressId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
