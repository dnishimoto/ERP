CREATE TABLE [dbo].[BudgetNote] (
    [BudgetNoteId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [BudgetId]     BIGINT   NOT NULL,
    [Note]         NTEXT    NOT NULL,
    [Create]       DATETIME CONSTRAINT [DF_BudgetNote_Create] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BudgetNote] PRIMARY KEY CLUSTERED ([BudgetNoteId] ASC),
    CONSTRAINT [FK_BudgetNote_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budget] ([BudgetId])
);

