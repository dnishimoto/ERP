USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[SupervisorEmployees]    Script Date: 7/30/2018 6:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupervisorEmployees](
	[SupervisorEmployeesId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupervisorId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_SupervisorEmployees] PRIMARY KEY CLUSTERED 
(
	[SupervisorEmployeesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SupervisorEmployees]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[SupervisorEmployees]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorEmployees_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Supervisor] ([SupervisorId])
GO
ALTER TABLE [dbo].[SupervisorEmployees] CHECK CONSTRAINT [FK_SupervisorEmployees_Supervisor]
GO
