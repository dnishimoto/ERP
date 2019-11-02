CREATE TABLE [dbo].[PayRollEmployeeDeductionLiabilities] (
    [PayRollEmployeeDeductionLiabilitiesId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Employee]                              BIGINT        NOT NULL,
    [DeductionLiabilitiesCode]              INT           NOT NULL,
    [Amount]                                MONEY         NULL,
    [Percentage]                            MONEY         NULL,
    [Description]                           VARCHAR (255) NOT NULL,
    [DeductionLiabilitiesType]              VARCHAR (10)  NOT NULL,
    [LimitAmount]                           MONEY         NULL,
    [BenefitOption]                         INT           NULL,
    [Frequency]                             VARCHAR (50)  NULL,
    CONSTRAINT [PK_PayRollEmployeeDeductionLiabilities] PRIMARY KEY CLUSTERED ([PayRollEmployeeDeductionLiabilitiesId] ASC)
);

