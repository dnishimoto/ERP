CREATE TABLE [dbo].[ScheduleEvent] (
    [ScheduleEventId]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [EmployeeId]          BIGINT   NOT NULL,
    [EventDateTime]       DATETIME NULL,
    [ServiceId]           BIGINT   NOT NULL,
    [DurationMinutes]     BIGINT   NULL,
    [CustomerId]          BIGINT   NULL,
    [ScheduleEventNumber] BIGINT   NOT NULL,
    CONSTRAINT [PK__Schedule__9EA964918AAF2100] PRIMARY KEY CLUSTERED ([ScheduleEventId] ASC),
    CONSTRAINT [FK__ScheduleE__Emplo__2759D01A] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK__ScheduleE__Servi__2942188C] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[ServiceInformation] ([ServiceId]),
    CONSTRAINT [FK_ScheduleEvent_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);



