CREATE TABLE [dbo].[ScheduleEvent] (
    [ScheduleEventId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [EmployeeId]      BIGINT   NOT NULL,
    [EventDateTime]   DATETIME NULL,
    [ServiceId]       BIGINT   NOT NULL,
    [DurationMinutes] BIGINT   NULL,
    [CustomerId]      BIGINT   NULL,
    PRIMARY KEY CLUSTERED ([ScheduleEventId] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[ServiceInformation] ([ServiceId]),
    CONSTRAINT [FK_ScheduleEvent_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

