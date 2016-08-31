CREATE TABLE [security].[Site] (
    [SiteId]  INT              IDENTITY (1, 1) NOT NULL,
    [SiteUid] UNIQUEIDENTIFIER CONSTRAINT [DF_Site_SiteUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Name]    NVARCHAR (128)   NOT NULL,
    [URL]     NVARCHAR (MAX)   NOT NULL,
    [Host]    NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([SiteId] ASC)
);

