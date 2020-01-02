CREATE TABLE [dbo].[Supervisor] (
    [SupervisorId]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [AddressId]          BIGINT       NOT NULL,
    [SupervisorCode]     VARCHAR (20) NULL,
    [JobTitleXrefId]     BIGINT       NULL,
    [ParentSupervisorId] BIGINT       NULL,
    [IsActive]           BIT          NULL,
    [Area]               VARCHAR (20) NULL,
    [DepartmentCode]     VARCHAR (20) NULL,
    [SupervisorNumber]   BIGINT       NOT NULL,
    CONSTRAINT [PK__Supervisor__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED ([SupervisorId] ASC),
    CONSTRAINT [FK_Supervisor_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId]),
    CONSTRAINT [FK_Supervisor_UDC] FOREIGN KEY ([JobTitleXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



