CREATE TABLE [dbo].[ProjectManagementTaskToEmployee] (
    [TaskToEmployeeId]     BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeId]           BIGINT NOT NULL,
    [TaskId]               BIGINT NOT NULL,
    [TaskToEmployeeNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_ProjectManagementTaskToEmployee] PRIMARY KEY CLUSTERED ([TaskToEmployeeId] ASC),
    CONSTRAINT [FK__ProjectMa__Emplo__4DB4832C] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[ProjectManagementTask] ([TaskId])
);





