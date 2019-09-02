CREATE TABLE [dbo].[PayRollPaySequence] (
    [PayRollPaySequenceId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [PaySeqence]           BIGINT       NOT NULL,
    [PayRollBeginDate]     DATE         NOT NULL,
    [PayRollEndDate]       DATE         NOT NULL,
    [Frequency]            VARCHAR (50) NOT NULL,
    [PayRollGroupCode]     INT          NOT NULL,
    CONSTRAINT [PK_PayRollPaySequence] PRIMARY KEY CLUSTERED ([PayRollPaySequenceId] ASC)
);

