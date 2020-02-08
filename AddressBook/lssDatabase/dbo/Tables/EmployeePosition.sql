CREATE TABLE [dbo].[EmployeePosition] (
    [PositionCodeId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [PositionCode]       VARCHAR (20)  NOT NULL,
    [CompanyId]          BIGINT        NOT NULL,
    [Description]        VARCHAR (100) NULL,
    [PositionCodeNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED ([PositionCodeId] ASC),
    CONSTRAINT [FK_EmployeePosition_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

