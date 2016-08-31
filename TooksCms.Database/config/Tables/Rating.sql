CREATE TABLE [config].[Rating] (
    [RatingId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RatingUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Name]      NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED ([RatingId] ASC)
);

