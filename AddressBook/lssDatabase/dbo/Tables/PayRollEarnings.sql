CREATE TABLE [dbo].[PayRollEarnings] (
    [PayRollEarningsId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [EarningCode]       INT           NOT NULL,
    [Description]       VARCHAR (255) NOT NULL,
    [EarningType]       VARCHAR (10)  NULL,
    CONSTRAINT [PK_PayRollEarnings] PRIMARY KEY CLUSTERED ([PayRollEarningsId] ASC)
);

