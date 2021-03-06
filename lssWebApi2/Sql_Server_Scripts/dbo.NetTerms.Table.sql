USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[NetTerms]    Script Date: 7/30/2018 6:36:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetTerms](
	[NetTermId] [bigint] IDENTITY(1,1) NOT NULL,
	[NetTerms] [varchar](50) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
 CONSTRAINT [PK_NetTerms] PRIMARY KEY CLUSTERED 
(
	[NetTermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
