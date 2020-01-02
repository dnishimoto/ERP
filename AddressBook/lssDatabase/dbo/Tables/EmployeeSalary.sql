CREATE TABLE [dbo].[EmployeeSalary] (
    [EmployeeSalaryId]   BIGINT IDENTITY (1, 1) NOT NULL,
    [Employee]           BIGINT NOT NULL,
    [AnnualSalary]       MONEY  NOT NULL,
    [StartEffectiveDate] DATE   NOT NULL,
    [EndEffectiveDate]   DATE   NOT NULL,
    CONSTRAINT [PK_EmployeeSalary] PRIMARY KEY CLUSTERED ([EmployeeSalaryId] ASC)
);

