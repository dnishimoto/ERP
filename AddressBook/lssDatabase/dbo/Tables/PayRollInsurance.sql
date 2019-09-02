CREATE TABLE [dbo].[PayRollInsurance] (
    [PayRollInsuranceId] BIGINT        NOT NULL,
    [InsuranceCode]      VARCHAR (50)  NOT NULL,
    [Description]        VARCHAR (255) NOT NULL,
    [EligibleAmount]     MONEY         NOT NULL,
    [Rate]               MONEY         NOT NULL,
    [Employee]           BIGINT        NOT NULL,
    CONSTRAINT [PK_PayRollInsurance] PRIMARY KEY CLUSTERED ([PayRollInsuranceId] ASC)
);

