CREATE TABLE [config].[ArticleType] (
    [ArticleTypeId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ArticleTypeUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Name]           NVARCHAR (200)   NOT NULL,
    [Description]    NVARCHAR (MAX)   NOT NULL,
    [Class]          NVARCHAR (MAX)   NOT NULL,
    [Assembly]       NVARCHAR (MAX)   NOT NULL,
    [Action]         NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_ArticleType] PRIMARY KEY CLUSTERED ([ArticleTypeId] ASC)
);

