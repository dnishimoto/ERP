CREATE TABLE [dbo].[NextNumber] (
    [NextNumberId]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [NextNumberName]  VARCHAR (30) NULL,
    [NextNumberValue] BIGINT       CONSTRAINT [DF_NextNumber_NextNumberValue] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_NextNumber] PRIMARY KEY CLUSTERED ([NextNumberId] ASC)
);

