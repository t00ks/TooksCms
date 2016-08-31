CREATE TABLE [dbo].[ArticleComment] (
    [ArticleCommentId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ArticleCommentUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [ArticleId]         INT              NOT NULL,
    [UserId]            INT              NULL,
    [GuestId]           INT              NULL,
    [Title]             NVARCHAR (512)   NOT NULL,
    [Content]           NVARCHAR (MAX)   NOT NULL,
    [Date]              DATETIME         NOT NULL,
    [ParentCommentId]   INT              NULL,
    CONSTRAINT [PK_ArticleComment] PRIMARY KEY CLUSTERED ([ArticleCommentId] ASC),
    CONSTRAINT [FK_ArticleComment_Article] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([ArticleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArticleComment_ArticleComment] FOREIGN KEY ([ParentCommentId]) REFERENCES [dbo].[ArticleComment] ([ArticleCommentId]),
    CONSTRAINT [FK_ArticleComment_Guest] FOREIGN KEY ([GuestId]) REFERENCES [user].[Guest] ([GuestId]),
    CONSTRAINT [FK_ArticleComment_User] FOREIGN KEY ([UserId]) REFERENCES [user].[User] ([UserId])
);

