CREATE TABLE [dbo].[Article] (
    [ArticleId]     INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ArticleUid]    UNIQUEIDENTIFIER CONSTRAINT [DF_Article_ArticleUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ArticleTypeId] INT              NOT NULL,
    [SiteId]        INT              NOT NULL,
    [Status]        INT              NOT NULL,
    [CategoryId]    INT              NOT NULL,
    [Date]          DATETIME         NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([ArticleId] ASC),
    CONSTRAINT [FK_Article_ArticleType] FOREIGN KEY ([ArticleTypeId]) REFERENCES [config].[ArticleType] ([ArticleTypeId]),
    CONSTRAINT [FK_Article_Category] FOREIGN KEY ([CategoryId]) REFERENCES [lookup].[Category] ([CategoryId]),
    CONSTRAINT [FK_Article_Site] FOREIGN KEY ([SiteId]) REFERENCES [security].[Site] ([SiteId])
);

