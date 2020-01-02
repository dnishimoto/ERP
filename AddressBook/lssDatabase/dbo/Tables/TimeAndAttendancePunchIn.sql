﻿CREATE TABLE [dbo].[TimeAndAttendancePunchIn] (
    [TimePunchinId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [PunchinDate]           DATE          NULL,
    [PunchinDateTime]       CHAR (14)     NULL,
    [PunchoutDateTime]      CHAR (14)     NULL,
    [JobCodeXrefId]         BIGINT        NOT NULL,
    [Approved]              BIT           NULL,
    [EmployeeId]            BIGINT        NOT NULL,
    [SupervisorId]          BIGINT        NOT NULL,
    [ProcessedDate]         DATE          NULL,
    [PunchoutDate]          DATE          NULL,
    [Note]                  VARCHAR (255) NULL,
    [ShiftId]               BIGINT        NULL,
    [mealPunchin]           CHAR (14)     NULL,
    [mealPunchout]          CHAR (14)     NULL,
    [ScheduledToWork]       BIT           NULL,
    [TypeOfTimeUdcXrefId]   BIGINT        NOT NULL,
    [ApprovingAddressId]    BIGINT        NOT NULL,
    [PayCodeXrefId]         BIGINT        NOT NULL,
    [ScheduleId]            BIGINT        NULL,
    [DurationInMinutes]     INT           NULL,
    [MealDurationInMinutes] INT           NULL,
    [TypeOfTime]            VARCHAR (20)  NULL,
    [PayCode]               VARCHAR (20)  NULL,
    [JobCode]               VARCHAR (20)  NULL,
    [TransferJobCode]       VARCHAR (20)  NULL,
    [TransferSupervisorId]  BIGINT        NULL,
    [TaskStatusXRefId]      BIGINT        NULL,
    [TaskStatus]            VARCHAR (20)  NULL,
    [Account]               VARCHAR (100) NULL,
    [AreaCode]              VARCHAR (20)  NULL,
    [DepartmentCode]        VARCHAR (20)  NULL,
    CONSTRAINT [PK_TimeAndAttendancePunchIn] PRIMARY KEY CLUSTERED ([TimePunchinId] ASC),
    CONSTRAINT [FK__TimeAndAt__Emplo__4CC05EF3] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_TimeAndAttendancePunchIn_Supervisor] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[Supervisor] ([SupervisorId]),
    CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[TimeAndAttendanceSchedule] ([ScheduleId]),
    CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId]),
    CONSTRAINT [FK_TimeAndAttendancePunchIn_UDC] FOREIGN KEY ([TypeOfTimeUdcXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



