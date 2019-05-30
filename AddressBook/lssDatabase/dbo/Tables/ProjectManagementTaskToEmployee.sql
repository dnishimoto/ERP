CREATE TABLE [dbo].[ProjectManagementTaskToEmployee] (
    [TaskToEmployeeId] BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeId]       BIGINT NULL,
    [TaskId]           BIGINT NULL,
    CONSTRAINT [PK_ProjectManagementTaskToEmployee] PRIMARY KEY CLUSTERED ([TaskToEmployeeId] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[ProjectManagementTask] ([TaskId])
);

