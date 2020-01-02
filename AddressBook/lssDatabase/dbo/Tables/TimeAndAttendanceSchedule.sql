CREATE TABLE [dbo].[TimeAndAttendanceSchedule] (
    [ScheduleId]                      BIGINT        IDENTITY (1, 1) NOT NULL,
    [ScheduleName]                    VARCHAR (255) NULL,
    [StartDate]                       DATE          NULL,
    [EndDate]                         DATE          NULL,
    [ShiftId]                         BIGINT        NULL,
    [ScheduleGroup]                   VARCHAR (50)  NULL,
    [Monday]                          BIT           NULL,
    [Tuesday]                         BIT           NULL,
    [Wednesday]                       BIT           NULL,
    [Thursday]                        BIT           NULL,
    [Friday]                          BIT           NULL,
    [Saturday]                        BIT           NULL,
    [Sunday]                          BIT           NULL,
    [TimeAndAttendanceScheduleNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_TimeAndAttendanceSchedule] PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    CONSTRAINT [FK_TimeAndAttendanceSchedule_TimeAndAttendanceShift] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
);



