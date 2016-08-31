CREATE TABLE [user].[User] (
    [UserId]      INT              IDENTITY (1, 1) NOT NULL,
    [UserUid]     UNIQUEIDENTIFIER CONSTRAINT [DF_User_UserUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [LoginName]   NVARCHAR (256)   NOT NULL,
    [ScreenName]  NVARCHAR (256)   NOT NULL,
    [SiteId]      INT              NOT NULL,
    [Password]    NVARCHAR (512)   NOT NULL,
    [Salt]        NVARCHAR (512)   NOT NULL,
    [DateCreated] DATETIME         CONSTRAINT [DF_User_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreationIP]  VARCHAR (15)     NOT NULL,
    [LastLogin]   DATETIME         NOT NULL,
    [LastLoginIP] VARCHAR (15)     NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Site] FOREIGN KEY ([SiteId]) REFERENCES [security].[Site] ([SiteId])
);

