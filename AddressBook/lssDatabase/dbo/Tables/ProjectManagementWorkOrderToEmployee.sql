CREATE TABLE [dbo].[ProjectManagementWorkOrderToEmployee] (
    [WorkOrderToEmployeeId]     BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeId]                BIGINT NOT NULL,
    [WorkOrderId]               BIGINT NOT NULL,
    [WorkOrderToEmployeeNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_ProjectManagementWorkOrderToEmployee] PRIMARY KEY CLUSTERED ([WorkOrderToEmployeeId] ASC),
    CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_ProjectManagementWorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [dbo].[ProjectManagementWorkOrder] ([WorkOrderId])
);



