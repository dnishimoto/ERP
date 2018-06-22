USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceSchedule]    Script Date: 6/17/2018 8:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceSchedule](
	[ScheduleId] [bigint] IDENTITY(1,1) NOT NULL,
	[ScheduleName] [varchar](255) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[ShiftId] [bigint] NULL,
	[ScheduleGroupXrefId] [bigint] NULL,
 CONSTRAINT [PK_TimeAndAttendanceSchedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
