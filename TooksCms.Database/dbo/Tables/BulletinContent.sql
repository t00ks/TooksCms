CREATE TABLE [dbo].[BulletinContent] (
    [BulletinContentId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BulletinContentUid] UNIQUEIDENTIFIER CONSTRAINT [DF_BulletinContent_BulletinContentUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [BulletinId]         INT              NOT NULL,
    [Content]            XML              NOT NULL,
    CONSTRAINT [PK_BulletinContent] PRIMARY KEY CLUSTERED ([BulletinContentId] ASC),
    CONSTRAINT [FK_BulletinContent_Bulletin] FOREIGN KEY ([BulletinId]) REFERENCES [dbo].[Bulletin] ([BulletinId])
);

