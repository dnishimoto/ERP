CREATE TABLE [dbo].[JobPhase] (
    [JobPhaseId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [PhaseGroup]     INT           NOT NULL,
    [JobMasterId]    BIGINT        NOT NULL,
    [Phase]          VARCHAR (100) NOT NULL,
    [ContractId]     BIGINT        NOT NULL,
    [JobPhaseNumber] BIGINT        NOT NULL,
    [JobCostTypeId]  BIGINT        NOT NULL,
    CONSTRAINT [PK_JobPhaseId] PRIMARY KEY CLUSTERED ([JobPhaseId] ASC),
    CONSTRAINT [FK_JobPhase_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    CONSTRAINT [FK_JobPhase_JobMaster] FOREIGN KEY ([JobMasterId]) REFERENCES [dbo].[JobMaster] ([JobMasterId])
);

