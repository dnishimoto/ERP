CREATE TABLE [dbo].[WorkFlow] (
    [WorkflowId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [DataEntityId]       BIGINT        NOT NULL,
    [DataEntityType]     VARCHAR (100) NULL,
    [NextStep]           VARCHAR (50)  NULL,
    [PreviousStep]       VARCHAR (50)  NULL,
    [Sequence]           BIGINT        NULL,
    [PreviousWorkflowId] BIGINT        NOT NULL,
    CONSTRAINT [PK_WorkFlow] PRIMARY KEY CLUSTERED ([WorkflowId] ASC)
);

