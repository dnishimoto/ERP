CREATE TYPE [dbo].[AccountRegistrationTableType] AS TABLE (
    [CustomerName]  VARCHAR (50)  NULL,
    [FirstName]     VARCHAR (50)  NULL,
    [LastName]      VARCHAR (50)  NULL,
    [CompanyName]   VARCHAR (100) NULL,
    [Address_Line1] VARCHAR (100) NULL,
    [Address_Line2] VARCHAR (100) NULL,
    [City]          VARCHAR (100) NULL,
    [State]         VARCHAR (100) NULL,
    [Zipcode]       VARCHAR (100) NULL,
    [EmailText]     VARCHAR (50)  NULL,
    [LoginEmail]    BIT           NULL,
    [Password]      VARCHAR (50)  NULL);

