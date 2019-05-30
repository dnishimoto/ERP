CREATE TABLE [dbo].[TimeAndAttendanceShift] (
    [ShiftId]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [ShiftName]       CHAR (20)    NULL,
    [ShiftStartTime]  INT          NULL,
    [ShiftEndTime]    INT          NULL,
    [ShiftType]       VARCHAR (50) NULL,
    [DurationHours]   INT          NULL,
    [DurationMinutes] INT          NULL,
    [Monday]          BIT          NULL,
    [Tuesday]         BIT          NULL,
    [Wednesday]       BIT          NULL,
    [Thursday]        BIT          NULL,
    [Friday]          BIT          NULL,
    [Saturday]        BIT          NULL,
    [Sunday]          BIT          NULL,
    CONSTRAINT [PK_TimeAndAttendanceShift] PRIMARY KEY CLUSTERED ([ShiftId] ASC)
);

