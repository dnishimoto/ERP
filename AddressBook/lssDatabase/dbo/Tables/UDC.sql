CREATE TABLE [dbo].[UDC] (
    [XRefId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [ProductCode] VARCHAR (20)  NULL,
    [KeyCode]     VARCHAR (50)  NULL,
    [Value]       VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([XRefId] ASC)
);

