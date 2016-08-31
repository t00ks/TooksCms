CREATE TABLE [dbo].[Gallery] (
    [GalleryId]       INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GalleryUid]      UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Title]           NVARCHAR (MAX)   NOT NULL,
    [CreatedByUserId] INT              NOT NULL,
    [CreatedDate]     DATETIME2 (7)    NOT NULL,
    [CategoryId]      INT              NOT NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED ([GalleryId] ASC),
    CONSTRAINT [FK_Gallery_Category] FOREIGN KEY ([CategoryId]) REFERENCES [lookup].[Category] ([CategoryId]),
    CONSTRAINT [FK_Gallery_User] FOREIGN KEY ([CreatedByUserId]) REFERENCES [user].[User] ([UserId])
);

