CREATE TABLE [dbo].[TimeAndAttendanceScheduledToWork] (
    [ScheduledToWorkId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [EmployeeId]        BIGINT        NOT NULL,
    [ScheduleId]        BIGINT        NOT NULL,
    [ScheduleName]      VARCHAR (255) NULL,
    [StartDateTime]     VARCHAR (16)  NULL,
    [EndDateTime]       VARCHAR (16)  NULL,
    [StartDate]         DATE          NULL,
    [EndDate]           DATE          NULL,
    [EmployeeName]      VARCHAR (255) NULL,
    [ShiftId]           BIGINT        NOT NULL,
    [JobCode]           VARCHAR (20)  NULL,
    [PayCode]           VARCHAR (20)  NULL,
    [WorkedJobCode]     VARCHAR (20)  NULL,
    CONSTRAINT [PK_TimeAndAttendanceScheduledToWork] PRIMARY KEY CLUSTERED ([ScheduledToWorkId] ASC),
    CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceSchedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[TimeAndAttendanceSchedule] ([ScheduleId]),
    CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceShift] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
);

