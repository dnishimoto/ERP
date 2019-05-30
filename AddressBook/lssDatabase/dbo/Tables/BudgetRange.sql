CREATE TABLE [dbo].[BudgetRange] (
    [RangeId]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [StartDate]      DATE         NULL,
    [EndDate]        DATE         NULL,
    [Location]       VARCHAR (2)  NULL,
    [GenCode]        VARCHAR (3)  NULL,
    [SubCode]        VARCHAR (3)  NULL,
    [CompanyCode]    VARCHAR (10) NULL,
    [BusinessUnit]   VARCHAR (10) NULL,
    [Subsidiary]     VARCHAR (10) NULL,
    [AccountId]      BIGINT       NULL,
    [SupervisorCode] VARCHAR (50) NULL,
    [LastUpdated]    DATETIME     NULL,
    [ObjectNumber]   VARCHAR (10) NULL,
    [IsActive]       BIT          NULL,
    [PayCycles]      INT          NULL,
    CONSTRAINT [PK_BudgetRange] PRIMARY KEY CLUSTERED ([RangeId] ASC),
    CONSTRAINT [FK_BudgetRange_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccts] ([AccountId])
);

