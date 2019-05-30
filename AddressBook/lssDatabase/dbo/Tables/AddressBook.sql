CREATE TABLE [dbo].[AddressBook] (
    [AddressId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (255) NULL,
    [FirstName]         VARCHAR (50)  NULL,
    [LastName]          VARCHAR (50)  NULL,
    [PeopleXrefId]      BIGINT        NULL,
    [CategoryCodeChar1] VARCHAR (50)  NULL,
    [CategoryCodeChar2] VARCHAR (50)  NULL,
    [CategoryCodeChar3] VARCHAR (50)  NULL,
    [CategoryCodeInt1]  INT           NULL,
    [CategoryCodeInt2]  INT           NULL,
    [CategoryCodeInt3]  INT           NULL,
    [CategoryCodeDate1] DATE          NULL,
    [CategoryCodeDate2] DATE          NULL,
    [CategoryCodeDate3] DATE          NULL,
    [CompanyName]       VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

