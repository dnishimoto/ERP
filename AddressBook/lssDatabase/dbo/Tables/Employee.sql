CREATE TABLE [dbo].[Employee] (
    [EmployeeId]             BIGINT       IDENTITY (1, 1) NOT NULL,
    [AddressId]              BIGINT       NOT NULL,
    [JobTitleXrefId]         BIGINT       NOT NULL,
    [EmploymentStatusXRefId] BIGINT       NOT NULL,
    [HiredDate]              DATE         NULL,
    [TerminationDate]        DATE         NULL,
    [TaxIdentification]      VARCHAR (50) NULL,
    [PayRollGroupCode]       INT          NULL,
    CONSTRAINT [PK__Employee__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId]),
    FOREIGN KEY ([EmploymentStatusXRefId]) REFERENCES [dbo].[UDC] ([XRefId]),
    FOREIGN KEY ([JobTitleXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



