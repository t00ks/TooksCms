CREATE TABLE [dbo].[Gallery2Tag] (
    [GalleryId] INT NOT NULL,
    [TagId]     INT NOT NULL,
    CONSTRAINT [PK_Gallery2Tag] PRIMARY KEY CLUSTERED ([GalleryId] ASC, [TagId] ASC),
    CONSTRAINT [FK_Gallery2Tag_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [dbo].[Gallery] ([GalleryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Gallery2Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [lookup].[Tag] ([TagId]) ON DELETE CASCADE
);

