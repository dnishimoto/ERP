USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ProjectManagementMilestones]    Script Date: 7/30/2018 6:36:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementMilestones](
	[MilestoneId] [bigint] IDENTITY(1,1) NOT NULL,
	[MilestoneName] [varchar](255) NULL,
	[ProjectId] [bigint] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[WBS] [varchar](50) NULL,
 CONSTRAINT [PK_ProjectManagementMilestones] PRIMARY KEY CLUSTERED 
(
	[MilestoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectManagementMilestones]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementMilestones] CHECK CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject]
GO
