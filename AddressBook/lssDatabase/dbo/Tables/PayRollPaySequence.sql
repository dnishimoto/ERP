CREATE TABLE [dbo].[PayRollPaySequence] (
    [PayRollPaySequenceId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [PaySequence]              BIGINT       NOT NULL,
    [PayRollBeginDate]         DATE         NOT NULL,
    [PayRollEndDate]           DATE         NOT NULL,
    [Frequency]                VARCHAR (50) NOT NULL,
    [PayRollGroupCode]         INT          NOT NULL,
    [PayRollPaySequenceNumber] BIGINT       NOT NULL,
    CONSTRAINT [PK_PayRollPaySequence] PRIMARY KEY CLUSTERED ([PayRollPaySequenceId] ASC),
    CONSTRAINT [PayRollPaySequenceConstraint] UNIQUE NONCLUSTERED ([PaySequence] ASC, [PayRollBeginDate] ASC, [PayRollEndDate] ASC, [PayRollGroupCode] ASC)
);



