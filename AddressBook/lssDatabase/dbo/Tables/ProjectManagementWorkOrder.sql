CREATE TABLE [dbo].[ProjectManagementWorkOrder] (
    [WorkOrderId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (100)   NULL,
    [StartDate]       DATETIME        NULL,
    [EndDate]         DATETIME        NULL,
    [ActualAmount]    MONEY           NULL,
    [ActualHours]     DECIMAL (18, 2) NULL,
    [EstimatedAmount] MONEY           NULL,
    [EstimatedHours]  DECIMAL (18, 2) NULL,
    [AccountNumber]   VARCHAR (50)    NULL,
    [Instructions]    VARCHAR (2000)  NULL,
    [ProjectId]       BIGINT          NOT NULL,
    [Status]          VARCHAR (20)    NULL,
    [Location]        VARCHAR (200)   NULL,
    [WorkOrderNumber] BIGINT          NULL,
    [AccountId]       BIGINT          NOT NULL,
    CONSTRAINT [PK_ProjectManagementWorkOrder] PRIMARY KEY CLUSTERED ([WorkOrderId] ASC),
    CONSTRAINT [FK_ProjectManagementWorkOrder_ProjectManagementWorkOrder] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
);



