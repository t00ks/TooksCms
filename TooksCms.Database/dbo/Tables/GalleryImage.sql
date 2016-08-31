CREATE TABLE [dbo].[GalleryImage] (
    [GalleryImageId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GalleryImageUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [GalleryId]       INT              NOT NULL,
    [Image]           NVARCHAR (250)   NOT NULL,
    [Thumbnail]       NVARCHAR (250)   NOT NULL,
    CONSTRAINT [PK_GalleryImage] PRIMARY KEY CLUSTERED ([GalleryImageId] ASC),
    CONSTRAINT [FK_GalleryImage_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [dbo].[Gallery] ([GalleryId])
);

