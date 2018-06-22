USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[AddressBook]    Script Date: 6/17/2018 8:38:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressBook](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[PeopleXrefId] [bigint] NULL,
	[CategoryCodeChar1] [varchar](50) NULL,
	[CategoryCodeChar2] [varchar](50) NULL,
	[CategoryCodeChar3] [varchar](50) NULL,
	[CategoryCodeInt1] [int] NULL,
	[CategoryCodeInt2] [int] NULL,
	[CategoryCodeInt3] [int] NULL,
	[CategoryCodeDate1] [date] NULL,
	[CategoryCodeDate2] [date] NULL,
	[CategoryCodeDate3] [date] NULL,
	[CompanyName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
