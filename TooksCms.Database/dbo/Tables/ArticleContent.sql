CREATE TABLE [dbo].[ArticleContent] (
    [ArticleContentId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ArticleContentUid] UNIQUEIDENTIFIER CONSTRAINT [DF_ArticleContent_ArticleContentUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ArticleId]         INT              NOT NULL,
    [Content]           XML              NOT NULL,
    [Version]           INT              NOT NULL,
    CONSTRAINT [PK_ArticleContent] PRIMARY KEY CLUSTERED ([ArticleContentId] ASC),
    CONSTRAINT [FK_ArticleContent_Article] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([ArticleId]) ON DELETE CASCADE
);

