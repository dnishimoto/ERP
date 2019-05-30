CREATE TABLE [dbo].[SupervisorEmployees] (
    [SupervisorEmployeesId] BIGINT IDENTITY (1, 1) NOT NULL,
    [SupervisorId]          BIGINT NOT NULL,
    [EmployeeId]            BIGINT NOT NULL,
    CONSTRAINT [PK_SupervisorEmployees] PRIMARY KEY CLUSTERED ([SupervisorEmployeesId] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_SupervisorEmployees_Supervisor] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[Supervisor] ([SupervisorId])
);

