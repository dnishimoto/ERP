CREATE TABLE [dbo].[HumanResourcesSalary] (
    [HumanResourcesSalaryId] BIGINT IDENTITY (1, 1) NOT NULL,
    [Employee]               BIGINT NOT NULL,
    [AnnualizedSalary]       MONEY  NULL,
    [HourlyRate]             MONEY  NULL,
    [EffectiveDate]          DATE   NOT NULL,
    CONSTRAINT [PK_HumanResourcesSalary] PRIMARY KEY CLUSTERED ([HumanResourcesSalaryId] ASC)
);

