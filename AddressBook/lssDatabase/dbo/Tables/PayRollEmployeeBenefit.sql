CREATE TABLE [dbo].[PayRollEmployeeBenefit] (
    [PayrollEmployeeBenefitsId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [Employee]                  BIGINT       NOT NULL,
    [BenefitCode]               VARCHAR (50) NOT NULL,
    [Amount]                    MONEY        NOT NULL,
    [TransactionCode]           BIGINT       NOT NULL,
    [Percentage]                MONEY        NULL,
    [Frequency]                 VARCHAR (50) NOT NULL,
    [BenefitOption]             INT          NULL,
    CONSTRAINT [PK_PayRollEmployeeBenefit] PRIMARY KEY CLUSTERED ([PayrollEmployeeBenefitsId] ASC)
);



