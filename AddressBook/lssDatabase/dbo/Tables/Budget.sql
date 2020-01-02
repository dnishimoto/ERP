CREATE TABLE [dbo].[Budget] (
    [BudgetId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [BudgetHours]     DECIMAL (18, 1) NULL,
    [BudgetAmount]    DECIMAL (18, 4) NULL,
    [ActualHours]     DECIMAL (18, 2) NULL,
    [ActualAmount]    DECIMAL (18, 4) NULL,
    [AccountId]       BIGINT          NULL,
    [RangeId]         BIGINT          NULL,
    [ProjectedHours]  DECIMAL (18, 1) NULL,
    [ProjectedAmount] DECIMAL (18, 4) NULL,
    [ActualsAsOfDate] DATE            NULL,
    [BudgetNumber]    BIGINT          NOT NULL,
    CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([BudgetId] ASC),
    CONSTRAINT [FK_Budget_BudgetRange] FOREIGN KEY ([RangeId]) REFERENCES [dbo].[BudgetRange] ([RangeId]),
    CONSTRAINT [FK_Budget_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccount] ([AccountId])
);



