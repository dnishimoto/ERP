USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Equations]    Script Date: 7/30/2018 6:36:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[equation] [varchar](255) NULL,
	[queueid] [varchar](20) NULL,
	[evaluated] [varchar](255) NULL,
	[cellname] [varchar](10) NULL,
 CONSTRAINT [PK_equations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
