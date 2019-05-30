CREATE TABLE [dbo].[ProjectManagementMilestones] (
    [MilestoneId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [MilestoneName]      VARCHAR (255)   NULL,
    [ProjectId]          BIGINT          NULL,
    [EstimatedHours]     DECIMAL (18, 2) NULL,
    [ActualDays]         INT             NULL,
    [EstimatedDays]      INT             NULL,
    [ActualHours]        DECIMAL (18, 2) NULL,
    [ActualStartDate]    DATETIME        NULL,
    [ActualEndDate]      DATETIME        NULL,
    [EstimatedStartDate] DATETIME        NULL,
    [EstimatedEndDate]   DATETIME        NULL,
    [Cost]               DECIMAL (18, 2) NULL,
    [WBS]                VARCHAR (50)    NULL,
    [MileStoneNumber]    BIGINT          NULL,
    CONSTRAINT [PK_ProjectManagementMilestones] PRIMARY KEY CLUSTERED ([MilestoneId] ASC),
    CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
);

