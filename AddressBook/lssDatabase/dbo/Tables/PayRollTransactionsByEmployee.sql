CREATE TABLE [dbo].[PayRollTransactionsByEmployee] (
    [PayRollTransactionsByEmployeeId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [Employee]                            BIGINT       NOT NULL,
    [PayRollTransactionCode]              BIGINT       NOT NULL,
    [Amount]                              MONEY        NOT NULL,
    [TaxPercentOfGross]                   MONEY        NULL,
    [AdditionalAmount]                    MONEY        NULL,
    [PayRollGroupCode]                    INT          NULL,
    [BenefitOption]                       INT          NULL,
    [PayRollTransactionsByEmployeeNumber] BIGINT       NULL,
    [PayRollType]                         VARCHAR (20) NULL,
    [TransactionType]                     VARCHAR (1)  NULL,
    CONSTRAINT [PK_PayRollTransactionsByEmployee] PRIMARY KEY CLUSTERED ([PayRollTransactionsByEmployeeId] ASC)
);





