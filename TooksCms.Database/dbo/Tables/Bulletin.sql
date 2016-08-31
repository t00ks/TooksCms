CREATE TABLE [dbo].[Bulletin] (
    [BulletinId]     INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BulletinUid]    UNIQUEIDENTIFIER CONSTRAINT [DF_Bulletin_BulletinUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ArticleId]      INT              NULL,
    [GalleryId]      INT              NULL,
    [BulletinTypeId] INT              NOT NULL,
    [SiteId]         INT              NOT NULL,
    [Date]           DATETIME         NOT NULL,
    CONSTRAINT [PK_Bulletin] PRIMARY KEY CLUSTERED ([BulletinId] ASC),
    CONSTRAINT [FK_Bulletin_Article] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([ArticleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bulletin_BulletinType] FOREIGN KEY ([BulletinTypeId]) REFERENCES [config].[BulletinType] ([BulletinTypeId]),
    CONSTRAINT [FK_Bulletin_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [dbo].[Gallery] ([GalleryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bulletin_Site] FOREIGN KEY ([SiteId]) REFERENCES [security].[Site] ([SiteId])
);

