CREATE TABLE [dbo].[PayRollEmployeeEarnings] (
    [PayRollEmployeeEarningsId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Employee]                  BIGINT        NOT NULL,
    [EarningCode]               INT           NOT NULL,
    [Description]               VARCHAR (255) NOT NULL,
    [EarningType]               VARCHAR (10)  NULL,
    [Amount]                    MONEY         NOT NULL,
    CONSTRAINT [PK_PayEmployeeRollEarnings] PRIMARY KEY CLUSTERED ([PayRollEmployeeEarningsId] ASC)
);

