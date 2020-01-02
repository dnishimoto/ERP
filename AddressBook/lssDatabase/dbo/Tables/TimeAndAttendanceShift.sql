CREATE TABLE [dbo].[TimeAndAttendanceShift] (
    [ShiftId]                      BIGINT       IDENTITY (1, 1) NOT NULL,
    [ShiftName]                    CHAR (20)    NULL,
    [ShiftStartTime]               VARCHAR (4)  NULL,
    [ShiftEndTime]                 VARCHAR (4)  NULL,
    [ShiftType]                    VARCHAR (50) NULL,
    [DurationHours]                INT          NULL,
    [DurationMinutes]              INT          NULL,
    [Monday]                       BIT          NULL,
    [Tuesday]                      BIT          NULL,
    [Wednesday]                    BIT          NULL,
    [Thursday]                     BIT          NULL,
    [Friday]                       BIT          NULL,
    [Saturday]                     BIT          NULL,
    [Sunday]                       BIT          NULL,
    [TimeAndAttendanceShiftNumber] BIGINT       NULL,
    CONSTRAINT [PK_TimeAndAttendanceShift] PRIMARY KEY CLUSTERED ([ShiftId] ASC)
);



