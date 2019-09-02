CREATE TABLE [dbo].[PayRollTransactionsByEmployee] (
    [PayRollTransactionsByEmployeeId] BIGINT IDENTITY (1, 1) NOT NULL,
    [Employee]                        BIGINT NOT NULL,
    [PayRollTransactionCode]          BIGINT NOT NULL,
    [Amount]                          MONEY  NOT NULL,
    [TaxPercentOfGross]               MONEY  NULL,
    [AdditionalAmount]                MONEY  NULL,
    CONSTRAINT [PK_PayRollTransactionsByEmployee] PRIMARY KEY CLUSTERED ([PayRollTransactionsByEmployeeId] ASC)
);

