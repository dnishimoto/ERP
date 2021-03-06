USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ProjectManagementProject]    Script Date: 7/30/2018 6:36:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementProject](
	[ProjectId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](255) NULL,
	[Version] [varchar](50) NULL,
	[Description] [varchar](2000) NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[EstimatedEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
 CONSTRAINT [PK_ProjectManagementProject] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
