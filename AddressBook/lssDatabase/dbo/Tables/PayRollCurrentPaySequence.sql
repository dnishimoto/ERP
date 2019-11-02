CREATE TABLE [dbo].[PayRollCurrentPaySequence] (
    [PayRollCurrentPaySequenceId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [PaySequence]                     BIGINT       NOT NULL,
    [PayRollCurrentPaySequenceNumber] BIGINT       NOT NULL,
    [PayRollCode]                     BIGINT       NOT NULL,
    [PayRollBeginDate]                DATE         NOT NULL,
    [PayRollEndDate]                  DATE         NOT NULL,
    [Frequency]                       VARCHAR (50) NOT NULL,
    [Active]                          BIT          NOT NULL,
    CONSTRAINT [PK_PayRollCurrentPaySequence] PRIMARY KEY CLUSTERED ([PayRollCurrentPaySequenceId] ASC)
);

