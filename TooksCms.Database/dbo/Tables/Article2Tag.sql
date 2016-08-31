CREATE TABLE [dbo].[Article2Tag] (
    [ArticleId] INT NOT NULL,
    [TagId]     INT NOT NULL,
    CONSTRAINT [PK_Article2Tag] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [TagId] ASC),
    CONSTRAINT [FK_Article2Tag_Article] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([ArticleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Article2Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [lookup].[Tag] ([TagId]) ON DELETE CASCADE
);

