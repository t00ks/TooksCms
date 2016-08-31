CREATE TABLE [dbo].[ArticleImage] (
    [ArticleImageId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ArticleImageUid] UNIQUEIDENTIFIER CONSTRAINT [DF_ArticleImage_ArticleImageUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ArticleId]       INT              NOT NULL,
    [Image]           NVARCHAR (250)   NOT NULL,
    [Thumbnail]       NVARCHAR (250)   NOT NULL,
    [Position]        VARCHAR (10)     NOT NULL,
    [Size]            CHAR (1)         NOT NULL,
    CONSTRAINT [PK_ArticleImage] PRIMARY KEY CLUSTERED ([ArticleImageId] ASC),
    CONSTRAINT [FK_ArticleImage_Article] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([ArticleId])
);

