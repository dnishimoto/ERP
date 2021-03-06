USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[NextNumber]    Script Date: 7/30/2018 6:36:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NextNumber](
	[NextNumberId] [bigint] IDENTITY(1,1) NOT NULL,
	[NextNumberName] [varchar](20) NULL,
	[NextNumberValue] [bigint] NOT NULL,
 CONSTRAINT [PK_NextNumber] PRIMARY KEY CLUSTERED 
(
	[NextNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NextNumber] ADD  CONSTRAINT [DF_NextNumber_NextNumberValue]  DEFAULT ((1)) FOR [NextNumberValue]
GO
