CREATE TABLE [dbo].[PayRollW4] (
    [PayRollW4Id]  BIGINT       IDENTITY (1, 1) NOT NULL,
    [Allowances]   INT          NOT NULL,
    [Employee]     BIGINT       NOT NULL,
    [Married]      BIT          NULL,
    [Single]       BIT          NULL,
    [PayFrequency] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PayRollW4] PRIMARY KEY CLUSTERED ([PayRollW4Id] ASC)
);

