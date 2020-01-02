CREATE TABLE [dbo].[PayRollDeductionLiabilities] (
    [PayRollDeductionLiabilitiesId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [DeductionLiabilitiesCode]          INT           NOT NULL,
    [Amount]                            MONEY         NULL,
    [Percentage]                        MONEY         NULL,
    [Description]                       VARCHAR (255) NOT NULL,
    [DeductionLiabilitiesType]          VARCHAR (10)  NOT NULL,
    [LimitAmount]                       MONEY         NULL,
    [PayRollDeductionLiabilitiesNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_PayRollDeductionLiabilities] PRIMARY KEY CLUSTERED ([PayRollDeductionLiabilitiesId] ASC)
);



