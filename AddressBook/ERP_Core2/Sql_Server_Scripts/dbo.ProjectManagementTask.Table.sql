USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ProjectManagementTask]    Script Date: 7/30/2018 6:36:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementTask](
	[TaskId] [bigint] IDENTITY(1,1) NOT NULL,
	[WBS] [varchar](50) NULL,
	[TaskName] [varchar](255) NULL,
	[Description] [varchar](2000) NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[EstimatedEndDate] [datetime] NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[MileStoneId] [bigint] NOT NULL,
	[StatusXrefId] [bigint] NOT NULL,
	[EstimatedCost] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
	[ProjectId] [bigint] NOT NULL,
	[AccountNumber] [varchar](100) NULL,
 CONSTRAINT [PK_ProjectManagementTask] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones] FOREIGN KEY([MileStoneId])
REFERENCES [dbo].[ProjectManagementMilestones] ([MilestoneId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_ProjectManagementProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_ProjectManagementProject]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_UDC] FOREIGN KEY([StatusXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_UDC]
GO
