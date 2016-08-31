CREATE TABLE [config].[Rating2ArticleType2Category] (
    [RatingId]      INT NOT NULL,
    [ArticleTypeId] INT NOT NULL,
    [CategoryId]    INT NOT NULL,
    [Ordinal]       INT NOT NULL,
    CONSTRAINT [PK_Rating2ArticleType2Category] PRIMARY KEY CLUSTERED ([RatingId] ASC, [ArticleTypeId] ASC, [CategoryId] ASC),
    CONSTRAINT [FK_Rating2ArticleType2Category_ArticleType] FOREIGN KEY ([ArticleTypeId]) REFERENCES [config].[ArticleType] ([ArticleTypeId]),
    CONSTRAINT [FK_Rating2ArticleType2Category_Category] FOREIGN KEY ([CategoryId]) REFERENCES [lookup].[Category] ([CategoryId]),
    CONSTRAINT [FK_Rating2ArticleType2Category_Rating] FOREIGN KEY ([RatingId]) REFERENCES [config].[Rating] ([RatingId])
);

