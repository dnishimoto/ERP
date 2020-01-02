CREATE TABLE [dbo].[Employee] (
    [EmployeeId]             BIGINT       IDENTITY (1, 1) NOT NULL,
    [AddressId]              BIGINT       NOT NULL,
    [JobTitleXrefId]         BIGINT       NOT NULL,
    [EmploymentStatusXRefId] BIGINT       NOT NULL,
    [HiredDate]              DATE         NULL,
    [TerminationDate]        DATE         NULL,
    [TaxIdentification]      VARCHAR (50) NULL,
    [PayRollGroupCode]       INT          NULL,
    [Salary]                 MONEY        NULL,
    [HourlyRate]             MONEY        NULL,
    [SalaryPerPayPeriod]     MONEY        NULL,
    [EmployeeNumber]         BIGINT       NOT NULL,
    CONSTRAINT [PK__Employee__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [FK__Employee__Addres__4AD81681] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId]),
    CONSTRAINT [FK__Employee__Employ__49E3F248] FOREIGN KEY ([EmploymentStatusXRefId]) REFERENCES [dbo].[UDC] ([XRefId]),
    CONSTRAINT [FK__Employee__JobTit__48EFCE0F] FOREIGN KEY ([JobTitleXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);





