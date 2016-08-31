CREATE TABLE [stat].[PageVisit] (
    [Id]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AreaType]       TINYINT        NOT NULL,
    [ItemId]         INT            NULL,
    [SearchTerm]     NVARCHAR (MAX) NULL,
    [Referer]        NVARCHAR (MAX) NOT NULL,
    [Url]            NVARCHAR (MAX) NOT NULL,
    [LinkType]       NVARCHAR (48)  NULL,
    [PreviousId]     INT            NULL,
    [UserAgent]      NVARCHAR (MAX) NOT NULL,
    [UserLanguages]  NVARCHAR (MAX) NOT NULL,
    [BrowserVersion] NVARCHAR (48)  NOT NULL,
    [BrowserName]    NVARCHAR (256) NOT NULL,
    [IpAddress]      NVARCHAR (48)  NOT NULL,
    [DateTime]       DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_PageVisit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageVisit_PageVisit] FOREIGN KEY ([PreviousId]) REFERENCES [stat].[PageVisit] ([Id])
);

