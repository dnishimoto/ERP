CREATE TABLE [dbo].[TimeAndAttendanceSetup] (
    [TimeAndAttendanceSetupId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [TimeZone]                 VARCHAR (50) NULL,
    [DaylightSavings]          BIT          NULL,
    CONSTRAINT [PK_TimeAndAttendanceSetup] PRIMARY KEY CLUSTERED ([TimeAndAttendanceSetupId] ASC)
);

