﻿CREATE TABLE [dbo].[ProjectManagementTask] (
    [TaskId]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [WBS]                VARCHAR (50)    NULL,
    [TaskName]           VARCHAR (255)   NULL,
    [Description]        VARCHAR (2000)  NULL,
    [EstimatedStartDate] DATETIME        NULL,
    [EstimatedHours]     DECIMAL (18, 2) NULL,
    [EstimatedEndDate]   DATETIME        NULL,
    [ActualStartDate]    DATETIME        NULL,
    [ActualHours]        DECIMAL (18, 2) NULL,
    [ActualEndDate]      DATETIME        NULL,
    [Cost]               DECIMAL (18, 2) NULL,
    [MileStoneId]        BIGINT          NOT NULL,
    [StatusXrefId]       BIGINT          NOT NULL,
    [EstimatedCost]      DECIMAL (18, 2) NULL,
    [ActualDays]         INT             NULL,
    [EstimatedDays]      INT             NULL,
    [ProjectId]          BIGINT          NOT NULL,
    [AccountNumber]      VARCHAR (100)   NULL,
    [WorkOrderId]        BIGINT          NULL,
    [TaskNumber]         BIGINT          NULL,
    CONSTRAINT [PK_ProjectManagementTask] PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones] FOREIGN KEY ([MileStoneId]) REFERENCES [dbo].[ProjectManagementMilestone] ([MilestoneId]),
    CONSTRAINT [FK_ProjectManagementTask_ProjectManagementProject] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManagementProject] ([ProjectId]),
    CONSTRAINT [FK_ProjectManagementTask_UDC] FOREIGN KEY ([StatusXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



