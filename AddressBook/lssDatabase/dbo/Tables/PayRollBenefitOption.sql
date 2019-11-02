CREATE TABLE [dbo].[PayRollBenefitOption] (
    [PayRollBenefitOptionId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [PayRollBenefitOption]   INT           NOT NULL,
    [Description]            VARCHAR (200) NULL,
    CONSTRAINT [PK_PayRollBenefitOption] PRIMARY KEY CLUSTERED ([PayRollBenefitOptionId] ASC)
);

