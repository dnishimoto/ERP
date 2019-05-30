CREATE TABLE [dbo].[Comment] (
    [CommentId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [EntityId]       BIGINT        NOT NULL,
    [EntityType]     VARCHAR (50)  NOT NULL,
    [CommentContent] VARCHAR (MAX) NULL,
    [CommentNumber]  BIGINT        NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([CommentId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IDX_Entity]
    ON [dbo].[Comment]([EntityId] ASC, [EntityType] ASC);

