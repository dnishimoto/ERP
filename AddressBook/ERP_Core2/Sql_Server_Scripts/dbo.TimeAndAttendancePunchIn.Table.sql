USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[TimeAndAttendancePunchIn]    Script Date: 7/30/2018 6:36:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendancePunchIn](
	[TimePunchinId] [bigint] IDENTITY(1,1) NOT NULL,
	[PunchinDate] [date] NULL,
	[PunchinDateTime] [char](14) NULL,
	[PunchoutDateTime] [char](14) NULL,
	[JobCodeXrefId] [bigint] NOT NULL,
	[Approved] [bit] NULL,
	[EmployeeId] [bigint] NOT NULL,
	[SupervisorId] [bigint] NOT NULL,
	[ProcessedDate] [date] NULL,
	[PunchoutDate] [date] NULL,
	[Note] [varchar](255) NULL,
	[ShiftId] [bigint] NULL,
	[mealPunchin] [char](14) NULL,
	[mealPunchout] [char](14) NULL,
	[ScheduledToWork] [bit] NULL,
	[TypeOfTimeUdcXrefId] [bigint] NOT NULL,
	[ApprovingAddressId] [bigint] NOT NULL,
	[PayCodeXrefId] [bigint] NOT NULL,
	[ScheduleId] [bigint] NULL,
	[DurationInMinutes] [int] NULL,
	[MealDurationInMinutes] [int] NULL,
 CONSTRAINT [PK_TimeAndAttendancePunchIn] PRIMARY KEY CLUSTERED 
(
	[TimePunchinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Supervisor] ([SupervisorId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_Supervisor]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[TimeAndAttendanceSchedule] ([ScheduleId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift] FOREIGN KEY([ShiftId])
REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_UDC] FOREIGN KEY([TypeOfTimeUdcXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_UDC]
GO
