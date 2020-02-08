CREATE TABLE [dbo].[ProjectManager] (
    [ProjectManagerId] BIGINT IDENTITY (1, 1) NOT NULL,
    [AddressBookId]    BIGINT NOT NULL,
    CONSTRAINT [PK_ProjectManager] PRIMARY KEY CLUSTERED ([ProjectManagerId] ASC)
);

