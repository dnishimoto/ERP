CREATE TABLE [dbo].[ProjectManagementProject] (
    [ProjectId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProjectName]        VARCHAR (255)   NULL,
    [Version]            VARCHAR (50)    NULL,
    [Description]        VARCHAR (2000)  NULL,
    [ActualHours]        DECIMAL (18, 2) NULL,
    [ActualStartDate]    DATETIME        NULL,
    [ActualEndDate]      DATETIME        NULL,
    [EstimatedStartDate] DATETIME        NULL,
    [EstimatedHours]     DECIMAL (18, 2) NULL,
    [EstimatedEndDate]   DATETIME        NULL,
    [Cost]               DECIMAL (18, 2) NULL,
    [ActualDays]         INT             NULL,
    [EstimatedDays]      INT             NULL,
    [BudgetAmount]       MONEY           NULL,
    [BudgetHours]        DECIMAL (18, 2) NULL,
    [ProjectNumber]      BIGINT          NULL,
    CONSTRAINT [PK_ProjectManagementProject] PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);

