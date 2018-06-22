USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceShift]    Script Date: 6/17/2018 8:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceShift](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftName] [char](20) NULL,
	[ShiftStartTime] [nchar](14) NULL,
	[ShiftEndTime] [nchar](14) NULL,
	[ShiftTypeXrefId] [bigint] NULL,
 CONSTRAINT [PK_TimeAndAttendanceShift] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
