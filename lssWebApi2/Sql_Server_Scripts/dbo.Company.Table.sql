USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[CompanyCode] [varchar](10) NULL,
	[CompanyStreet] [varchar](100) NULL,
	[CompanyCity] [varchar](50) NULL,
	[CompanyState] [varchar](20) NULL,
	[CompanyZipcode] [varchar](20) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
