CREATE TABLE [dbo].[PayRollEmployeeSequence] (
    [EmployeePaySequenceId] BIGINT IDENTITY (1, 1) NOT NULL,
    [Employee]              BIGINT NOT NULL,
    [PayRollBeginDate]      DATE   NOT NULL,
    [PayRollEndDate]        DATE   NOT NULL,
    [PaySequence]           BIGINT NOT NULL,
    [PayRollCode]           INT    NOT NULL,
    CONSTRAINT [PK_PayRollEmployeeSequence] PRIMARY KEY CLUSTERED ([EmployeePaySequenceId] ASC)
);

