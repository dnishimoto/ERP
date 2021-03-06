USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ProjectManagementTaskToEmployee]    Script Date: 7/30/2018 6:36:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementTaskToEmployee](
	[TaskToEmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[TaskId] [bigint] NULL,
 CONSTRAINT [PK_ProjectManagementTaskToEmployee] PRIMARY KEY CLUSTERED 
(
	[TaskToEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[ProjectManagementTask] ([TaskId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee] CHECK CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask]
GO
