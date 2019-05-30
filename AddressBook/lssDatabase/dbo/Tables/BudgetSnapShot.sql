CREATE TABLE [dbo].[BudgetSnapShot] (
    [BudgetId]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [BudgetHours]             DECIMAL (18, 1) NULL,
    [BudgetAmount]            DECIMAL (18, 4) NULL,
    [ActualHours]             DECIMAL (18, 2) NULL,
    [ActualAmount]            DECIMAL (18, 4) NULL,
    [AccountId]               BIGINT          NULL,
    [RangeId]                 BIGINT          NULL,
    [ProjectedHours]          DECIMAL (18, 1) NULL,
    [ProjectedAmount]         DECIMAL (18, 4) NULL,
    [OpenPurchaseOrderAmount] DECIMAL (18, 4) NULL,
    [Comments]                VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_BudgetSnapShot] PRIMARY KEY CLUSTERED ([BudgetId] ASC)
);

